using ColorPicker.Windows;

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class GlobalException
    {
        public static void Init()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ExceptionHandling(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandling((Exception)e.ExceptionObject);
        }

        private static void ExceptionHandling(Exception e)
        {
#if DEBUG
            if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                PreventApplication.UnRegister();

                if (MessageBox.Show("계속 진행하기 위해서는 관리자 권한이 필요합니다.\r\n계속 진행하시려면 확인 버튼을 클릭해 주세요.", "ColorPicker Principal", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo() { UseShellExecute = true, Verb = "runas", FileName = Application.ExecutablePath });
                }
                
                Environment.Exit(1);
            }
            else
            {
                ErrorWindow ew = new ErrorWindow(e);
                if (ew.ShowDialog() == DialogResult.OK)
                {
                    Environment.Exit(1);
                }
                else
                {
                }
            }
#endif
        }
    }
}