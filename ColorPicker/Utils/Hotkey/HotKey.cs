using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorPicker.Utils.Hotkey
{
    public class HotKey
    {
        internal string Name { get; set; }
        public Keys[] Keys { get; set; }
        public HotKeyEvent Action { get; set; }
        public bool Enabled { get; set; } = true;

        public override string ToString()
        {
            var kLst = new List<string>();

            foreach (Keys k in Keys)
            {
                kLst.Add(k.ToString());
            }

            return $"{Enabled}, {string.Join(", ", kLst.ToArray())}";
        }

        public void Apply(string valueLine)
        {
            string[] values = valueLine.Split(',');

            Enabled = bool.Parse(values[0]);

            var kv = new KeysConverter();
            var kLst = new List<Keys>();

            for (int i = 1; i < values.Length; i++)
            {
                kLst.Add((Keys)kv.ConvertFromString(values[i]));
            }

            Keys = kLst.ToArray();
        }
    }
}