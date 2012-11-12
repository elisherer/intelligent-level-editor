using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class ImageBox : Form
    {
        private Bitmap _bmp;

        private ImageBox()
        {
            InitializeComponent();
        }

        private void SetImage(Image image)
        {
            pictureBox.Image = image;
            _bmp = new Bitmap(image);
            pictureBox.Size = new Size(image.Width + 2,image.Height + 2);
        }

        public static DialogResult ShowDialog(Image image)
        {
            var imBox = new ImageBox();
            imBox.SetImage(image);
            return imBox.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = @"PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var imgFormat = ImageFormat.Png;
            switch (Path.GetExtension(sfd.FileName))
            {
                case ".jpg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    imgFormat = ImageFormat.Png;
                    break;
                case ".bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;
                case ".gif":
                    imgFormat = ImageFormat.Gif;
                    break;
            }
            pictureBox.Image.Save(sfd.FileName, imgFormat);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox.Image);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            btnZoomIn.Checked = !btnZoomIn.Checked;
            btnZoomIn.ToolTipText = (btnZoomIn.Checked ? @"Zoom Out" : @"Zoom In"); //caitsith2
            pictureBox.Width = pictureBox.Image.Width * (btnZoomIn.Checked ? 2 : 1);
            pictureBox.Height = pictureBox.Image.Height * (btnZoomIn.Checked ? 2 : 1);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X/(btnZoomIn.Checked ? 2 : 1);
            var y = e.Y/(btnZoomIn.Checked ? 2 : 1);
            if (!(x >= 0 && y >= 0 && x < _bmp.Width && y < _bmp.Height)) return;
            var clr = _bmp.GetPixel(x, y); //caitsith2
            lblColor.Text = string.Format("RGBA({0},{1},{2},{3})", clr.R, clr.G, clr.B, clr.A);
        }
    }
}
