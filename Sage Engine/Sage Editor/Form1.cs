using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sage_Editor
{
    using Image = System.Drawing.Image;
    public partial class Form1 : Form
    {
        SpriteBatch spriteBatch;
        Camera camera = new Camera();
        TileLayer layer = new TileLayer();
      


        public TileMap Map = new TileMap();

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Image> Images = new Dictionary<string, Image>();


        public GraphicsDevice GraphicsDevice
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


            if (layer.LayerWidthInPixels >= tileDisplay1.Width)
            {
                hScrollBar1.Visible = true;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = layer.LayerWidthinTiles; 
            }

            if (layer.LayerHeightinTiles >= tileDisplay1.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = layer.LayerHeightinTiles;
            }

            while(string.IsNullOrEmpty(texPathAddress.Text))
            {
                if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
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

        void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            vScrollBar1.Location = new System.Drawing.Point(tileDisplay1.Location.X + tileDisplay1.Size.Width, vScrollBar1.Location.Y); 
            Logic();
            Render();
        }

        private void Logic()
        {
            Camera.position.X = hScrollBar1.Value * TileLayer.GetTileWidth;
            Camera.position.Y = vScrollBar1.Value * TileLayer.GetTileHeight;
        }

        private void Render()
        {
            GraphicsDevice.Clear(Color.Black);
            layer.Draw(spriteBatch);
        }

        void tileDisplay1_OnInitialise(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
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
            btnAddFiles.Enabled = true;
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

    

    }
}
