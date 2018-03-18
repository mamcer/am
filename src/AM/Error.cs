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
            get => txtErrorMessage.Text;
            set => txtErrorMessage.Text = value;
        }
    }
}