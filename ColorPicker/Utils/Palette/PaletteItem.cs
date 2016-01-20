using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public class PaletteItem : Palette
    {
        public string Name { get; set; }
        public List<ListViewItem> ViewItems = new List<ListViewItem>();

        public PaletteItem(string fileName) : base(fileName)
        {
        }

        public ListViewItem Add(IColor c, int iconIndex)
        {
            var item = new ListViewItem() { Tag = c };
            item.SubItems.Add(c.ToString());
            item.ImageIndex = iconIndex;

            ViewItems.Add(item);

            if (!Items.Contains(c))
            {
                base.Add(c);
            }

            return item;
        }
        
        public override void Remove(IColor c)
        {
            foreach (var item in ViewItems)
            {
                if (item.Tag.Equals(c))
                {
                    ViewItems.Remove(item);
                    break;
                }
            }

            base.Remove(c);
        }

        public override void RemoveAt(int idx)
        {
            ViewItems.RemoveAt(idx);
            base.RemoveAt(idx);
        }

        public override void Load()
        {
            SetName();
            base.Load();
        }

        public override void Rename(string newName)
        {
            SetName();
            base.Rename(newName);
        }

        private void SetName()
        {
            this.Name = System.IO.Path.GetFileNameWithoutExtension(FileInfo.FullName);
        }
    }
}