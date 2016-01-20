using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class PaletteManager
    {
        private static Dictionary<string, PaletteItem> pDict = new Dictionary<string, PaletteItem>();
        public static string BaseDirectory { get; set; } = "Palettes";

        public static string[] GetPaletteNames()
        {
            List<string> lst = new List<string>();

            foreach (var p in pDict.Values)
            {
                lst.Add(p.Name);
            }

            return lst.ToArray();
        }

        public static PaletteItem[] GetPalettes()
        {
            List<PaletteItem> lst = new List<PaletteItem>();

            foreach (var p in pDict.Values)
            {
                lst.Add(p);
            }

            return lst.ToArray();
        }

        public static void AddPalette(PaletteItem palette)
        {
            if (!pDict.ContainsKey(palette.Name))
            {
                pDict.Add(palette.Name, palette);
            }
        }

        public static void RenamePalette(string paletteName, string newName)
        {
            var p = GetPalette(paletteName);
            p.Rename(newName);

            pDict.Remove(paletteName);
            pDict.Add(newName, p);
        }

        public static void AddPalette(string paletteName)
        {
            if (!pDict.ContainsKey(paletteName))
            {
                pDict.Add(paletteName, new PaletteItem($@"{AppDomain.CurrentDomain.BaseDirectory}{BaseDirectory}\{paletteName}.p") { Name = paletteName });
            }
        }

        public static void RemovePalette(int idx)
        {
            if (idx < pDict.Count)
            {
                PaletteItem p = pDict[NameFromIndex(idx)];

                p.Destroy();

                pDict.Remove(p.Name);
            }
        }

        public static void RemovePalette(string paletteName)
        {
            if (pDict.ContainsKey(paletteName))
            {
                pDict.Remove(paletteName);
            }
        }

        public static ListViewItem Add(string paletteName, IColor c, int iconIndex)
        {
            if (pDict.ContainsKey(paletteName))
            {
                return pDict[paletteName].Add(c, iconIndex);
            }

            return null;
        }

        public static string NameFromIndex(int idx)
        {
            int r = -1;

            foreach (var k in pDict.Keys)
            {
                r++;
                if (r == idx)
                {
                    return k;
                }
            }

            return "";
        }

        public static PaletteItem GetPalette(string paletteName)
        {
            if (pDict.ContainsKey(paletteName))
            {
                return pDict[paletteName];
            }

            return null;
        }

        public static void SaveAll()
        {
            foreach (var p in pDict.Values)
            {
                p.Save();
            }
        }
    }
}
