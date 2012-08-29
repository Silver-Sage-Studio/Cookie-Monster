using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sage_Engine;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sage_Editor
{
    using Image = System.Drawing.Image;
    public partial class Form1 : Form
    {
        public SpriteBatch spriteBatch;

        public Texture2D EmptyTile;

        public Dictionary<string, Texture2D> dictTextures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Image> dictImages = new Dictionary<string, Image>();
        public Dictionary<string, TileLayer> dictLayer = new Dictionary<string,TileLayer>();

        public TileMap Map = new TileMap();
        public TileLayer currentLayer;

        public GraphicsDevice GraphicsDevices
        {
            get
            {
                return tileDisplay1.GraphicsDevice;
            }
        }

        public Form1()
        {
            

            InitializeComponent();
            tileDisplay1.OnInitialise += new EventHandler(tileDisplay1_OnInitialise);
            tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);

            Application.Idle += delegate { tileDisplay1.Invalidate(); };
            Application.Idle += delegate { vScrollBar1.Invalidate(); };

           
            while (string.IsNullOrEmpty(texPathAddress.Text))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    texPathAddress.Text = folderBrowserDialog1.SelectedPath;
                    activateControls();
                }
                else
                {
                    MessageBox.Show("Please Choose A Content Directory");
                }
            }

            
        }

       public void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            vScrollBar1.Location = new System.Drawing.Point(tileDisplay1.Location.X + tileDisplay1.Size.Width, vScrollBar1.Location.Y);
            Logic();
            Render();
            
        }

       void Form1_MouseWheel(object sender, MouseEventArgs e)
       {

           
           if (e.Delta > 0)
           {
               int x = e.Delta % 119;
               vScrollBar1.Value -= x;
              

               
           }
           else
           {
               int x = e.Delta % 119;
               vScrollBar1.Value -= x;
                 
           }
           Console.WriteLine(e.Delta);
           
       }

       private void Logic()
       {
           Camera.Position = new Vector2(hScrollBar1.Value * 4, vScrollBar1.Value * 4);

           if (currentLayer != null)
           {
               if (currentLayer.LayerWidthInPixels >= tileDisplay1.Width)
               {
                   hScrollBar1.Visible = true;
                   hScrollBar1.Minimum = 0;
                   hScrollBar1.Maximum = (int)Math.Ceiling((currentLayer.LayerWidthinTiles * TileLayer.GetTileWidth) * 0.25);
               }

               if (currentLayer.LayerHeightInPixels >= tileDisplay1.Height)
               {
                   vScrollBar1.Visible = true;
                   vScrollBar1.Minimum = 0;
                   vScrollBar1.Maximum = (int)Math.Ceiling((currentLayer.LayerHeightinTiles * TileLayer.GetTileHeight) * 0.25);
               }
           }
       }
       


     
      

        private void Render()
        {
            GraphicsDevices.Clear(Color.Black);
            
            DrawLayers();
        }


        private void DrawLayers()
        {
            //if (currentLayer == null)
            //    return;

            foreach (TileLayer layer in Map.Layers)
            {
                if (layer == null)
                    return;
                layer.Draw(spriteBatch);

                DrawEmptyTiles.DrawTiles();

                if (layer == currentLayer)
                    break;
            }
        }
            

        void tileDisplay1_OnInitialise(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevices);
            using( FileStream stream = new FileStream(@"Content/tile.png", FileMode.Open))
            {
                EmptyTile = Texture2D.FromStream(GraphicsDevices, stream);
            }
            radDraw.Select();
            DrawEmptyTiles.Initialise(this);

            Mouse.WindowHandle = tileDisplay1.Handle;
            MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.Focus();
            //Camera.Initialise(GraphicsDevices);
        }

        

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                texPathAddress.Text = folderBrowserDialog1.SelectedPath;
                activateControls();
             
            }
           
        }

        private void activateControls()
        {

            radDraw.Enabled = true;
            radErase.Enabled = true;
            chekFill.Enabled = true;
            barAlpha.Enabled = true;
            LayerList.Enabled = true;
            btnAddLayer.Enabled = true;
            btnRemoveTexture.Enabled = true;
            btnAddFiles.Enabled = false;
            btnRemoveLayer.Enabled = true;
            TextureList.Enabled = true;
            btnAddTexture.Enabled = true;
            btnRemoveLayer.Enabled = true;
            pictureBox1.Enabled = true;
        }

        private void exitBitchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            string layerName;
            int layerWidth;
            int layerHeight;
            TileLayer layer;

            LayerForm Form = new LayerForm();
            Form.ShowDialog();

            if (Form.OkPressed)
            {
                layerName = Form.txtLayerName.Text;
                layerWidth = int.Parse(Form.txtLayrWidth.Text);
                layerHeight = int.Parse(Form.txtLayHeight.Text);
                layer = new TileLayer(layerWidth, layerHeight);
                dictLayer.Add(layerName, layer);
                LayerList.Items.Add(layerName);
                Map.Addlayer(layer);
                currentLayer = layer;
                LayerList.SetSelected(LayerList.Items.Count - 1, true);
            }
            
        }

        private void LayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CurrentList = LayerList.SelectedItem.ToString();
            currentLayer = dictLayer[CurrentList];
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            if (currentLayer != null)
            {
                string Path;
                openFileDialog1.Filter = "Png|*.png|Jpeg|*.jpeg|Jpg|*.jpg";


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    Path = openFileDialog1.FileName;

                    FileStream stream = new FileStream(Path, FileMode.Open);

                    Texture2D text = Texture2D.FromStream(GraphicsDevices, stream);
                    Image img = Image.FromStream(stream);
                        
                    string[] FileNames = Path.Split('\\');
                    string FileName = FileNames[FileNames.Length - 1];
                    string[] tmpFileName = FileName.Split('.');
                    string FileNameMod = tmpFileName[0];
  
                    currentLayer.AddTexture(text);
                    foreach (TileLayer layer in Map.Layers)
                    {
                        if (layer.HasTexture(text) == -1)
                        {
                            layer.AddTexture(text);
                        }
                    }

                    dictTextures[FileNameMod] = text;
                    dictImages[FileNameMod] = img;

                    TextureList.Items.Add(FileNameMod);
                       
                    stream.Dispose();

                    

                }
            }
            else
            {
                MessageBox.Show("Select A Layer First Please");
            }
            
        }


        private void TextureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = dictImages[TextureList.SelectedItem as string];
        }

        private void btnRemoveTexture_Click(object sender, EventArgs e)
        {
            //Image i = Image.FromFile("D:\\silver sages\\Bull shit\\XNAGifAnimationv1.9D\\GifAnimationSample\\GifAnimationSampleContent\\texture1.jpg");
            //pictureBox1.Image = i;
        }

 

    }
}
