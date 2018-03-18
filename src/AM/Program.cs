using System;
using System.Windows.Forms;

namespace AM
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Golem());
            }
            catch (Exception ex)
            {
                Error error = new Error();
                error.ErrorText = ex.Message + ex.StackTrace;
                error.ShowDialog();
            }
        }
    }
}