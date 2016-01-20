using ColorPicker.Windows.Base;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class ErrorWindow : SkinWindow
    {
        public ErrorWindow(Exception e)
        {
            InitializeComponent();

            errorBox.Text = $"Message: {e.Message}\r\n\r\nStackTrace:\r\n{e.StackTrace}";

            Rectangle sc = Screen.GetWorkingArea(this);
            Size sz = TextRenderer.MeasureText(errorBox.Text, errorBox.Font);

            this.Width = (int)Math.Min(sz.Width + errorBox.Left + (this.Width - errorBox.Right) + 15, sc.Width * 0.7);
            this.Height = (int)Math.Min(sz.Height + errorBox.Top + (this.Height - errorBox.Bottom), sc.Height * 0.7);

            this.Left = sc.X + sc.Width / 2 - this.Width / 2;
            this.Top = sc.Y + sc.Height / 2 - this.Height / 2;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnForce_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}