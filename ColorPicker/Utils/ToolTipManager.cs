using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class ToolTipManager
    {
        private static ToolTip tip;

        static ToolTipManager()
        {
            tip = new ToolTip();
        }

        public static void SetToolTip(Control c, string content)
        {
            tip.SetToolTip(c, content);
        }
    }
}