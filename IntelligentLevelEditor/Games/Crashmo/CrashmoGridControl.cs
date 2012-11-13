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
            public readonly byte Type;
            public byte Flags;

            public CrashmoFixedPosition(byte x, byte y, byte type, byte flags)
            {
                X = x;
                Y = y;
                Type = type;
                Flags = flags;
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
        private readonly bool[][] _cloudy;
        public CrashmoFixedPosition Flag;
        public CrashmoFixedPosition[] Manholes;
        public CrashmoFixedPosition[] Switches;
        public CrashmoFixedPosition[] Doors;
        public CrashmoFixedPosition[] Clouds;
        private bool _grid = true;
        private readonly Bitmap _thumb = new Bitmap(Crashmo.BitmapSize, Crashmo.BitmapSize);

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

            _cloudy = new bool[Crashmo.BitmapSize][];
            for (var i =0; i< _cloudy.Length; i++)
                _cloudy[i] = new bool[Crashmo.BitmapSize];
        }

        public void SetData(Crashmo.CrashmoQrData cData)
        {
            Bitmap = Crashmo.Decode(cData.LevelData);
            Palette = cData.PaletteData;
            Switches = new CrashmoFixedPosition[4];
            Manholes = new CrashmoFixedPosition[6];
            Doors = new CrashmoFixedPosition[6];
            Clouds = new CrashmoFixedPosition[5];
            for (var i = 0; i < cData.Utilities.Length; i++ )
            {
                switch (cData.Utilities[i].Type)
                {
                    case 1:
                        Flag = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                    case 2:
                        var mhidx = cData.Utilities[i].Flags*2;
                        if (Manholes[mhidx] != null) //written the first one already
                            mhidx++;
                        Manholes[mhidx] = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                    case 3:
                        var swidx = cData.Utilities[i].Flags == 4 ? 3 : cData.Utilities[i].Flags;
                        Switches[swidx] = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                    case 4:
                        var dridx = cData.Utilities[i].Flags*2;
                        if (Doors[dridx] != null) //written the first one already
                            dridx++;
                        Doors[dridx] = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                    case 5:
                        var clidx = 0;
                        while (Clouds[clidx] != null && clidx < 5) clidx++;
                        if (clidx == 5) clidx = 4; //overwrite the last one, clearly an error
                        Clouds[clidx] = new CrashmoFixedPosition(cData.Utilities[i]);
                        break;
                }
            }
        }

        public void SetGrid(bool grid)
        {
            _grid = grid;
        }
        
        private void DrawManhole(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos == null) return;
            Image toDraw = Resources.sprite_ladder_red; //if it's 0
            var flipped = pos.Y > 0 && Bitmap[pos.Y - 1][pos.X] == Bitmap[pos.Y][pos.X];
            switch (pos.Flags)
            {
                case 1:
                    toDraw = Resources.sprite_ladder_yellow;
                    break;
                case 2:
                    toDraw = Resources.sprite_ladder_blue;
                    break;
            }
            g.DrawImage(toDraw, 1 + pos.X * pixWidth, 1 + pos.Y * pixHeight + (flipped ? pixHeight - 2 : 0), pixWidth - 2, flipped ? -pixHeight : pixHeight);
        }

        private void DrawDoor(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos == null) return;
            Image toDraw = Resources.sprite_door_red; //if it's 0
            switch (pos.Flags)
            {
                case 1:
                    toDraw = Resources.sprite_door_yellow;
                    break;
                case 2:
                    toDraw = Resources.sprite_door_blue;
                    break;
            }
            g.DrawImage(toDraw, 1 + pos.X * pixWidth, 1 + pos.Y * pixHeight , pixWidth - 2, pixHeight);
        }

        private void DrawSwitch(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos == null) return; //TODO: new sprites for angles
            g.DrawImage(Resources.sprite_switch_trans, pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
        }

        private void RecursiveDrawCloud(Graphics g, int x, int y, byte baseColor, int pixWidth, int pixHeight)
        {
            _cloudy[y][x] = true; //mark
            g.FillRectangle(Brushes.White, (x + 0.3f) * pixWidth, (y + 0.3f) * pixHeight, pixWidth*0.3f, pixHeight*0.3f);
            // <            
            if (x > 0 && Bitmap[y][x - 1] == baseColor && !_cloudy[y][x - 1])
                RecursiveDrawCloud(g, x - 1, y, baseColor, pixWidth, pixHeight);
            // >
            if (x < Bitmap[0].Length - 1 && Bitmap[y][x + 1] == baseColor && !_cloudy[y][x + 1])
                RecursiveDrawCloud(g, x + 1, y, baseColor, pixWidth, pixHeight);
            // ^
            if (y > 0 && Bitmap[y - 1][x] == baseColor && !_cloudy[y - 1][x])
                RecursiveDrawCloud(g, x, y - 1, baseColor, pixWidth, pixHeight);
            // v
            if (y < Bitmap.Length - 1 && Bitmap[y + 1][x] == baseColor && !_cloudy[y + 1][x])
                RecursiveDrawCloud(g, x, y + 1, baseColor, pixWidth, pixHeight);
        }

        private void DrawCloud(Graphics g, CrashmoFixedPosition pos, int pixWidth, int pixHeight)
        {
            if (pos == null) return;

            if (Bitmap[pos.Y][pos.X] > 0) //not transparent
            {
                RecursiveDrawCloud(g, pos.X, pos.Y, Bitmap[pos.Y][pos.X], pixWidth, pixHeight);
                g.DrawImage(Resources.sprite_cloud, pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
            }
            else
                g.DrawImage(Resources.sprite_cloud, pos.X * pixWidth, pos.Y * pixHeight, pixWidth, pixHeight);
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
                foreach (var cl in _cloudy)
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
                if (Flag != null)
                    g.DrawImage(Resources.sprite_flag, Flag.X * pixWidth, Flag.Y * pixHeight, pixWidth, pixHeight);
            }
            g.Dispose();
        }

        public void Redraw()
        {
            if (DesignMode) return;
            if (Height != Image.Height || Width != Image.Width)
                Image = new Bitmap(Width, Height);
            DrawCrashmoToImage(Image, Width, Height, _grid, true);
            Invalidate();
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
            if (Image != null && Height != Image.Height && Width != Image.Width)
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
            if (x < 0 || y < 0 || x >= Crashmo.BitmapSize || y >= Crashmo.BitmapSize) return;
            GridCellHover(x, y);
            if (e.Button == MouseButtons.Left)
                GridCellHoverDown(x, y);
        }
    }
}
