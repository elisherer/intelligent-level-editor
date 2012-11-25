using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.WebCam;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace IntelligentLevelEditor
{
    public partial class CameraCapture : Form
    {
        private QRCodeReader _reader = new QRCodeReader();
        public byte[] ByteArray;

        public CameraCapture()
        {
            InitializeComponent();
        }

        public static byte[] GetByteArray()
        {
            var capture = new CameraCapture();
            return capture.ShowDialog() == DialogResult.OK ? capture.ByteArray : null;
        }

        private void WebCamCaptureImageCaptured(object source, WebcamEventArgs e)
        {
            pictureCamera.Image = e.WebCamImage;

            try
            {
                var bmp = new Bitmap(pictureCamera.Image);
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));

                var result = _reader.decode(binary);

                ByteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                if (MessageBox.Show(@"Data captured, Do you want to return it?","Data Captured",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch// (ReaderException ex)
            {
                ByteArray = null;
            }
        }

        private void CameraCapture_Load(object sender, System.EventArgs e)
        {
            webcamCapture.CaptureWidth = pictureCamera.Width;
            webcamCapture.CaptureHeight = pictureCamera.Height;

            webcamCapture.TimeToCapture_milliseconds = 500;

            // Start the video capture. let the control handle the frame numbers.
            webcamCapture.Start(0);
        }

        private void CameraCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            webcamCapture.Stop();
        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
        {
            if (webcamCapture.IsRunning())
            {
                webcamCapture.Stop();
                btnStartStop.Text = @"Start";
            }
            else
            {
                webcamCapture.Start(0);
                btnStartStop.Text = @"Stop";
            }
        }
    }
}
