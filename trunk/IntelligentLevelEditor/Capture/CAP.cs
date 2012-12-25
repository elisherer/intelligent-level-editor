using System;
using System.Runtime.InteropServices;

namespace IntelligentLevelEditor.Capture  
{  
    public class CAP
    {
        // All information can be found on: http://msdn.microsoft.com/en-us/library/windows/desktop/dd743599(v=vs.85).aspx

        public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

        #region API Calls
        [DllImport("avicap32.dll")]   
        public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);  
        [DllImport("avicap32.dll")]   
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
        [DllImport("avicap32.dll")]
        public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormatSize(IntPtr hWnd);
        [DllImport("User32.dll")]   
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);   
        [DllImport("User32.dll")]   
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);   
        [DllImport("User32.dll")]   
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);   
        [DllImport("User32.dll")]   
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPSTATUS lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPDRIVERCAPS lParam);  
        [DllImport("User32.dll")]   
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        #endregion

        #region API Constants
        public const int WM_USER = 0x400;
        public const int WS_CHILD = 0x40000000;  
        public const int WS_VISIBLE = 0x10000000;  
        public const int SWP_NOMOVE = 0x2;  
        public const int SWP_NOZORDER = 0x4;

        private const int WM_CAP_START = WM_USER;

        public const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        public const int WM_CAP_SET_CALLBACK_STATUS = WM_CAP_START + 3;
        public const int WM_CAP_SET_CALLBACK_YIELD = WM_CAP_START + 4;
        public const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        public const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        public const int WM_CAP_SET_CALLBACK_WAVESTREAM = WM_CAP_START + 7;

        public const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        public const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        public const int WM_CAP_DRIVER_GET_CAPS = WM_CAP_START + 14;

        public const int WM_CAP_EDIT_COPY = WM_CAP_START + 30;

        public const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
        public const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
        public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;

        public const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        public const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        public const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        public const int WM_CAP_GET_STATUS = WM_CAP_START + 54;

        public const int WM_CAP_GET_FRAME = WM_CAP_START + 60;

        public const int WM_CAP_SET_CALLBACK_CAPCONTROL = WM_CAP_START + 85;
        #endregion

        #region API Structures
        [StructLayout(LayoutKind.Sequential)] 
        public struct VIDEOHDR  
        {
            [MarshalAs(UnmanagedType.I4)] public int lpData;  
            [MarshalAs(UnmanagedType.I4)] public int dwBufferLength;  
            [MarshalAs(UnmanagedType.I4)] public int dwBytesUsed;  
            [MarshalAs(UnmanagedType.I4)] public int dwTimeCaptured;  
            [MarshalAs(UnmanagedType.I4)] public int dwUser;  
            [MarshalAs(UnmanagedType.I4)] public int dwFlags;  
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)] public int[] dwReserved;  
        }
   
        [StructLayout(LayoutKind.Sequential)] 
        public struct BITMAPINFOHEADER  
        {  
            [MarshalAs(UnmanagedType.I4)] public Int32 biSize ;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biWidth ;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biHeight ;  
            [MarshalAs(UnmanagedType.I2)] public short biPlanes;  
            [MarshalAs(UnmanagedType.I2)] public short biBitCount ;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biCompression;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biSizeImage;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biXPelsPerMeter;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biYPelsPerMeter;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biClrUsed;  
            [MarshalAs(UnmanagedType.I4)] public Int32 biClrImportant;  
        }   
   
        [StructLayout(LayoutKind.Sequential)] 
        public struct BITMAPINFO  
        {  
            [MarshalAs(UnmanagedType.Struct, SizeConst=40)] public BITMAPINFOHEADER bmiHeader;  
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=1024)] public Int32[] bmiColors;  
        }  

        [StructLayout(LayoutKind.Sequential)] 
        public struct POINTAPI
        {
            [MarshalAs(UnmanagedType.I4)] public Int32 x; 
            [MarshalAs(UnmanagedType.I4)] public Int32 y;
        }
        [StructLayout(LayoutKind.Sequential)] 
        public struct CAPSTATUS
        {
            [MarshalAs(UnmanagedType.I4)] public Int32  uiImageWidth; // Width of the image
            [MarshalAs(UnmanagedType.I4)] public Int32  uiImageHeight; // Height of the image
            [MarshalAs(UnmanagedType.I4)] public Int32  fLiveWindow; // Now Previewing video?
            [MarshalAs(UnmanagedType.I4)] public Int32  fOverlayWindow; // Now Overlaying video?
            [MarshalAs(UnmanagedType.I4)] public Int32  fScale; // Scale image to client?
            [MarshalAs(UnmanagedType.Struct, SizeConst = 8)] public POINTAPI ptScroll; // Scroll position
            [MarshalAs(UnmanagedType.I4)] public Int32  fUsingDefaultPalette; // Using default driver palette?
            [MarshalAs(UnmanagedType.I4)] public Int32  fAudioHardware; // Audio hardware present?
            [MarshalAs(UnmanagedType.I4)] public Int32  fCapFileExists; // Does capture file exist?
            [MarshalAs(UnmanagedType.I4)] public Int32  dwCurrentVideoFrame; // # of video frames cap'td
            [MarshalAs(UnmanagedType.I4)] public Int32  dwCurrentVideoFramesDropped; // # of video frames dropped
            [MarshalAs(UnmanagedType.I4)] public Int32  dwCurrentWaveSamples; // # of wave samples cap'td
            [MarshalAs(UnmanagedType.I4)] public Int32  dwCurrentTimeElapsedMS; // Elapsed capture duration
            [MarshalAs(UnmanagedType.I4)] public Int32  hPalCurrent; // Current palette in use
            [MarshalAs(UnmanagedType.I4)] public Int32  fCapturingNow; // Capture in progress?
            [MarshalAs(UnmanagedType.I4)] public Int32  dwReturn; // Error value after any operation
            [MarshalAs(UnmanagedType.I4)] public Int32  wNumVideoAllocated; // Actual number of video buffers
            [MarshalAs(UnmanagedType.I4)] public Int32  wNumAudioAllocated; // Actual number of audio buffers
        }

        [StructLayout(LayoutKind.Sequential)] 
        public struct CAPDRIVERCAPS
        {
            [MarshalAs(UnmanagedType.I4)] public UInt32 wDeviceIndex;
            [MarshalAs(UnmanagedType.I4)] public Int32 fHasOverlay;
            [MarshalAs(UnmanagedType.I4)] public Int32 fHasDlgVideoSource;
            [MarshalAs(UnmanagedType.I4)] public Int32 fHasDlgVideoFormat;
            [MarshalAs(UnmanagedType.I4)] public Int32 fHasDlgVideoDisplay;
            [MarshalAs(UnmanagedType.I4)] public Int32 fCaptureInitialized;
            [MarshalAs(UnmanagedType.I4)] public Int32 fDriverSuppliesPalettes;
            [MarshalAs(UnmanagedType.I4)] public Int32 hVideoIn;
            [MarshalAs(UnmanagedType.I4)] public Int32 hVideoOut;
            [MarshalAs(UnmanagedType.I4)] public Int32 hVideoExtIn;
            [MarshalAs(UnmanagedType.I4)] public Int32 hVideoExtOut;
        }
        #endregion

        #region Macros (using SendMessage)

        public static bool capDlgVideoCompression(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DLG_VIDEOCOMPRESSION, 0, 0);
        }

        public static bool capDlgVideoDisplay(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DLG_VIDEODISPLAY, 0, 0);
        }

        public static bool capDlgVideoFormat(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DLG_VIDEOFORMAT, 0, 0);
        }

        public static bool capDlgVideoSource(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DLG_VIDEOSOURCE, 0, 0);
        }

        public static bool capDriverConnect(IntPtr lwnd, short iIndex)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DRIVER_CONNECT, iIndex, 0);
        }

        public static bool capDriverDisconnect(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        public static bool capDriverGetCaps(IntPtr lwnd, ref CAPDRIVERCAPS psCaps, int wSize)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_DRIVER_GET_CAPS, wSize, ref psCaps);
        }

        public static bool capEditCopy(IntPtr lwnd)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_EDIT_COPY, 0, 0);
        }

        public static bool capDriverGetCaps(IntPtr lwnd, ref CAPSTATUS s, int wSize)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_GET_STATUS, wSize, ref s);
        }

        public static bool capPreview(IntPtr lwnd, bool f)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_SET_PREVIEW, f, 0);
        }

        public static bool capPreviewRate(IntPtr lwnd, short wMS)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        public static bool capSetCallbackOnFrame(IntPtr lwnd, CAP.FrameEventHandler fpProc)
        {
            return CAP.SendMessage(lwnd, CAP.WM_CAP_SET_CALLBACK_FRAME, 0, fpProc);
        }

        public static bool capSetVideoFormat(IntPtr hCapWnd, ref CAP.BITMAPINFO psVideoFormat, int wSize)
        {
            return CAP.SendMessage(hCapWnd, CAP.WM_CAP_SET_VIDEOFORMAT, wSize, ref psVideoFormat);
        }

        #endregion
    }  
}  
 