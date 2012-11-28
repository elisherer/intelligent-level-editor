using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using IntelligentLevelEditor.Games;
using IntelligentLevelEditor.Properties;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;

/*
 * Todolist:
 * 
 * TODO: (1) add undo/redo
 * TODO: (2) try fix the catching of the click after opening a file from read image or open
 */

namespace IntelligentLevelEditor
{
    public partial class FormEditor : Form
    {
        private string _filePath, _remoteVer;
        private bool _checkNow;
        private IStudio _studio;
        private GameSelect.GameMode _gameMode;

        public FormEditor()
        {
            InitializeComponent();
            
            menuHelpCheckUpdates.Checked = Settings.Default.CheckForUpdatesOnStartup;
            if (Settings.Default.CheckForUpdatesOnStartup)
                bwCheckForUpdates.RunWorkerAsync();
        }

        private void CleanStudio()
        {
            if (_studio == null) return;
            pnlEditor.Controls.Remove((UserControl)_studio);
            ((UserControl)_studio).Dispose();
            _studio = null;
        }

        private void ReadByteArray(byte[] data)
        {
            _gameMode = GameSelect.DetectGame(data);

            if (_gameMode == GameSelect.GameMode.Unknown)
            {
                MessageBox.Show(@"Unknown data.");
                return;
            }

            CleanStudio();
            _studio = GameSelect.GetStudio(_gameMode, statusStrip);
            _studio.LoadData(data);
            pnlEditor.Controls.Add((UserControl)_studio);
            EnableAfterOpen();
        }

        private void NewFile()
        {
            CleanStudio();
            _studio = GameSelect.GetStudio(_gameMode, statusStrip);
            _studio.NewData();
            pnlEditor.Controls.Add((UserControl)_studio);
            EnableAfterOpen();
        }
        
        #region Menu -> File

        private void EnableAfterOpen()
        {
            menuFileSave.Enabled = true;
            menuFileSaveAs.Enabled = true;
            menuQRCodeMake.Enabled = true;
            menuQRCodeMakeCard.Enabled = true;
            pnlEditor.BackColor = SystemColors.Control;
            Text = Application.ProductName + @" (" + _gameMode + @")";
        }

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            if (menuFileSave.Enabled && MessageBox.Show(@"Are you sure you want to discard the current level and create a new one?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            var gameSelect = new GameSelect();
            if (gameSelect.ShowDialog() == DialogResult.Cancel)
                return;
            _gameMode = gameSelect.SelectedGame;
            NewFile();
            _filePath = null;
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"Binary Files|*.bin" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var data = File.ReadAllBytes(ofd.FileName);
                ReadByteArray(data);
                _filePath = ofd.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error Loading:" + Environment.NewLine + ex.Message);
            }
        }

        private void SaveLevel()
        {
            var data = _studio.SaveData();
            if (data == null)
                return; //do nothing
            var fs = File.OpenWrite(_filePath);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                var sfd = new SaveFileDialog { Filter = @"Binary Files|*.bin" };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                _filePath = sfd.FileName;
            }
            if (!string.IsNullOrEmpty(_filePath))
                SaveLevel();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = @"Binary Files|*.bin" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            _filePath = sfd.FileName;
            SaveLevel();
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Menu -> QR Code

        private void menuQRCodeRead_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"All Supported|*.png;*.jpg;*.bmp;*.gif|PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var bmp = new Bitmap(Image.FromFile(ofd.FileName));
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));
                var reader = new QRCodeReader();
                var result = reader.decode(binary);
                if ((result.RawBytes[0] & 0xf0) != 0x40)
                    throw new Exception(@"This code is not for this game");
                var byteArray = (byte[]) ((ArrayList) result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                ReadByteArray(byteArray);
            }
            catch (ReaderException ex)
            {
                MessageBox.Show(@"Error Loading:" + Environment.NewLine + ex.Message);
            }
        }

        private void menuQRCodeCapture_Click(object sender, EventArgs e)
        {
            var byteArray = CameraCapture.GetByteArray();
            if (byteArray != null)
            {
                ReadByteArray(byteArray);
            }
        }

        private ByteMatrix GetQRMatrix(int size, byte[] data)
        {  
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            var str = Encoding.GetEncoding(encoding).GetString(data);
            var hints = new Hashtable { { EncodeHintType.CHARACTER_SET, encoding } };
            return writer.encode(str, BarcodeFormat.QR_CODE, size, size, hints); 
        }

        private void menuQRCodeMake_Click(object sender, EventArgs e)
        {
            var data = _studio.SaveData();
            if (data == null)
                return; //do nothing
            var matrix = GetQRMatrix(100, data);
            var img = new Bitmap(200, 200);
            var g = Graphics.FromImage(img);
            g.Clear(Color.White);
            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                    if (matrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, x * 2, y * 2, 2, 2);
            ImageBox.ShowDialog(img);
        }

        private void menuQRCodeMakeCard_Click(object sender, EventArgs e)
        {
            var data = _studio.SaveData();
            if (data == null)
                return; //do nothing
            var card = _studio.MakeQrCard(GetQRMatrix(100, data));
            if (card != null)
                ImageBox.ShowDialog(card);
        }

        #endregion

        #region Menu -> Help

        private void menuHelpCheckNow_Click(object sender, EventArgs e)
        {
            _checkNow = true;
            bwCheckForUpdates.RunWorkerAsync();
        }

        private void menuHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            Settings.Default.CheckForUpdatesOnStartup = menuHelpCheckUpdates.Checked;
            Settings.Default.Save();
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            (new AboutBox(Icon.ToBitmap())).ShowDialog();
        }

        #endregion

        #region Check for updates
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _remoteVer = @"<Error: Couldn't parse the version number>";
                var request = (HttpWebRequest)WebRequest.Create("http://intelligent-level-editor.googlecode.com/svn/trunk/IntelligentLevelEditor/Properties/AssemblyInfo.cs");
                var responseStream = request.GetResponse().GetResponseStream();
                if (responseStream == null) return;
                var reader = new StreamReader(responseStream);
                string line;
                while ((line = reader.ReadLine()) != null)
                    if (line.Contains("AssemblyFileVersion")) //Get the version between the quotation marks
                    {
                        var start = line.IndexOf('"') + 1;
                        var len = line.LastIndexOf('"') - start;
                        _remoteVer = line.Substring(start, len);
                        break;
                    }
            }
            catch
            {
                //No harm done...possibly no internet connection
            }
        }

        private bool IsNewerAvailable(string newerVersion)
        {
            var thisVersion = Version.Parse(Application.ProductVersion);
            var remoteVersion = Version.Parse(newerVersion);
            return remoteVersion.CompareTo(thisVersion) > 0;
        }

        private void bwCheckForUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsNewerAvailable(_remoteVer))
                MessageBox.Show(string.Format(@"This version is v{0}" + Environment.NewLine + "The version on the server is v{1}" + Environment.NewLine + "You might want to download a newer version.", Application.ProductVersion, _remoteVer));
            else if (_checkNow)
                MessageBox.Show(string.Format(@"v{0} is the latest version.", Application.ProductVersion));
        }
        #endregion

    }
}
