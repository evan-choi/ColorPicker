using ColorPicker.Utils.Hotkey;

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ColorPicker.Utils
{
    public class Setting
    {
        private const string SECTION_GENERAL = "General";
        private const string SECTION_HOTKEY = "Hotkey";
        private const string SECTION_PALETTE = "Palette";

        public event UpdatedHandler Updated;
        public delegate void UpdatedHandler(object sender);

        #region [ General ]
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

        private string[] _palettes = new string[] { };
        public string[] PaletteList
        {
            get
            {
                return _palettes;
            }
            set
            {
                _palettes = value;
                INI?.SetValue(SECTION_PALETTE, nameof(PaletteList), string.Join(",", value));
            }
        }
        #endregion

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

            var fsw = new FileSystemWatcher();
            fsw.Path = INI.FileInfo.DirectoryName;
            fsw.Filter = INI.FileInfo.Name;
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            fsw.Changed += Fsw_Changed;
            fsw.EnableRaisingEvents = true;

            Update();
        }
        
        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            TimeSpan delta = DateTime.Now - INI.LastUpdate;

            if (Math.Abs(delta.TotalMilliseconds) >= 800)
            {
                Update();
                Updated?.Invoke(this);
            }
        }

        private void Update()
        {
            LoadHotkey(out _HkPause, "Pause");
            LoadHotkey(out _HkRgb, "RGB");
            LoadHotkey(out _HkHex, "HEX");
            LoadHotkey(out _HkHsl, "HSL");
            LoadHotkey(out _HkHsb, "HSB");
            LoadHotkey(out _HkZoomIn, "ZoomIn");
            LoadHotkey(out _HkZoomOut, "ZoomOut");

            Zoom = Math.Max(1, INI.GetValue(SECTION_GENERAL, nameof(Zoom), _zoom));
            ShowGrid = INI.GetValue(SECTION_GENERAL, nameof(ShowGrid), _showGrid);
            SemiControl = INI.GetValue(SECTION_GENERAL, nameof(SemiControl), _semiControl);
            PaletteList = Regex.Split(INI.GetValue(SECTION_PALETTE, nameof(PaletteList), ""), ",");
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