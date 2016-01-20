using ColorPicker.Utils;

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorPicker
{
    public static class Program
    {
        public static readonly string GUID =
            ((GuidAttribute)(typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0])).Value;

        [STAThread]
        static void Main()
        {
            if (PreventApplication.Register(GUID))
            {
                GlobalException.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Windows.MainWindow());
            }
        }
    }
}
