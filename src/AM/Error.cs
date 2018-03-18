using System.Windows.Forms;

namespace AM
{
    public partial class Error : Form
    {
        public Error()
        {
            InitializeComponent();
        }

        public string ErrorText
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
    }
}
