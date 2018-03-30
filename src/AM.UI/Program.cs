using System;
using System.Reflection;
using System.Windows.Forms;
using AM.Core;
using Ninject;

namespace AM.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var player = kernel.Get<IAudioPlayer>();
            Application.Run(new Main(player));
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Error error = new Error(e.Exception);
            error.ShowDialog();
            Application.Exit();
        }
    }
}