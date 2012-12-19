using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.DenpaMen
{
    public partial class DenpaMenStudio : UserControl, IStudio
    {
        private DenpaMen.DenapMenData _data;

        private static readonly Color[][] BodyColors = new [] {
            new [] { Color.Black, Color.Black },
            new [] { Color.White, Color.White },
            new [] { Color.Red, Color.Red },
            new [] { Color.Blue, Color.Blue},
            new [] { Color.DeepSkyBlue, Color.DeepSkyBlue},
            new [] { Color.Orange, Color.Orange},
            new [] { Color.Lime, Color.Lime},
            new [] { Color.Red, Color.Blue},
            new [] { Color.Red, Color.DeepSkyBlue},
            new [] { Color.Red, Color.Orange},
            new [] { Color.Red, Color.Lime},
            new [] { Color.Red, Color.White},
            new [] { Color.Blue, Color.DeepSkyBlue},
            new [] { Color.Blue, Color.Orange},
            new [] { Color.Blue, Color.Lime},
            new [] { Color.Blue, Color.White},
            new [] { Color.DeepSkyBlue, Color.Orange},
            new [] { Color.DeepSkyBlue, Color.Lime},
            new [] { Color.DeepSkyBlue, Color.White},
            new [] { Color.Orange, Color.Lime},
            new [] { Color.Orange, Color.White},
            new [] { Color.Lime, Color.White}
        };

        private static readonly Bitmap[] HeadShapeBitmaps = {
            Resources.denpamen_head_00, Resources.denpamen_head_01,
            Resources.denpamen_head_02, Resources.denpamen_head_03,
            Resources.denpamen_head_04, Resources.denpamen_head_05,
            Resources.denpamen_head_06, Resources.denpamen_head_07,
            Resources.denpamen_head_08, Resources.denpamen_head_09,
            Resources.denpamen_head_0A, Resources.denpamen_head_10,
            Resources.denpamen_head_11, Resources.denpamen_head_12,
            Resources.denpamen_head_13, Resources.denpamen_head_14,
            Resources.denpamen_head_15, Resources.denpamen_head_16,
            Resources.denpamen_head_20, Resources.denpamen_head_21,
            Resources.denpamen_head_22, Resources.denpamen_head_23,
            Resources.denpamen_head_24
        };

        public DenpaMenStudio()
        {
            InitializeComponent();

            //Initialization for DenpaMen Studio Controls
            var headShapesImageList = new ImageList {ImageSize = new Size(48, 48)};
            headShapesImageList.Images.AddRange(HeadShapeBitmaps);
            lvwHeadShape.LargeImageList = headShapesImageList;
            for (var i = 0; i < headShapesImageList.Images.Count ; i++)
                lvwHeadShape.Items.Add(i.ToString(), i);

            var bodyColorsImageList = new ImageList { ImageSize = new Size(32, 32) };
            foreach (var bodyColor in BodyColors)
            {
                var bodyColorBitmap = new Bitmap(32, 32);
                var g = Graphics.FromImage(bodyColorBitmap);
                g.FillRectangle(new SolidBrush(bodyColor[0]), 0 ,0 ,bodyColorBitmap.Width / 2,bodyColorBitmap.Height);
                g.FillRectangle(new SolidBrush(bodyColor[1]), bodyColorBitmap.Width / 2, 0, bodyColorBitmap.Width / 2, bodyColorBitmap.Height);
                g.DrawRectangle(Pens.Black, 0, 0, bodyColorBitmap.Width-1, bodyColorBitmap.Height-1);
                g.Dispose();
                bodyColorsImageList.Images.Add(bodyColorBitmap);
            }
            lvwBodyColors.LargeImageList = bodyColorsImageList;
            for (var i = 0; i < BodyColors.Length; i++)
                lvwBodyColors.Items.Add(i.ToString(), i);
        }

        public void NewData()
        {
            _data = new DenpaMen.DenapMenData
                        {
                            Region = DenpaMen.RegionUs, 
                            Color = 4, 
                            Name = "No Name"
                        };
            DataToGui();
        }

        public void LoadData(byte[] data)
        {
            return;
            //DataToGui();
        }

        public byte[] SaveData()
        {
            return null;
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            return null;
        }

        private void DataToGui()
        {
            lvwHeadShape.Items[_data.HeadShape].Selected = true;
            lvwBodyColors.Items[_data.Color].Selected = true;
            txtName.Text = _data.Name;
            if (_data.Region == DenpaMen.RegionUs)
                radioRegionUS.Checked = true;
            else if (_data.Region == DenpaMen.RegionJp)
                radioRegionJP.Checked = true;
            else
                radioRegionEU.Checked = true;
            RenderDenpa();
        }

        private void lvwHeadShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwHeadShape.SelectedIndices.Count == 0) return;
            _data.HeadShape = (byte)lvwHeadShape.SelectedIndices[0];
            RenderDenpa();
        }

        
        private void lvwBodyColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwBodyColors.SelectedIndices.Count == 0) return;
            _data.Color = (byte)lvwBodyColors.SelectedIndices[0];
            RenderDenpa();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _data.Name = txtName.Text;
        }

        private const int RendererBodyStartX = 0;
        private const int RendererBodyStartY = 112;
        private const int RendererHeadStartX = 2;
        private const int RendererHeadStartY = 24;

        private static Bitmap ColorBitmapWith(Bitmap src, Color color1, Color color2)
        {
            var coloredBitmap = (Bitmap)src.Clone();
            for (var y = 0; y < src.Height; y++)
                for (var x = 0; x < src.Width; x++)
                {
                    var color = src.GetPixel(x, y);
                    if (color.ToArgb() == Color.Magenta.ToArgb())
                        coloredBitmap.SetPixel(x, y, color1);
                    else if (color.ToArgb() == Color.Lime.ToArgb())
                        coloredBitmap.SetPixel(x, y, color2);
                }
            return coloredBitmap;
        }

        private void RenderDenpa()
        {
            var bmp = new Bitmap(102, 180);
            var g = Graphics.FromImage(bmp);
            var top = new Point(0, 0);
            var bottom = new Point(0, 180);

            var color1 = BodyColors[_data.Color][0];
            var color2 = BodyColors[_data.Color][1];

            g.FillRectangle(new LinearGradientBrush(top,bottom,Color.White,Color.Wheat),0,0,bmp.Width,bmp.Height);

            //Draw Body
            var body = ColorBitmapWith(Resources.denpamen_body, color1, color2);
            g.DrawImage(body, RendererBodyStartX, RendererBodyStartY, body.Width, body.Height);

            //Draw Head
            var head = ColorBitmapWith(HeadShapeBitmaps[_data.HeadShape], color1, color2);
            g.DrawImage(head, RendererHeadStartX, RendererHeadStartY, head.Width, head.Height);

            g.Dispose();
            pnlDenpa.BackgroundImage = bmp;
        }


    }
}
