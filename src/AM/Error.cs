using System;
using System.Windows.Forms;

namespace AM
{
    public partial class Error : Form
    {
        public Error(Exception ex)
        {
            InitializeComponent();

            
            txtErrorMessage.Text = ex.Message.ToUpper() + Environment.NewLine + Environment.NewLine + "STACK TRACE" + Environment.NewLine + ex.StackTrace;

            txtErrorMessage.Text += ex.InnerException != null
                ? Environment.NewLine + "INNER EXCEPTION" + Environment.NewLine +
                  ex.InnerException.Message
                : string.Empty;
        }

        private void lnkCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(txtErrorMessage.Text);
        }
    }
}