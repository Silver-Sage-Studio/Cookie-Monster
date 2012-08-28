using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sage_Editor
{
    public partial class LayerForm : Form
    {
        public LayerForm()
        {
            InitializeComponent();
        }

        public bool OkPressed;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OkPressed = true;
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            OkPressed = false;
            Close();

        }

        
    }
}
