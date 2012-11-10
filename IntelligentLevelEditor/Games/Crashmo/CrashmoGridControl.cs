using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Crashmo
{
    public partial class CrashmoGridControl : PictureBox
    {
        public class CrashmoFixedPosition
        {
            public byte X;
            public byte Y;
            public byte Type;
            public byte Flags;

            public CrashmoFixedPosition(Crashmo.CrashmoPosition src)
            {
                X = (byte) (src.Xy & 0x1F);
                Y = (byte) ~( (src.Xy & 0x3E) >> 5 );
                Type = src.Type;
                Flags = src.Flags;
            }

            public Crashmo.CrashmoPosition UnFix()
            {
                Crashmo.CrashmoPosition dst;
                dst.Flags = Flags;
                dst.Type = Type;
                dst.Xy = (ushort) (X + ((~Y & 0x1F) << 5));
                return dst;
            }
        }

        public byte[] Palette;
        public byte[][] Bitmap;
        public CrashmoFixedPosition Flag;
        public List<CrashmoFixedPosition> Manholes;
        public List<CrashmoFixedPosition> Switches;
        public List<CrashmoFixedPosition> Doors;
        public List<CrashmoFixedPosition> Clouds;
        private bool _grid = true;
        private readonly Bitmap _thumb = new Bitmap(Crashmo.BitmapSize, Crashmo.BitmapSize);
        public readonly Bitmap[] SwitchBitmaps = new Bitmap[10];

        public delegate void GridCellHandler(int x, int y);

        [Category("Action")]
        [Description("Fires when a certain pixel being clicked.")]
        public event GridCellHandler GridCellClick;

        [Category("Action")]
        [Description("Fires when a certain pixel being hovered on.")]
        public event GridCellHandler GridCellHover;

        [Category("Action")]
        [Description("Fires when a certain pixel being hovered while mouse held down.")]
        public event GridCellHandler GridCellHoverDown;

        public CrashmoGridControl()
        {
            InitializeComponent();
        }

        public void SetData(byte[][] data, Crashmo.CrashmoQrData cData)
        {
            Bitmap = data;
            Palette = cData.PaletteData;
            Manholes = new List<CrashmoFixedPosition>();
            Switches = new List<CrashmoFixedPosition>();
            Doors = new List<CrashmoFixedPosition>();
            Clouds = new List<CrashmoFixedPosition>();
            for (var i = 0; i < cData.Utilities.Length; i++ )
            {
                switch (cData.Utilities[i].Type)
                {
                    case 1:
                        Flag = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                    case 2:
                        Manholes.Add(new CrashmoFixedPosition(cData.Utilities[i]));
                        break;
                    case 3:
                        Switches.Add(new CrashmoFixedPosition(cData.Utilities[i]));
                        break;
                    case 4:
                        Doors.Add(new CrashmoFixedPosition(cData.Utilities[i]));
                        break;
                    case 5:
                        Clouds.Add(new CrashmoFixedPosition(cData.Utilities[i]));
                        break;
                }
            }
            InitializeSwitches();
        }

        public void SetGrid(bool grid)
        {
            _grid = grid;
        }

        public void InitializeSwitches()
        {
            var srcColor = Color.FromArgb(255, 255, 0, 255);
            for (var i = 0; i < SwitchBitmaps.Length; i++)
            {
                var bmp = new Bitmap(11, 11);
                var clr = Crashmo.CrashmoColorPalette.Entries[Palette[i]];
                var g = Graphics.FromImage(bmp);
                g.DrawImage(Resources.switch_trans,0,0);
                g.Dispose();
                for (var y=0; y<11; y++)
                    for (var x=0;x<11;x++)
                        if (bmp.GetPixel(x,y).Equals(srcColor))
                            bmp.SetPixel(x,y,clr);
                SwitchBitmaps[i] = bmp;
            }
        }
        
        private void DrawManhole(Graphics g, int i, int pixWidth, int pixHeight)
        {

            if (Manholes[i].X == 0xFF) return;
            Image toDraw = Resources.ladder_red; //if it's 0
            var flipped = Manholes[i].Y > 0 && Bitmap[Manholes[i].Y - 1][Manholes[i].X] != 0xa && Bitmap[Manholes[i].Y][Manholes[i].X] != 0xa;
            Manholes[i].Flags = (byte)((Manholes[i].Flags & 0xF0) + 1 * (flipped ? 1 : 0));
            switch (Manholes[i].Flags >> 4)
            {
                case 1:
                    toDraw = Resources.ladder_blue;
                    break;
                case 2:
                    toDraw = Resources.ladder_yellow;
                    break;
                case 3:
                    toDraw = Resources.ladder_green;
                    break;
                case 4:
                    toDraw = Resources.ladder_purple;
                    break;
            }
            g.DrawImage(toDraw, 1 + Manholes[i].X * pixWidth, 1 + Manholes[i].Y * pixHeight + (flipped ? pixHeight - 2 : 0), pixWidth - 2, flipped ? -pixHeight : pixHeight);
        }

        private void DrawSwitch(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos.X == 0xFF) return;
            g.DrawImage(SwitchBitmaps[pos.Flags >> 4], pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
        }

        private void RecursiveFloodFill(int x, int y, byte baseColor, byte withColor)
        {
            
            Bitmap[y][x] = withColor;
            if (x > 0 && Bitmap[y][x - 1] == baseColor)
                RecursiveFloodFill(x-1, y, baseColor,withColor);
            if (x + 1 < Crashmo.BitmapSize && Bitmap[y][x + 1] == baseColor)
                RecursiveFloodFill(x+1, y, baseColor,withColor);
            if (y + 1 < Crashmo.BitmapSize && Bitmap[y + 1][x] == baseColor)
                RecursiveFloodFill(x, y+1, baseColor, withColor);
            if (y > 0 && Bitmap[y - 1][x] == baseColor)
                RecursiveFloodFill(x, y-1, baseColor, withColor);
        }

        public void FloodFill(int x, int y, byte color)
        {
            if (Bitmap[y][x] == color) return;
            RecursiveFloodFill(x,y,Bitmap[y][x],color);
        }

        public void DrawCrashmoToImage(Image dstImage, int width, int height, bool gridLines, bool drawTrans)
        {
            var pixWidth = width / Crashmo.BitmapSize;
            var pixHeight = height / Crashmo.BitmapSize;
            var g = Graphics.FromImage(dstImage);
            g.Clear(Color.Transparent);
            var brushes = new Brush[Palette.Length];
            for (var i = 0; i < brushes.Length; i++)
                brushes[i] = new SolidBrush(Crashmo.CrashmoColorPalette.Entries[Palette[i]]);
            var transBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
            if (Bitmap != null)
            {
                if (drawTrans)
                    g.FillRectangle(transBrush, 0, 0, pixWidth * Crashmo.BitmapSize, pixHeight * Crashmo.BitmapSize);
                for (var y = 0; y < Bitmap.Length; y++)
                    for (var x = 0; x < Bitmap[y].Length; x++)
                        if (Bitmap[y][x] != 0x0A)
                            g.FillRectangle(brushes[Bitmap[y][x]], x * pixWidth, y * pixHeight, pixWidth, pixHeight);
                if (gridLines)
                {
                    for (var i = 0; i <= Crashmo.BitmapSize; i++) //vertical lines
                        g.DrawLine(Pens.DimGray, i * pixWidth, 0, i * pixWidth, pixHeight * Crashmo.BitmapSize);
                    for (var i = 0; i <= Crashmo.BitmapSize; i++) //horizontal lines
                        g.DrawLine(Pens.DimGray, 0, i * pixHeight, pixWidth * Crashmo.BitmapSize, i * pixHeight);
                }
                if (Flag.X != 0xFF)
                    g.DrawImage(Resources.flag_icon, Flag.X * pixWidth, Flag.Y * pixHeight, pixWidth, pixHeight);
                for (var i = 0; i < 10; i++)
                {
                    DrawManhole(g, i, pixWidth, pixHeight);
                    DrawSwitch(g, Switches[i], pixWidth, pixHeight);
                    //TODO: crashmo_gridcontrol: add clouds & doors

                }
            }
            g.Dispose();
        }

        public void Redraw()
        {
            if (!DesignMode)
            {
                if (Height != Image.Height || Width != Image.Width)
                    Image = new Bitmap(Width, Height);
                DrawCrashmoToImage(Image, Width, Height, _grid, true);
                Invalidate();
            }
        }

        public Bitmap CreatePreview(int width, int height)
        {
            var bmp = new Bitmap(width, height);
            DrawCrashmoToImage(bmp,width,height,false,false);
            return bmp;
        }

        public Bitmap GetThumb()
        {
            for (var y = 0; y < Bitmap.Length; y++)
                for (var x = 0; x < Bitmap[y].Length; x++)
                {
                    var clr = Bitmap[y][x] != 0x0A
                                  ? Crashmo.CrashmoColorPalette.Entries[Palette[Bitmap[y][x]]]
                                  : Color.Transparent;
                    _thumb.SetPixel(x,y,clr);
                }
            return _thumb;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;
            if (Height != Image.Height && Width != Image.Width)
                Redraw();
        }

        private void CellClicked(object sender, MouseEventArgs e)
        {
            if (GridCellClick == null) return;
            var x = e.X / (Width / Crashmo.BitmapSize);
            var y = e.Y / (Height / Crashmo.BitmapSize);
            if (x >= 0 && y >= 0 && x < Crashmo.BitmapSize && y < Crashmo.BitmapSize)
                GridCellClick(x, y);
        }

        private void CellHovered(object sender, MouseEventArgs e)
        {
            if (GridCellHover == null || GridCellHoverDown == null) return;
            var x = e.X / (Width / Crashmo.BitmapSize);
            var y = e.Y / (Height / Crashmo.BitmapSize);
            if (x >= 0 && y >= 0 && x < Crashmo.BitmapSize && y < Crashmo.BitmapSize)
            {
                GridCellHover(x, y);
                if (e.Button == MouseButtons.Left)
                    GridCellHoverDown(x, y);
            }
        
        }
    }
}
