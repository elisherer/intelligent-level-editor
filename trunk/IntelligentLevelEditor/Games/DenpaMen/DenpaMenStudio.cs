using System;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.DenpaMen
{
    public partial class DenpaMenStudio : UserControl, IStudio
    {
        private DenpaMen.DenapMenData _data;

        private readonly Color[][] _bodyColors = new [] {
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

        public DenpaMenStudio()
        {
            InitializeComponent();
        }

        public void NewData()
        {
            _data = new DenpaMen.DenapMenData {Name = new byte[24]};
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
            cmbColor.SelectedIndex = _data.Color;

            RenderDenpa();
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _data.Color = (byte)cmbColor.SelectedIndex;
            RenderDenpa();
        }

        private const int RendererBodyStartX = 0;
        private const int RendererBodyStartY = 100;

        private void RenderDenpa()
        {


            var bmp = new Bitmap(100, 167);
            //var g = Graphics.FromImage(bmp);
            var body = Resources.denpamen_body;
            
            //Draw Body
            var color1 = _bodyColors[_data.Color][0];
            var color2 = _bodyColors[_data.Color][1];

            for (var y = 0; y < body.Height; y++)
                for (var x = 0; x < body.Width; x++)
                {
                    var color = body.GetPixel(x, y);
                    if (color.ToArgb() == Color.Magenta.ToArgb())
                        bmp.SetPixel(RendererBodyStartX + x, RendererBodyStartY + y, color1);
                    else if (color.ToArgb() == Color.Lime.ToArgb())
                        bmp.SetPixel(RendererBodyStartX + x, RendererBodyStartY + y, color2);
                    else
                        bmp.SetPixel(RendererBodyStartX + x, RendererBodyStartY + y, color);
                }
            pnlDenpa.BackgroundImage = bmp;
        }

    }
}
