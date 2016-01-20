using ColorPicker.Input;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class ToolTipManager
    {
        private static ToolTip tip;
        private static Control Parent;

        public static void Init(Control parent)
        {
            tip = new ToolTip();
            Parent = parent;
        }

        public static void SetToolTip(Control c, string content)
        {
            tip.SetToolTip(c, content);
        }

        public static void Show(string content, int duration = 1000)
        {
            Point mPt = Mouse.Position;

            tip.Show(content, Parent, mPt.X - Parent.Left + 1, mPt.Y - Parent.Top + 1, duration);
        }
    }
}