using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IntelligentLevelEditor.Capture
{

    public class CaptureDevice
    {
        public delegate void RecievedFrameEventHandler(Bitmap bmp);

        public event RecievedFrameEventHandler RecievedFrame;

        private IntPtr _lwndC; // Holds the unmanaged handle of the control
        private readonly IntPtr _controlPtr; // Holds the managed pointer of the control
        private int _width;
        private int _height;
        private readonly short _rate;
        private readonly CAP.FrameEventHandler _frameEventHandler; // Delegate instance for the frame callback - must keep alive! gc should NOT collect it  
        private bool _running;

        public CaptureDevice(IntPtr handle, int width, int height, short rate, RecievedFrameEventHandler recievedFrame = null)
        {
            _controlPtr = handle;
            _width = width;
            _height = height;
            _rate = rate;
            _frameEventHandler = FrameCallBack;
            if (recievedFrame != null)
                RecievedFrame = recievedFrame;
        }

        public void Stop()
        {
            CAP.capDriverDisconnect(_lwndC);
            _running = false;
        }

        public short Start(short deviceIndex = -1)
        {
            var lpszName = new byte[80];
            var lpszVer = new byte[80];

            var found = false;

            if (deviceIndex < 0) //search for the first device
            {
                deviceIndex = 0;
                while (!found && deviceIndex < 10) //try only values 0-9
                    found = CAP.capGetDriverDescriptionA(deviceIndex++, lpszName, 80, lpszVer, 80);
                deviceIndex--;
            }
            else //use the specified device
                found = CAP.capGetDriverDescriptionA(deviceIndex, lpszName, 80, lpszVer, 80);
            
            if (!found)
                return -1;

            _lwndC = CAP.capCreateCaptureWindowA(lpszName, CAP.WS_VISIBLE + CAP.WS_CHILD, 0, 0, _width, _height, _controlPtr, 0);

            if (CAP.capDriverConnect(_lwndC, 0))
            {
                CAP.capPreviewRate(_lwndC, _rate); //set preview mode refresh rate
                CAP.capPreview(_lwndC, true); //enable preview mode
                
                var bitmapinfo = new CAP.BITMAPINFO();   
                bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(bitmapinfo.bmiHeader);  
                bitmapinfo.bmiHeader.biWidth = _width;
                bitmapinfo.bmiHeader.biHeight = _height;  
                bitmapinfo.bmiHeader.biPlanes = 1;  
                bitmapinfo.bmiHeader.biBitCount = 24;  
                CAP.capSetVideoFormat(_lwndC, ref bitmapinfo, Marshal.SizeOf(bitmapinfo));

                CAP.capSetCallbackOnFrame(_lwndC, _frameEventHandler);
                CAP.SetWindowPos(_lwndC, 0, 0, 0, _width, _height, 6);
                _running = true;
            }
            return deviceIndex;
        }

        public bool IsRunning()
        {
            return _running;
        }

        public void OpenVideoFormatDialog()
        {
            CAP.capDlgVideoFormat(_lwndC);

            var s = new CAP.CAPSTATUS();
            CAP.capDriverGetCaps(_lwndC, ref s, Marshal.SizeOf(s));
            _width = s.uiImageWidth;
            _height = s.uiImageHeight;
        }

        public void OpenVideoSourceDialog()
        {
            CAP.capDlgVideoSource(_lwndC);
        }

        public void OpenVideoDisplayDialog()
        {
            CAP.capDlgVideoDisplay(_lwndC);
        }

        public void OpenVideoCompressionDialog()
        {
            CAP.capDlgVideoCompression(_lwndC);
        }

        public CAP.CAPDRIVERCAPS GetCapabilities()
        {
            var d = new CAP.CAPDRIVERCAPS();
            CAP.capDriverGetCaps(_lwndC, ref d, Marshal.SizeOf(d));
            return d;
        }

        #region Private methods

        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            if (RecievedFrame != null)
            {
                var videoHeader = (CAP.VIDEOHDR) Marshal.PtrToStructure(lpVHdr, typeof (CAP.VIDEOHDR));
                //videoHeader.lpData is (videoHeader.dwBytesUsed) bytes long.
                var ptr = new IntPtr(videoHeader.lpData);
                var height = videoHeader.dwBytesUsed/(_width*3);
                var bmp24 = new Bitmap(_width, height, _width*3, System.Drawing.Imaging.PixelFormat.Format24bppRgb, ptr);
                var bmp = new Bitmap(bmp24); //convert to 32bpp
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                RecievedFrame(bmp);
            }
        }
        #endregion
    }  
}
