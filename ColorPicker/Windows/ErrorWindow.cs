using ColorPicker.Input;
using ColorPicker.Utils;
using ColorPicker.Windows.Base;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class ErrorWindow : SkinWindow
    {
        public ErrorWindow(Exception e)
        {
            InitializeComponent();

            errorBox.Text = $"Message: {e.Message}\r\n\r\nStackTrace:\r\n{e.StackTrace}";
        }
    }
}