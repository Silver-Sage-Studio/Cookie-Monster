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
        private string WorkspacePath;
        public workspace()
        {
            InitializeComponent();
            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Directory.GetCurrentDirectory() + "\\Content\\editorInfo.info");
            XmlNode nodes = doc.DocumentElement;

            if ((nodes.Attributes["workspace"].Value as string).Equals(""))
            {
                
                folderBrowserDialog1.RootFolder=Environment.SpecialFolder.MyDocuments;
                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Sages Workspace";
                WorkspacePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {

                this.Hide();
                Form1 form = new Form1();
                form.Show();
               
                
            }

           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath+"\\Sages Workspace";
                WorkspacePath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void workspace_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(textBox1.Text);
            Directory.CreateDirectory(textBox1.Text+"\\Content Folder");
            Directory.CreateDirectory(textBox1.Text+"\\Data Files");


            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Directory.GetCurrentDirectory() + "\\Content\\editorInfo.info");
            XmlNode node = doc.DocumentElement;
            node.Attributes["workspace"].Value = textBox1.Text;
            doc.Save(System.IO.Directory.GetCurrentDirectory() + "\\Content\\editorInfo.info");

        }
    }
}
