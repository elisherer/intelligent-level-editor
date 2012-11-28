using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Pyramids
{
    public partial class PyramidsGridControl : PictureBox
    {
        private const int NumberOfObjects = 0x34;

        private Pyramids _levelData;
        private bool _grid = true;
        private readonly Image[] _images = new Image[NumberOfObjects];

        public delegate void GridCellHandler(int x, int y);

        [Category("Action")]
        [Description("Fires when a certain cell being clicked.")]
        public event GridCellHandler GridCellClick;

        [Category("Action")]
        [Description("Fires when a certain cell being hovered on.")]
        public event GridCellHandler GridCellHover;

        [Category("Action")]
        [Description("Fires when a certain cell being hovered while mouse held down.")]
        public event GridCellHandler GridCellHoverDown;

        [Category("Grid")]
        [Description("Number of cells in a row.")]
        public int Columns { set; get; }
        
        [Category("Grid")]
        [Description("Number of cells in a column.")]
        public int Rows { set; get; }

        public PyramidsGridControl()
        {
            InitializeComponent();
            InitImages();
        }

        private void InitImages()
        {
            for (byte i = 0; i < NumberOfObjects; i++)
            {
                var byteStr = i.ToString("X2");
                var img = Resources.ResourceManager.GetObject("pyramids_sprite_" + byteStr);
                if (img == null) continue;
                _images[i] = (Image)img;
            }
        }

        public void SetData(Pyramids data)
        {
            _levelData = data;
        }

        public void SetGrid(bool grid)
        {
            _grid = grid;
        }

        private void DrawToImage(Image dstImage, int width, int height, bool gridLines, bool drawTrans)
        {
            var pixWidth = width / Columns;
            var pixHeight = height / Rows;
            var g = Graphics.FromImage(dstImage);
            g.Clear(Color.Transparent);
            if (_levelData != null)
            {
                for (var y = 0; y < Rows; y++)
                    for (var x = 0; x < Columns; x++)
                        if (_levelData.Get(x, y) > 0)
                            g.DrawImage(_images[_levelData.Get(x, y)], x * pixWidth, y * pixHeight, pixWidth + 1, pixHeight + 1);
                if (gridLines)
                {
                    for (var i = 0; i <= Columns; i++) //vertical lines
                        g.DrawLine(Pens.Black, i * pixWidth, 0, i * pixWidth, pixHeight * Rows);
                    for (var i = 0; i <= Rows; i++) //horizontal lines
                        g.DrawLine(Pens.Black, 0, i * pixHeight, pixWidth * Columns, i * pixHeight);
                }
            }
            g.Dispose();
        }

        public void Redraw()
        {
            if (Image == null || Height != Image.Height || Width != Image.Width)
                Image = new Bitmap(Width, Height);
            DrawToImage(Image,Width, Height, _grid, true);
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (Image != null && Height != Image.Height && Width != Image.Width)
                Redraw();
        }

        private bool _mouseDown;

        private void CellClicked(object sender, MouseEventArgs e)
        {
            if (GridCellClick == null) return;
            var x = e.X / (Width / Columns);
            var y = e.Y / (Height / Rows);
            if (x >= 0 && y >= 0 && x < Columns && y < Rows)
                GridCellClick(x, y);
        }

        private void CellHovered(object sender, MouseEventArgs e)
        {
            if (GridCellHover == null && GridCellHoverDown == null) return;
            var x = e.X / (Width / Columns);
            var y = e.Y / (Height / Rows);
            if (x < 0 || y < 0 || x >= Columns || y >= Rows) return;
            if (GridCellHover != null)
                GridCellHover(x, y);
            if (e.Button == MouseButtons.Left && _mouseDown && GridCellHoverDown != null)
                GridCellHoverDown(x, y);
        }

        private void PyramidsGridControl_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
        }

        private void PyramidsGridControl_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }
    }
}
