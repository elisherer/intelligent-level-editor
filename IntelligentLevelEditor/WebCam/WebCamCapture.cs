using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IntelligentLevelEditor.WebCam
{
	[Designer("Sytem.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))] // make composite
	public class WebCamCapture : UserControl
	{
		private IContainer components;
		private Timer _timer1;

		// property variables
		private int _mTimeToCaptureMilliseconds = 100;
		private int _mWidth = 320;
		private int _mHeight = 240;
		private int _mCapHwnd;
		private ulong _mFrameNumber;

		// global variables to make the video capture go faster
		private WebcamEventArgs x = new WebcamEventArgs();
		private IDataObject _tempObj;
		private System.Drawing.Image _tempImg;
		private bool _bStopped = true;

		// event delegate
		public delegate void WebCamEventHandler (object source, WebcamEventArgs e);
		// fired when a new image is captured
		public event WebCamEventHandler ImageCaptured; 

		#region API Declarations

		[DllImport("user32", EntryPoint="SendMessage")]
		private static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

		[DllImport("avicap32.dll", EntryPoint="capCreateCaptureWindowA")]
		private static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

		[DllImport("user32", EntryPoint="OpenClipboard")]
        private static extern int OpenClipboard(int hWnd);

		[DllImport("user32", EntryPoint="EmptyClipboard")]
        private static extern int EmptyClipboard();

		[DllImport("user32", EntryPoint="CloseClipboard")]
        private static extern int CloseClipboard();

		#endregion

		#region API Constants

	    private const int WM_USER = 1024;

	    private const int WM_CAP_CONNECT = 1034;
	    private const int WM_CAP_DISCONNECT = 1035;
	    private const int WM_CAP_GET_FRAME = 1084;
	    private const int WM_CAP_COPY = 1054;

	    private const int WM_CAP_START = WM_USER;

        private const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
        private const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
        private const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
        private const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
        private const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
        private const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;
	    private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;

		#endregion

		#region NOTES

		/*
		 * If you want to allow the user to change the display size and 
		 * color format of the video capture, call:
		 * SendMessage (mCapHwnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0);
		 * You will need to requery the capture device to get the new settings
		*/

		#endregion


		public WebCamCapture()
		{
			InitializeComponent();
		}

		~WebCamCapture()
		{
			Stop();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{

				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._timer1 = new System.Windows.Forms.Timer(this.components);
			// 
			// timer1
			// 
			this._timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// WebCamCapture
			// 
			this.Name = "WebCamCapture";
			this.Size = new System.Drawing.Size(342, 252);

		}
		#endregion

		#region Control Properties

		/// <summary>
		/// The time intervale between frame captures
		/// </summary>
		public int TimeToCapture_milliseconds
		{
			get
			{ return _mTimeToCaptureMilliseconds; }

			set
			{ _mTimeToCaptureMilliseconds = value; }
		}

		/// <summary>
		/// The height of the video capture image
		/// </summary>
		public int CaptureHeight
		{
			get
			{ return _mHeight; }
			
			set
			{ _mHeight = value; }
		}

		/// <summary>
		/// The width of the video capture image
		/// </summary>
		public int CaptureWidth
		{
			get
			{ return _mWidth; }

			set
			{ _mWidth = value; }
		}

		/// <summary>
		/// The sequence number to start at for the frame number. OPTIONAL
		/// </summary>
		public ulong FrameNumber
		{
			get
			{ return _mFrameNumber; }

			set
			{ _mFrameNumber = value; }
		}

		#endregion

		#region Start and Stop Capture Functions

        public bool IsRunning()
        {
            return !_bStopped;
        }

		/// <summary>
		/// Starts the video capture
		/// </summary>
		/// <param name="frameNum">the frame number to start at. 
		/// Set to 0 to let the control allocate the frame number</param>
		public void Start(ulong frameNum)
		{
			try
			{
				// for safety, call stop, just in case we are already running
				Stop();

				// setup a capture window
				_mCapHwnd = capCreateCaptureWindowA("WebCap", 0, 0, 0, _mWidth, _mHeight, Handle.ToInt32(), 0);

				// connect to the capture device
				Application.DoEvents();
				SendMessage(_mCapHwnd, WM_CAP_CONNECT, 0, 0);
				SendMessage(_mCapHwnd, WM_CAP_SET_PREVIEW, 0, 0);

				// set the frame number
				_mFrameNumber = frameNum;

				// set the timer information
				_timer1.Interval = _mTimeToCaptureMilliseconds;
				_bStopped = false;
				_timer1.Start();
			}

			catch (Exception excep)
			{ 
				MessageBox.Show(@"An error ocurred while starting the video capture. Check that your webcamera is connected properly and turned on.\r\n\n" + excep.Message); 
				Stop();
			}
		}

		/// <summary>
		/// Stops the video capture
		/// </summary>
		public void Stop()
		{
			try
			{
				// stop the timer
				_bStopped = true;
				_timer1.Stop();

				// disconnect from the video source
				Application.DoEvents();
				SendMessage(_mCapHwnd, WM_CAP_DISCONNECT, 0, 0);
			}

			catch (Exception excep)
			{ // don't raise an error here.
                Console.WriteLine(excep.Message);
			}

		}

		#endregion

		#region Video Capture Code

		/// <summary>
		/// Capture the next frame from the video feed
		/// </summary>
		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				// pause the timer
				_timer1.Stop();

				// get the next frame;
				SendMessage(_mCapHwnd, WM_CAP_GET_FRAME, 0, 0);

				// copy the frame to the clipboard
				SendMessage(_mCapHwnd, WM_CAP_COPY, 0, 0);

				// paste the frame into the event args image
				if (ImageCaptured != null)
				{
					// get from the clipboard
					_tempObj = Clipboard.GetDataObject();
					_tempImg = (System.Drawing.Bitmap) _tempObj.GetData(DataFormats.Bitmap);

					/*
					* For some reason, the API is not resizing the video
					* feed to the width and height provided when the video
					* feed was started, so we must resize the image here
					*/
					x.WebCamImage = _tempImg.GetThumbnailImage(_mWidth, _mHeight, null, IntPtr.Zero);

					// raise the event
					ImageCaptured(this, x);
				}		

				// restart the timer
				Application.DoEvents();
				if (! _bStopped)
					_timer1.Start();
			}

			catch (Exception excep)
			{ 
				MessageBox.Show(@"An error ocurred while capturing the video image. The video capture will now be terminated.\r\n\n" + excep.Message);
				Stop(); // stop the process
			}
		}

		#endregion
	}
}
