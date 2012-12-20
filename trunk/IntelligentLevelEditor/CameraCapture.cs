using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.Capture;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace IntelligentLevelEditor
{
    public partial class CameraCapture : Form
    {
        private QRCodeReader _reader = new QRCodeReader();
        private Capture.CaptureDevice _camera;
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

        private void OnRecievedFrame(Bitmap bmp)
        {
            try
            {
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));

                var result = _reader.decode(binary);

                if (((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS]) != null)
                    ByteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                else
                    ByteArray = System.Text.Encoding.ASCII.GetBytes(result.Text);

                if (MessageBox.Show(@"Data captured, Do you want to return it?", "Data Captured", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
            _camera = new CaptureDevice(pictureCamera.Handle, pictureCamera.Width, pictureCamera.Height, 50, OnRecievedFrame);
            _camera.Start();
        }

        private void CameraCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            _camera.Stop();
        }

        private void menuDeviceStart_Click(object sender, System.EventArgs e)
        {
            _camera.Start();
            if (_camera.IsRunning())
            {
                menuDeviceStart.Enabled = false;
                menuDeviceStop.Enabled = true;

                var cap = _camera.GetCapabilities();
                menuVideoCompression.Enabled = true;
                menuVideoDisplay.Enabled = cap.fHasDlgVideoDisplay != 0;
                menuVideoSource.Enabled = cap.fHasDlgVideoSource != 0;
                menuVideoFormat.Enabled = cap.fHasDlgVideoFormat != 0;
            }
        }

        private void menuDeviceStop_Click(object sender, System.EventArgs e)
        {
            _camera.Stop();
            menuDeviceStart.Enabled = true;
            menuDeviceStop.Enabled = false;
        }

        private void menuDeviceCancel_Click(object sender, System.EventArgs e)
        {
            ByteArray = null;
            DialogResult = DialogResult.Cancel;
        }

        private void menuVideoSource_Click(object sender, System.EventArgs e)
        {
            _camera.OpenVideoSourceDialog();
        }

        private void menuVideoFormat_Click(object sender, System.EventArgs e)
        {
            _camera.OpenVideoFormatDialog();
        }

        private void menuVideoDisplay_Click(object sender, System.EventArgs e)
        {
            _camera.OpenVideoDisplayDialog();
        }

        private void menuVideoCompression_Click(object sender, System.EventArgs e)
        {
            _camera.OpenVideoCompressionDialog();
        }
    }
}
