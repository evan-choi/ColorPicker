using ColorPicker.Utils.Hotkey;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public class Setting
    {
        private const string SECTION_GENERAL = "General";
        private const string SECTION_HOTKEY = "Hotkey";

        private float _zoom = 1f;
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                INI?.SetValue(SECTION_GENERAL, nameof(Zoom), value);
            }
        }

        private bool _showGrid = false;
        public bool ShowGrid
        {
            get
            {
                return _showGrid;
            }
            set
            {
                _showGrid = value;
                INI?.SetValue(SECTION_GENERAL, nameof(ShowGrid), _showGrid);
            }
        }

        private bool _semiControl = false;
        public bool SemiControl
        {
            get
            {
                return _semiControl;
            }
            set
            {
                _semiControl = value;
                INI?.SetValue(SECTION_GENERAL, nameof(SemiControl), _semiControl);
            }
        }

        #region [ Hotkey ]
        private HotKey _HkPause;
        public HotKey HkPause
        {
            get
            {
                return _HkPause;
            }
            set
            {
                _HkPause = value;
                HotkeyManager.Register("Pause", value);
                INI.SetValue(SECTION_HOTKEY, "Pause", value.ToString());
            }
        }

        private HotKey _HkRgb;
        public HotKey HkRgb
        {
            get
            {
                return _HkRgb;
            }
            set
            {
                _HkRgb = value;
                HotkeyManager.Register("RGB", value);
                INI.SetValue(SECTION_HOTKEY, "RGB", value.ToString());
            }
        }

        private HotKey _HkHsl;
        public HotKey HkHsl
        {
            get
            {
                return _HkHsl;
            }
            set
            {
                _HkHsl = value;
                HotkeyManager.Register("HSL", value);
                INI.SetValue(SECTION_HOTKEY, "HSL", value.ToString());
            }
        }

        private HotKey _HkHsb;
        public HotKey HkHsb
        {
            get
            {
                return _HkHsb;
            }
            set
            {
                _HkHsb = value;
                HotkeyManager.Register("HSB", value);
                INI.SetValue(SECTION_HOTKEY, "HSB", value.ToString());
            }
        }

        private HotKey _HkHex;
        public HotKey HkHex
        {
            get
            {
                return _HkHex;
            }
            set
            {
                _HkHex = value;
                HotkeyManager.Register("HEX", value);
                INI.SetValue(SECTION_HOTKEY, "HEX", value.ToString());
            }
        }

        private HotKey _HkZoomIn;
        public HotKey HkZoomIn
        {
            get
            {
                return _HkZoomIn;
            }
            set
            {
                _HkZoomIn = value;
                HotkeyManager.Register("ZoomIn", value);
                INI.SetValue(SECTION_HOTKEY, "ZoomIn", value.ToString());
            }
        }

        private HotKey _HkZoomOut;
        public HotKey HkZoomOut
        {
            get
            {
                return _HkZoomOut;
            }
            set
            {
                _HkZoomOut = value;
                HotkeyManager.Register("ZoomOut", value);
                INI.SetValue(SECTION_HOTKEY, "ZoomOut", value.ToString());
            }
        }
        #endregion

        public INIFile INI;

        public Setting(string fileName)
        {
            INI = new INIFile(fileName);

            LoadHotkey(out _HkPause, "Pause");
            LoadHotkey(out _HkRgb, "RGB");
            LoadHotkey(out _HkHex, "HEX");
            LoadHotkey(out _HkHsl, "HSL");
            LoadHotkey(out _HkHsb, "HSB");
            LoadHotkey(out _HkZoomIn, "ZoomIn");
            LoadHotkey(out _HkZoomOut, "ZoomOut");

            Zoom = INI.GetValue(SECTION_GENERAL, nameof(Zoom), _zoom);
            ShowGrid = INI.GetValue(SECTION_GENERAL, nameof(ShowGrid), _showGrid);
            SemiControl = INI.GetValue(SECTION_GENERAL, nameof(SemiControl), _semiControl);
        }

        private void LoadHotkey(out HotKey hk, string name)
        {
            hk = HotkeyManager.GetHotKey(name);
            string defv = hk.ToString();

            hk.Apply(INI.GetValue(SECTION_HOTKEY, name, defv));

            if (hk.Keys.Length < 2)
            {
                hk.Apply(defv);
            }
        }
    }
}