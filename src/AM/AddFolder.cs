using System;
using System.IO;
using System.Windows.Forms;

namespace AM
{
    public partial class AddFolder : Form
    {
        public AddFolder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public string SelectedPath
        {
            get 
            {
                return textBox1.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                label1.Visible = true;
            }
        }
    }
}
