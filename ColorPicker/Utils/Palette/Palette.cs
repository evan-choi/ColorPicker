using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ColorPicker.Utils
{
    public class Palette
    {
        public FileInfo FileInfo;

        public List<IColor> Items = new List<IColor>();

        public Palette(string fileName)
        {
            FileInfo = new FileInfo(fileName);

            if (!FileInfo.Exists)
            {
                string[] dirs = FileInfo.DirectoryName.Split(new[] { @"\" }, StringSplitOptions.None);
                string cDir = dirs[0];

                for (int i = 1; i < dirs.Length; i++)
                {
                    cDir += $@"\{dirs[i]}";

                    if (!Directory.Exists(cDir))
                    {
                        Directory.CreateDirectory(cDir);
                    }
                }

                FileInfo.Create().Close();
            }
        }

        public virtual void Rename(string newName)
        {
            FileInfo nfi = new FileInfo($@"{FileInfo.DirectoryName}\{newName}.p");

            if (nfi.Exists)
            {
                throw new Exception("이미 파일이 존재합니다");
            }

            Destroy();
            nfi.Create().Close();
            FileInfo = nfi;

            Save();
        }

        public IColor this[int idx]
        {
            get
            {
                return Items[idx];
            }
            set
            {
                Items[idx] = value;
            }
        }

        public virtual void Add(IColor c)
        {
            Items.Add(c);
        }

        public virtual void Remove(IColor c)
        {
            Items.Remove(c);
        }

        public virtual void RemoveAt(int idx)
        {
            Items.RemoveAt(idx);
        }

        public void Save()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var itm in Items)
            {
                Color oc = itm.ToColor();
                
                sb.AppendLine($"{itm.Type.ToString()} {oc.R} {oc.G} {oc.B}");
            }

            using (var fs = new FileStream(FileInfo.FullName, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sb.ToString());
                }
            }
        }

        public virtual void Load()
        {
            string src;

            using (var fs = new FileStream(FileInfo.FullName, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    src = sr.ReadToEnd();
                }
            }

            string pattern = @"(HEX|HSV|HSL|RGB) ([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5]) ([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5]) ([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])";
            
            foreach (Match m in Regex.Matches(src, pattern, RegexOptions.IgnoreCase))
            {
                ColorType ct = (ColorType)Enum.Parse(typeof(ColorType), m.Groups[1].Value);
                byte r, g, b;

                if (!byte.TryParse(m.Groups[2].Value, out r)) continue;
                if (!byte.TryParse(m.Groups[3].Value, out g)) continue;
                if (!byte.TryParse(m.Groups[4].Value, out b)) continue;

                Color oc = Color.FromArgb(r, g, b);
                IColor c;

                switch (ct)
                {
                    case ColorType.HEX:
                        c = HEXColor.FromColor(oc);
                        break;

                    case ColorType.HSL:
                        c = HSLColor.FromColor(oc);
                        break;

                    case ColorType.HSV:
                        c = HSVColor.FromColor(oc);
                        break;

                    default:
                        c = RGBColor.FromColor(oc);
                        break;
                }

                if (!Items.Contains(c))
                {
                    Items.Add(c);
                }
            }
        }

        public virtual void Destroy()
        {
            if (File.Exists(FileInfo.FullName))
            {
                try
                {
                    FileInfo.Delete();
                }
                catch
                {
                }
            }
        }
    }
}