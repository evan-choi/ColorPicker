using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ColorPicker.Native;

namespace ColorPicker.Windows.Base
{
    public class ResizeWindow : DropShadowWindow
    {
        #region [ 열거형 ]
        [Flags]
        public enum ResizeDirection : short
        {
            Left = 0x01,
            Right = 0x02,
            Top = 0x04,
            Center = 0x08,
            Bottom = 0x16
        }
        #endregion

        #region [ 프리셋 ]
        private static ResizeDirection[] Directions =
        {
            ResizeDirection.Left | ResizeDirection.Top,
            ResizeDirection.Left | ResizeDirection.Center,
            ResizeDirection.Left | ResizeDirection.Bottom,
            ResizeDirection.Right | ResizeDirection.Top,
            ResizeDirection.Right | ResizeDirection.Center,
            ResizeDirection.Right | ResizeDirection.Bottom,
            ResizeDirection.Center | ResizeDirection.Top,
            ResizeDirection.Center | ResizeDirection.Bottom
        };

        private static Dictionary<ResizeDirection, NativeEnums.SCType> Direction_Drag =
            new Dictionary<ResizeDirection, NativeEnums.SCType>()
        {
            { Directions[0], NativeEnums.SCType.SC_DRAG_RESIZEUL },
            { Directions[1], NativeEnums.SCType.SC_DRAG_RESIZEL },
            { Directions[2], NativeEnums.SCType.SC_DRAG_RESIZEDL },
            { Directions[3], NativeEnums.SCType.SC_DRAG_RESIZEUR },
            { Directions[4], NativeEnums.SCType.SC_DRAG_RESIZER },
            { Directions[5], NativeEnums.SCType.SC_DRAG_RESIZEDR },
            { Directions[6], NativeEnums.SCType.SC_DRAG_RESIZEU },
            { Directions[7], NativeEnums.SCType.SC_DRAG_RESIZED }
        };

        private static Dictionary<ResizeDirection, Cursor> Direction_Cursor =
            new Dictionary<ResizeDirection, Cursor>()
        {
            { Directions[0], Cursors.SizeNWSE },
            { Directions[1], Cursors.SizeWE },
            { Directions[2], Cursors.SizeNESW },
            { Directions[3], Cursors.SizeNESW },
            { Directions[4], Cursors.SizeWE },
            { Directions[5], Cursors.SizeNWSE },
            { Directions[6], Cursors.SizeNS },
            { Directions[7], Cursors.SizeNS },
            { ResizeDirection.Center, Cursors.Default }
        };
        #endregion

        public int ResizeGrip { get; set; } = 4;
        public bool ResizeEnable { get; set; } = true;

        public ResizeWindow()
        {
            this.MouseDown += ResizeWindow_MouseDown;
            this.MouseMove += ResizeWindow_MouseMove;
        }

        protected override void OnLoad(EventArgs e)
        {
            SetWindowStyle();

            base.OnLoad(e);
        }

        private void SetWindowStyle()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            NativeEnums.WindowStyles wStyle = NativeMethods.GetWindowLong(this.Handle, NativeEnums.WindowLongFlags.GWL_STYLE);

            wStyle &= ~NativeEnums.WindowStyles.WS_CAPTION;
            wStyle &= ~NativeEnums.WindowStyles.WS_SIZEFRAME;

            NativeMethods.SetWindowLong(this.Handle, NativeEnums.WindowLongFlags.GWL_STYLE, wStyle);
        }

        private void ResizeWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (ResizeEnable)
            {
                this.Cursor = Direction_Cursor[GetResizeDirection(e)];
            }
        }

        private void ResizeWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Resizing(GetResizeDirection(e), e);
            }
        }

        public void DragMove(MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(this.Handle, (uint)NativeEnums.WM.NCLBUTTONDOWN, new IntPtr((int)NativeEnums.HitTestValues.HTCAPTION), IntPtr.Zero);
        }
        
        public void Resizing(ResizeDirection direction, MouseEventArgs e)
        {
            OnResizing(direction, e);
        }

        protected virtual void OnResizing(ResizeDirection direction, MouseEventArgs e)
        {
            if (ResizeEnable && Direction_Drag.ContainsKey(direction))
            {
                var v = Direction_Drag[direction];

                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(this.Handle, (uint)NativeEnums.WM.SYSCOMMAND, new IntPtr((uint)v), IntPtr.Zero);
            }
            else
            {
                DragMove(e);
            }
        }
        
        private ResizeDirection GetResizeDirection(MouseEventArgs mouse)
        {
            ResizeDirection r = ResizeDirection.Center;

            if (mouse.X <= ResizeGrip)
                r = ResizeDirection.Left;
            else if (mouse.X >= this.Width - ResizeGrip * 2)
                r = ResizeDirection.Right;

            if (mouse.Y <= ResizeGrip)
                r |= ResizeDirection.Top;
            else if (mouse.Y >= this.Height - ResizeGrip * 2)
                r |= ResizeDirection.Bottom;
            else
                r |= ResizeDirection.Center;

            return r;
        }
    }
}