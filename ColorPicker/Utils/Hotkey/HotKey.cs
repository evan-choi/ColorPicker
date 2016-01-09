using System.Windows.Forms;

namespace ColorPicker.Utils.Hotkey
{
    public class  HotKey
    {
        internal string Name { get; set; }
        public Keys[] Keys { get; set; }
        public HotKeyEvent Action { get; set; }
    }
}