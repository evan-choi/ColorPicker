using System;
using System.Windows.Forms;

namespace ColorPicker.Utils
{
    public static class CrossThreading
    {
        public delegate void Action();

        private static Control mControl;
        
        public static void UIInvoke(Action action)
        {
            if (mControl.InvokeRequired)
            {
                mControl.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public static void Init()
        {
            mControl = new Control();
        }
    }
}
