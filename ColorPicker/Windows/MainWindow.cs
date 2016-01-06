using ColorPicker.Input;
using ColorPicker.Windows.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class MainWindow : SkinWindow
    {
        private MouseHook mHook;

        public MainWindow()
        {
            InitializeComponent();

            mHook = new MouseHook(this.Handle);
            mHook.Hook();

            mHook.OnMouseMove += MHook_OnMouseMove; 
        }

        private void MHook_OnMouseMove(MouseHook sender, Point pt)
        {
            UIInvoke(() =>
            {
                label1.Text = pt.ToString(); 
            });
        }

        private delegate void Action();
        private void UIInvoke(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
