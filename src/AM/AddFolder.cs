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

        public string SelectedPath => txtFolderPath.Text;

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtFolderPath.Text))
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