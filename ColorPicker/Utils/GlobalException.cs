using ColorPicker.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class GlobalException
    {
        public static void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorWindow ew = new ErrorWindow((Exception)e.ExceptionObject);
            ew.ShowDialog();
            Environment.Exit(1);
        }
    }
}
