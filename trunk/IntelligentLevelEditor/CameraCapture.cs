using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IntelligentLevelEditor.Capture;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace IntelligentLevelEditor
{
    public partial class CameraCapture : Form
    {
        public delegate void ByteArrayReceivedCallback(byte[] result);

        public class CheckBitmapForQR
        {
            private ByteArrayReceivedCallback _callback;
            private bool _running;
            private Thread _thread;
            private Bitmap _examinedBitmap = null;
            private QRCodeReader _reader = new QRCodeReader();

            public CheckBitmapForQR(ByteArrayReceivedCallback callback)
            {
                _callback = callback;
                _thread = new Thread(new ThreadStart(Run));
                _thread.Name = "CheckBitmapForQR";
                _running = true;
                _thread.Start();
            }

            public void Kill()
            {
                _running = false;
            }

            public void Check(Bitmap bmp)
            {
                if (_examinedBitmap == null) //only if not busy
                    _examinedBitmap = bmp;
            }

            public void Run()
            {
                while (_running)
                {
                    if (_examinedBitmap != null)
                    {
                        try
                        {
                            var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(_examinedBitmap, _examinedBitmap.Width, _examinedBitmap.Height)));
                            var result = _reader.decode(binary);
                            byte[] byteArray;

                            if (((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS]) != null)
                                byteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                            else
                                byteArray = System.Text.Encoding.ASCII.GetBytes(result.Text);

                            if (_callback != null)
                                _callback(byteArray);
                        }
                        catch// (ReaderException ex)
                        {
                            //Nothing was found apperatnly
                        }

                    }
                    //finished checking this bitmap
                    _examinedBitmap = null; //dispose of examined bitmap
                }
            }
        }

        private Capture.CaptureDevice _camera;
        private short _deviceIndex = -1;
        public byte[] ByteArray;
        public CheckBitmapForQR _checker;

        public CameraCapture()
        {
            InitializeComponent();
        }

        public static byte[] GetByteArray()
        {
            var capture = new CameraCapture();
            return capture.ShowDialog() == DialogResult.OK ? capture.ByteArray : null;
        }

        private void Start(short deviceIndex = -1)
        {
            _deviceIndex = _camera.Start(deviceIndex);
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

        private void Stop()
        {
            _camera.Stop();

            menuDeviceStart.Enabled = true;
            menuDeviceStop.Enabled = false;

            menuVideoCompression.Enabled = false;
            menuVideoDisplay.Enabled = false;
            menuVideoSource.Enabled = false;
            menuVideoFormat.Enabled = false;
        }

        private void OnRecievedFrame(Bitmap bmp)
        {
            _checker.Check(bmp); //send bitmap to checking
        }

        private void CameraCapture_Load(object sender, System.EventArgs e)
        {
            var dict = CAP.ListDevices();

            if (dict.Count > 0)
            {
                menuDeviceSelect.DropDownItems.Clear();
                foreach (var key in dict.Keys)
                {
                    menuDeviceSelect.DropDownItems.Add(dict[key].Name, null, menuDeviceSelect_Click).Tag = key;
                }
            }

            _camera = new CaptureDevice(pictureCamera.Handle, pictureCamera.Width, pictureCamera.Height, 50, OnRecievedFrame);
            Start();
            _checker = new CheckBitmapForQR(OnByteArrayReceived);
        }

        private void CameraCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            _checker.Kill();
            _camera.Stop();
        }

        #region Menus

        private void menuDeviceStart_Click(object sender, System.EventArgs e)
        {
            Start(_deviceIndex);
        }

        private void menuDeviceStop_Click(object sender, System.EventArgs e)
        {
            Stop();
        }

        private void menuDeviceSelect_Click(object sender, System.EventArgs e)
        {
            var item = (ToolStripItem)sender;

            Stop();
            Start((short)item.Tag);
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

        #endregion

        private void OnByteArrayReceived(byte[] array)
        {
            if (MessageBox.Show(@"Data captured, Do you want to return it?", "Data Captured", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                ByteArray = array;
            }
        }
    }
}
