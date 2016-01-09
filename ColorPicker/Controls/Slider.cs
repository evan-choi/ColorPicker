using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

/// <summary>
/// 
/// VB.NET -> C# 컨버터 사용
/// 
/// 아주 오래전에 만든거라 최적화따위 찾아볼 수 없음
/// 
/// </summary>
namespace ColorPicker.Controls
{
    public partial class Slider : UserControl
    {
        #region "색상"
        private static Color[] Color_Bar = {
        Color.FromArgb(170, 170, 170),
        Color.FromArgb(155, 155, 155),
        Color.FromArgb(140, 140, 140)

    };
        private static Color[] Color_Orange = {
        Color.FromArgb(255, 90, 0),
        Color.FromArgb(201, 80, 0),
        Color.FromArgb(181, 72, 0)

    };
        private static Color[] Color_Blue = {
        Color.FromArgb(0, 122, 204),
        Color.FromArgb(0, 122, 204),
        Color.FromArgb(0, 122, 204)

    };
        private static Color[] Color_Purple = {
        Color.FromArgb(132, 41, 154),
        Color.FromArgb(104, 33, 122),
        Color.FromArgb(89, 28, 104)

    };
        private static Color[] Color_Mint_Bar = {
        Color.FromArgb(6, 126, 105),
        Color.FromArgb(7, 133, 110),
        Color.FromArgb(7, 143, 117)
    };
        private static Color[] Color_Mint = {
        Color.FromArgb(6, 126, 105),
        Color.FromArgb(149, 234, 191),
        Color.FromArgb(99, 213, 155)
    };
        private static Color Color_Mint_Slider = Color.FromArgb(218, 251, 234);

        private static Color Color_Mint_Slider_Edge = Color.FromArgb(0, 179, 143);
        private static Color[] Color_Black = {
        Color.FromArgb(64, 64, 64),
        Color.FromArgb(46, 46, 46),
        Color.FromArgb(38, 38, 38)
		#endregion
	};

        #region "속성 변수"
        public enum ControlStyles
        {
            Black = 0,
            Orange = 1,
            Blue = 2,
            Purple = 3,
            Mint = 4
        }

        public enum SliderStyles
        {
            Rectangle = 0,
            Ellipse = 1,
            None = 2
        }

        public enum TrackBarStyles
        {
            Horizontal = 0,
            Vertical = 1
        }

        private int _Maximum = 10;
        private int _Minimum = 0;

        private int _Value = 5;
        private ControlStyles _selectedStyle = ControlStyles.Black;

        private Color[] selectedStyle = Color_Black;
        private SliderStyles _SliderStyle = SliderStyles.Rectangle;

        private Size SliderSize = new Size(8, 17);

        private TrackBarStyles _TrackBarStyle = TrackBarStyles.Horizontal;

        private bool _DropShadow = true;
        #endregion
        private int _WheelChange = 1;

        #region "이벤트 변수"
        public new event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender, int value);
        #endregion

        #region "액션 변수"
        private bool loaded;
        private bool Downed;
        private Point startPt;
        private Point orgPt;
        private int orgValue;
        private Thread scrollThread;
        #endregion
        private int __Value;

