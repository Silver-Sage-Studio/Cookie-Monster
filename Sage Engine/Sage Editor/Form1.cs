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
    public partial class Form1 : Form
    {
        SpriteBatch spriteBatch;
        Camera camera = new Camera();
        //TileLayer tileLayer = new TileLayer(new int[,] { });


        public Form1()
        {
            tileDisplay1.OnInitialise += new EventHandler(tileDisplay1_OnInitialise);
            tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);
            InitializeComponent();
        }

        void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void tileDisplay1_OnInitialise(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tileDisplay1_Click(object sender, EventArgs e)
        {

        }

    }
}
