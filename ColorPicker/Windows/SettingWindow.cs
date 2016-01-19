using ColorPicker.Utils;
using ColorPicker.Utils.Hotkey;
using ColorPicker.Windows.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class SettingWindow : SkinWindow
    {
        private static string[] BaseKeyNames = new string[] { "None", "Ctrl", "Shift", "Alt" };
        private static Keys[] BaseKeys = new Keys[] { Keys.None, Keys.ControlKey, Keys.ShiftKey, Keys.Alt };

        private static string[] SubKeyNames = new string[] 
        {
            "None",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
            "Del", "End", "Insert", "Home", "PgDown", "PgUp",
            "+", "-"
        };
        private static Keys[] SubKeys = new Keys[]
        {
            Keys.None,
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.Y, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z,
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
            Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12,
            Keys.Delete, Keys.End, Keys.Insert, Keys.Home, Keys.PageDown, Keys.PageUp,
            Keys.Add, Keys.Subtract
        };

        private Setting Setting;

        private Dictionary<string, List<ComboBox>> cache = new Dictionary<string, List<ComboBox>>();
        private Dictionary<string, PropertyInfo> pCache = new Dictionary<string, PropertyInfo>();

        public SettingWindow(Setting setting)
        {
            InitializeComponent();

            this.Setting = setting;

            foreach (var pi in typeof(Setting).GetProperties())
            {
                if (pi.Name.StartsWith("Hk"))
                {
                    string dKey = pi.Name.Substring(2).ToLower();

                    pCache.Add(dKey, pi);
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)c;
                    string[] data = null;
                    string dKey = null;
                    bool isBase = false;

                    if (c.Name.StartsWith("cbBase"))
                    {
                        data = BaseKeyNames;
                        dKey = c.Name.Substring(7).ToLower();
                        isBase = true;
                    }
                    else if (c.Name.StartsWith("cbSub"))
                    {
                        data = SubKeyNames;
                        dKey = c.Name.Substring(6).ToLower();
                    }
                    else
                    {
                        continue;
                    }

                    HotKey hk = HotkeyManager.GetHotKey(dKey);

                    if (!cache.ContainsKey(dKey))
                    {
                        cache.Add(dKey, new List<ComboBox>());
                    }

                    cache[dKey].Add(cb);

                    foreach (var key in data)
                    {
                        cb.Items.Add(key);
                    }

                    cb.SelectedIndex = Array.IndexOf((isBase ? BaseKeys : SubKeys), hk.Keys[isBase ? 0 : 1]);

                    cb.SelectedIndexChanged += (s, e) =>
                    {
                        if (isBase)
                        {
                            hk.Keys[0] = BaseKeys[cb.SelectedIndex];
                        }
                        else
                        {
                            hk.Keys[1] = SubKeys[cb.SelectedIndex];
                        }

                        pCache[dKey].SetValue(setting, hk, null);
                    };

                    cb.Enabled = false;
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox chk = (CheckBox)c;
                    string dKey = chk.Name.Substring(3).ToLower();
                    HotKey hk = HotkeyManager.GetHotKey(dKey);
                    
                    chk.CheckedChanged += (s, e)=>
                    {
                        foreach (ComboBox b in cache[dKey])
                        {
                            b.Enabled = chk.Checked;
                        }

                        hk.Enabled = chk.Checked;

                        pCache[dKey].SetValue(setting, hk, null);
                    };

                    chk.Checked = hk.Enabled;
                }
            }
        }
    }
}