using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Sage_Editor
{
    public partial class workspace : Form
    {
        public workspace()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;

                }
            }
        

        private void workspace_Load(object sender, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content/editorInfo.info");
            XmlNode nodes = doc.DocumentElement;

            if (((nodes.Attributes["workspace"].Value as string).Equals("")) || !(Directory.Exists(nodes.Attributes["workspace"].Value)))
            {

                folderBrowserDialog1.SelectedPath =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GitHub\\Cookie-Monster\\Sage Engine";
                
                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\GitHub\\Cookie-Monster\\Sage Engine";
            }
            else
            {

                var form1 = new Form1();
                form1.Show();
                this.Visible = false;
                //this.Hide();
                //this.Close();
                //this.Dispose();

             

                    
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

                    
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content/editorInfo.info");
            XmlNode node = doc.DocumentElement;
            node.Attributes["workspace"].Value = textBox1.Text;
            doc.Save(@"Content/editorInfo.info");

            this.Hide();
            Form1 form = new Form1();
            form.Show();
            
        }
    }
}
