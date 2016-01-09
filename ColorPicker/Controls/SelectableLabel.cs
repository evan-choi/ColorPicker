using ColorPicker.Native;

using System;
using System.Windows.Forms;

namespace ColorPicker.Controls
{
    public class SelectableLabel : TextBox
    {
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            
            NativeMethods.HideCaret(this.Handle);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            NativeMethods.HideCaret(this.Handle);
        }

        public SelectableLabel()
        {
            this.BorderStyle = BorderStyle.None;
            this.ReadOnly = true;
            this.BackColor = System.Drawing.Color.White;
            this.TabStop = false;
        }
    }
}
