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

            public CrashmoFixedPosition()
            {
                
            }

            public CrashmoFixedPosition(byte x, byte y)
            {
                X = x;
                Y = y;
            }

            public CrashmoFixedPosition(Crashmo.CrashmoPosition src)
            {
                X = (byte) (src.Xy & 0x1F);
                Y = (byte)(((~src.Xy) >> 5) & 0x1F);
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
        public bool[][] Cloudy;
        public CrashmoFixedPosition Flag = new CrashmoFixedPosition {Type = 1};
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

            Cloudy = new bool[Crashmo.BitmapSize][];
            for (var i =0; i< Cloudy.Length; i++)
                Cloudy[i] = new bool[Crashmo.BitmapSize];
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
        
        private void DrawManhole(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {

            if (pos.X == 0xFF) return;
            Image toDraw = Resources.ladder_red; //if it's 0
            var flipped = pos.Y > 0 && Bitmap[pos.Y - 1][pos.X] == Bitmap[pos.Y][pos.X];
            //pos.Flags = (byte)((pos.Flags & 0xF0) + 1 * (flipped ? 1 : 0));
            switch (pos.Flags)
            {
                case 1:
                    toDraw = Resources.ladder_yellow;
                    break;
                case 2:
                    toDraw = Resources.ladder_blue;
                    break;
                case 3:
                    toDraw = Resources.ladder_green;
                    break;
                case 4:
                    toDraw = Resources.ladder_purple;
                    break;
            }
            g.DrawImage(toDraw, 1 + pos.X * pixWidth, 1 + pos.Y * pixHeight + (flipped ? pixHeight - 2 : 0), pixWidth - 2, flipped ? -pixHeight : pixHeight);
        }

        private void DrawDoor(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos.X == 0xFF) return;
            Image toDraw = Resources.sprite_door_red; //if it's 0
            switch (pos.Flags)
            {
                case 1:
                    toDraw = Resources.sprite_door_yellow;
                    break;
                case 2:
                    toDraw = Resources.sprite_door_blue;
                    break;
                case 3:
                    toDraw = Resources.sprite_door_green;
                    break;
                case 4:
                    toDraw = Resources.sprite_door_purple;
                    break;
            }
            g.DrawImage(toDraw, 1 + pos.X * pixWidth, 1 + pos.Y * pixHeight , pixWidth - 2, pixHeight);
        }

        private void DrawSwitch(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos.X == 0xFF) return;
            g.DrawImage(SwitchBitmaps[pos.Flags >> 4], pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
        }

        private void RecursiveDrawCloud(Graphics g, CrashmoFixedPosition pos, byte baseColor, int pixWidth, int pixHeight)
        {
            Cloudy[pos.Y][pos.X] = true; //mark
            g.FillRectangle(Brushes.White, (pos.X + 0.3f) * pixWidth, (pos.Y + 0.3f) * pixHeight, pixWidth*0.3f, pixHeight*0.3f);
            // <            
            if (pos.X > 0 && Bitmap[pos.Y][pos.X - 1] == baseColor && !Cloudy[pos.Y][pos.X - 1])
                RecursiveDrawCloud(g, new CrashmoFixedPosition((byte)(pos.X - 1), pos.Y), baseColor, pixWidth, pixHeight);
            // >
            if (pos.X < Bitmap[0].Length - 1 && Bitmap[pos.Y][pos.X + 1] == baseColor && !Cloudy[pos.Y][pos.X + 1])
                RecursiveDrawCloud(g, new CrashmoFixedPosition((byte)(pos.X + 1), pos.Y), baseColor, pixWidth, pixHeight);
            // ^
            if (pos.Y > 0 && Bitmap[pos.Y - 1][pos.X] == baseColor && !Cloudy[pos.Y - 1][pos.X])
                RecursiveDrawCloud(g, new CrashmoFixedPosition(pos.X, (byte)(pos.Y - 1)), baseColor, pixWidth, pixHeight);
            // v
            if (pos.Y < Bitmap.Length - 1 && Bitmap[pos.Y + 1][pos.X] == baseColor && !Cloudy[pos.Y + 1][pos.X])
                RecursiveDrawCloud(g, new CrashmoFixedPosition(pos.X, (byte)(pos.Y + 1)), baseColor, pixWidth, pixHeight);
        }

        private void DrawCloud(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos.X == 0xFF) return;

            if (Bitmap[pos.Y][pos.X] > 0) //not transparent
            {
                RecursiveDrawCloud(g, pos, Bitmap[pos.Y][pos.X], pixWidth, pixHeight);
                g.DrawImage(Resources.cloud_small, pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
            }
            else
                g.DrawImage(Resources.cloud_small, pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
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
                        if (Bitmap[y][x] > 0)
                            g.FillRectangle(brushes[Bitmap[y][x]-1], x * pixWidth, y * pixHeight, pixWidth, pixHeight);
                if (gridLines)
                {
                    for (var i = 0; i <= Crashmo.BitmapSize; i++) //vertical lines
                        g.DrawLine(Pens.DimGray, i * pixWidth, 0, i * pixWidth, pixHeight * Crashmo.BitmapSize);
                    for (var i = 0; i <= Crashmo.BitmapSize; i++) //horizontal lines
                        g.DrawLine(Pens.DimGray, 0, i * pixHeight, pixWidth * Crashmo.BitmapSize, i * pixHeight);
                }
                //reset clouds
                foreach (var cl in Cloudy)
                    for (var x = 0; x < cl.Length; x++)
                        cl[x] = false;
                foreach (var cloud in Clouds)
                    DrawCloud(g, cloud, pixWidth, pixHeight);
                foreach (var manhole in Manholes)
                    DrawManhole(g, manhole, pixWidth, pixHeight);
                foreach (var door in Doors)
                    DrawDoor(g, door, pixWidth, pixHeight);
                foreach (var swtch in Switches)
                    DrawSwitch(g, swtch, pixWidth, pixHeight);
                if (Flag.Type != (byte)Crashmo.PosType.Flag)
                    g.DrawImage(Resources.flag_icon, Flag.X * pixWidth, Flag.Y * pixHeight, pixWidth, pixHeight);
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
                    var clr = Bitmap[y][x] > 0
                                  ? Crashmo.CrashmoColorPalette.Entries[Palette[Bitmap[y][x]-1]]
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