        private void JY_TrackBar_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            pSlider.Parent = board;
            GraphicUpdate(selectedStyle, _Value);
            loaded = true;
        }

        private void JY_TrackBar_Resize(object sender, EventArgs e)
        {
            if (loaded)
            {
                GraphicUpdate(selectedStyle, _Value);
            }
        }

        public int WheelChange
        {
            get { return _WheelChange; }
            set { _WheelChange = value; }
        }

        public int Value
        {
            get
            {
                if (_TrackBarStyle == TrackBarStyles.Vertical)
                {
                    return _Maximum - _Value;
                }
                else {
                    return _Value;
                }
            }
            set
            {
                if (_TrackBarStyle == TrackBarStyles.Vertical)
                {
                    value = _Maximum - value;
                }
                if (value < _Minimum)
                    value = _Minimum;
                if (value > _Maximum)
                    value = _Maximum;

                _Value = value;
                GraphicUpdate(selectedStyle, _Value);

                Scroll?.Invoke(this, Value);
            }
        }

        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < Minimum)
                    value = _Minimum;
                _Maximum = value;
            }
        }

        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value > _Maximum)
                    value = _Maximum;
                _Minimum = value;
            }
        }

        public ControlStyles ControlStyle
        {
            get { return _selectedStyle; }
            set
            {
                _selectedStyle = value;
                switch (_selectedStyle)
                {
                    case ControlStyles.Black:
                        selectedStyle = Color_Black;
                        break;
                    case ControlStyles.Blue:
                        selectedStyle = Color_Blue;
                        break;
                    case ControlStyles.Mint:
                        selectedStyle = Color_Mint;
                        break;
                    case ControlStyles.Orange:
                        selectedStyle = Color_Orange;
                        break;
                    case ControlStyles.Purple:
                        selectedStyle = Color_Purple;
                        break;
                }
                GraphicUpdate(selectedStyle, _Value);
            }
        }

        public SliderStyles SliderStyle
        {
            get { return _SliderStyle; }
            set
            {
                _SliderStyle = value;
                switch (_SliderStyle)
                {
                    case SliderStyles.Ellipse:
                        SliderSize = new Size(11, 11);
                        break;
                    case SliderStyles.Rectangle:
                        SliderSize = new Size(8, 17);
                        break;
                }
                GraphicUpdate(selectedStyle, _Value);
            }
        }

        public TrackBarStyles SlideStyle
        {
            get { return _TrackBarStyle; }
            set
            {
                Size orgSize = this.Size;
                TrackBarStyles orgStyle = _TrackBarStyle;
                _TrackBarStyle = value;

                if (orgStyle != _TrackBarStyle)
                {
                    switch (_TrackBarStyle)
                    {
                        case TrackBarStyles.Horizontal:
                            this.Height = 21;
                            if (orgSize.Height > orgSize.Width)
                            {
                                this.Width = orgSize.Height;
                            }
                            else {
                                this.Width = orgSize.Width;
                            }
                            break;
                        case TrackBarStyles.Vertical:
                            this.Width = 21;
                            if (orgSize.Height > orgSize.Width)
                            {
                                this.Height = orgSize.Height;
                            }
                            else {
                                this.Height = orgSize.Width;
                            }
                            break;
                    }

                    GraphicUpdate(selectedStyle, _Value);
                }
            }
        }

        public bool DropShadow
        {
            get { return _DropShadow; }
            set
            {
                _DropShadow = value;
                GraphicUpdate(selectedStyle, _Value);
            }
        }

        private void scrollThread_Proc()
        {
            while (Downed)
            {
                Point endPt = this.PointToClient(MousePosition);
                Point delta = endPt - (Size)startPt;
                Point loc = orgPt + new Size(delta.X, delta.Y);
                int bValue = __Value;

                if (loc.X < 0)
                    loc.X = 0;
                if (loc.X > this.Width - pSlider.Width)
                    loc.X = this.Width - pSlider.Width;
                if (loc.Y < 0)
                    loc.Y = 0;
                if (loc.Y > this.Height - pSlider.Height)
                    loc.Y = this.Height - pSlider.Height;

                if (_TrackBarStyle == TrackBarStyles.Horizontal)
                {
                    __Value = (int)(orgValue + delta.X / (float)(this.Width - SliderSize.Width) * (_Maximum - _Minimum));
                }
                else {
                    __Value = (int)(orgValue + delta.Y / (float)(this.Height - SliderSize.Height) * (_Maximum - _Minimum));
                }

                if (__Value < _Minimum)
                    __Value = _Minimum;
                if (__Value > _Maximum)
                    __Value = _Maximum;

                _Value = __Value;
                if (bValue != __Value)
                {
                    if (Scroll != null)
                    {
                        Scroll(this, Value);
                    }
                    GraphicUpdate(selectedStyle, _Value);
                }

                Thread.Sleep(5);
            }
        }

        private void wheel_MouseWheel(object sender, MouseEventArgs e)
        {

        }


        private void GraphicUpdate(Color[] theme, int value)
        {
            Bitmap sliderImg = new Bitmap(SliderSize.Width, SliderSize.Height);
            using (Graphics g = Graphics.FromImage(sliderImg))
            {
                Rectangle rect = new Rectangle(new Point(0, 0), sliderImg.Size - new Size(1, 1));
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.Clear(Color.Transparent);
                Color sliderColor = Color.White;
                Color sliderEdge = theme[1];
                if (_selectedStyle == ControlStyles.Mint)
                {
                    sliderColor = Color_Mint_Slider;
                    sliderEdge = Color_Mint_Slider_Edge;
                }
                if (_SliderStyle == SliderStyles.Ellipse)
                {
                    g.FillEllipse(new SolidBrush(sliderColor), rect);
                    g.DrawEllipse(new Pen(new SolidBrush(sliderEdge)), rect);

                }
                else if (_SliderStyle == SliderStyles.Rectangle)
                {
                    g.FillRectangle(new SolidBrush(sliderColor), rect);
                    g.DrawRectangle(new Pen(new SolidBrush(sliderEdge)), rect);

                }
                else {
                    g.FillRectangle(Brushes.Transparent, rect);
                }
            }

            Bitmap track = default(Bitmap);
            Point tPt = default(Point);
            if (_TrackBarStyle == TrackBarStyles.Horizontal)
            {
                track = new Bitmap(this.Width - sliderImg.Width - 4, 3);
                tPt = new Point((int)((float)(value - _Minimum) / (_Maximum - _Minimum) * track.Width), 0);
            }
            else {
                track = new Bitmap(3, this.Height - sliderImg.Height - 6);
                tPt = new Point(0, (int)((float)(value - _Minimum) / (float)(_Maximum - _Minimum) * track.Height));
            }

            using (Graphics g = Graphics.FromImage(track))
            {
                Color[] cBar = Color_Bar;
                if (_selectedStyle == ControlStyles.Mint)
                {
                    cBar = Color_Mint_Bar;
                }
                if (_TrackBarStyle == TrackBarStyles.Horizontal)
                {
                    Point pt1 = new Point(0, 0);
                    Point pt2 = new Point(this.Width, 0);

                    for (int y = 0; y <= theme.Length - 1; y++)
                    {
                        Point incPt = new Point(0, y);
                        g.DrawLine(new Pen(new SolidBrush(cBar[y])), pt1 + (Size)incPt, pt2 + (Size)incPt);
                        g.DrawLine(new Pen(new SolidBrush(theme[y])), pt1 + (Size)incPt, tPt + (Size)incPt);
                    }
                }
                else {
                    Point pt1 = new Point(0, this.Height);
                    Point pt2 = new Point(0, 0);

                    for (int x = 0; x <= theme.Length - 1; x++)
                    {
                        Point incPt = new Point(x, 0);
                        g.DrawLine(new Pen(new SolidBrush(cBar[x])), pt1 + (Size)incPt, pt2 + (Size)incPt);
                        g.DrawLine(new Pen(new SolidBrush(theme[x])), pt1 + (Size)incPt, tPt + (Size)incPt);
                    }
                }
            }

            Bitmap mImg = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(mImg))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Point trackPt = new Point(this.Width / 2 - track.Width / 2, this.Height / 2 - track.Height / 2);
                Point sliderPt = default(Point);
                if (_TrackBarStyle == TrackBarStyles.Horizontal)
                {
                    sliderPt = new Point(trackPt.X + tPt.X - sliderImg.Width / 2, trackPt.Y + track.Height / 2 - sliderImg.Height / 2);
                }
                else {
                    sliderPt = new Point(this.Width / 2 - sliderImg.Width / 2, trackPt.Y + tPt.Y - sliderImg.Height / 2);
                }

                if (_DropShadow)
                {
                    if (SliderStyle == SliderStyles.Ellipse)
                    {
                        //# Drop Shadow - Slider - Ellipse
                        g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(90, Color.Black))), new Rectangle(sliderPt + new Size(1, 1), SliderSize - new Size(1, 1)));
                        g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(45, Color.Black))), new Rectangle(sliderPt + new Size(1, 1), SliderSize));
                    }
                    else if (SliderStyle == SliderStyles.Rectangle)
                    {
                        //# Drop Shadow - Slider - Rectangle
                        g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(100, Color.Black))), new Rectangle(sliderPt + new Size(1, 1), SliderSize - new Size(1, 1)));
                        g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, Color.Black))), new Rectangle(sliderPt + new Size(1, 1), SliderSize));
                    }

                    //# Drop Shadow - Track
                    g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(100, Color.Black))), new Rectangle(trackPt + new Size(1, 1), track.Size - new Size(1, 1)));
                    g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, Color.Black))), new Rectangle(trackPt + new Size(1, 1), track.Size));
                }

                pSlider.SizeMode = PictureBoxSizeMode.AutoSize;
                pSlider.Location = sliderPt;

                g.DrawImage(track, trackPt);
            }

            //If slider.Image IsNot Nothing Then slider.Image.Dispose()
            //If board.Image IsNot Nothing Then board.Image.Dispose()

            pSlider.Image = sliderImg;
            board.Image = mImg;
        }
        public Slider()
        {
            InitializeComponent();

            Resize += JY_TrackBar_Resize;
            Load += JY_TrackBar_Load;
        }

        private void pSlider_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = this.PointToClient(MousePosition);

            if (!Downed)
            {
                wheel.Focus();
                startPt = pt;
                orgPt = pSlider.Location;
                orgValue = _Value;
                Downed = true;

                scrollThread = new Thread(scrollThread_Proc);
                scrollThread.IsBackground = true;
                scrollThread.SetApartmentState(ApartmentState.STA);
                scrollThread.Start();
            }
        }

        private void pSlider_MouseUp(object sender, MouseEventArgs e)
        {
            Downed = false;
            startPt = Point.Empty;
            _Value = __Value;
            GraphicUpdate(selectedStyle, _Value);
        }
    }
}
