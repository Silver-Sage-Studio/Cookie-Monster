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
<<<<<<< HEAD
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

=======
       
        public workspace()
        {
            InitializeComponent();
            
>>>>>>> workspace-final
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath+"\\Sages Workspace";
                WorkspacePath = folderBrowserDialog1.SelectedPath;
=======
           
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
             
>>>>>>> workspace-final
            }
        }

        private void workspace_Load(object sender, EventArgs e)
        {

<<<<<<< HEAD
=======
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


>>>>>>> workspace-final
        }

        private void button2_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Directory.CreateDirectory(textBox1.Text);
            Directory.CreateDirectory(textBox1.Text+"\\Content Folder");
            Directory.CreateDirectory(textBox1.Text+"\\Data Files");


            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Directory.GetCurrentDirectory() + "\\Content\\editorInfo.info");
            XmlNode node = doc.DocumentElement;
            node.Attributes["workspace"].Value = textBox1.Text;
            doc.Save(System.IO.Directory.GetCurrentDirectory() + "\\Content\\editorInfo.info");

=======
                    
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content/editorInfo.info");
            XmlNode node = doc.DocumentElement;
            node.Attributes["workspace"].Value = textBox1.Text;
            doc.Save(@"Content/editorInfo.info");

            this.Hide();
            Form1 form = new Form1();
            form.Show();
            
>>>>>>> workspace-final
        }
    }
}
