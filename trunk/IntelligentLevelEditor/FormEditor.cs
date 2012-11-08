using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using IntelligentLevelEditor.Games;
using IntelligentLevelEditor.Games.Crashmo;
using IntelligentLevelEditor.Games.Pushmo;
using IntelligentLevelEditor.Properties;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;

/*
 * Todolist:
 * 
 * TODO: add undo/redo
 * TODO: figure out the rest of the flags of the pushmo...
 * TODO: make drawing even faster by using draw-per-change instead of draw-the-all-thing-on-every-change
 */

namespace IntelligentLevelEditor
{
    public partial class FormEditor : Form
    {
        private string _filePath, _remoteVer;
        private bool _checkNow;
        private IStudio _studio;

        public FormEditor()
        {
            InitializeComponent();
            Localization.ApplyToContainer(this, "FormEditor");
            //make language menu
            var current = Localization.getCurrentDescriptor();
            var langList = Localization.GetLanguages(Path.GetDirectoryName(Application.ExecutablePath));
            if (langList.Count == 0)
            {
                menuHelpLanguage.DropDownItems.Add(new ToolStripMenuItem(@"No languages found") {Enabled = false});
                MessageBox.Show("Error: The selected language file wasn't found.");
            }
            else foreach (var desc in langList)
            {
                var menuItem = new ToolStripMenuItem(desc.Name + " (" + desc.Author + ")");
                if (current!= null && current.Culture.Equals(desc.Culture))
                    menuItem.Checked = true;
                menuItem.Tag = desc.Culture;
                menuItem.Click += menuHelpLanguage_Click;
                menuHelpLanguage.DropDownItems.Add(menuItem);
            }
            
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
            if (Pushmo.IsMatchingData(data))
            {
                CleanStudio();
                var pushmoStudio = new PushmoStudio { Dock = DockStyle.Fill };
                pushmoStudio.LoadData(data);
                pnlEditor.Controls.Add(pushmoStudio);
                _studio = pushmoStudio;
            }
            //todo: else file->open
        }

        private void NewFile(string game)
        {
            switch (game)
            {
                case "Pushmo":
                    CleanStudio();
                    _studio = new PushmoStudio();
                    ((PushmoStudio)_studio).Dock = DockStyle.Fill;
                    pnlEditor.Controls.Add((PushmoStudio)_studio);
                    break;
                case "Crashmo":
                    //TODO: crashmo new
                    CleanStudio();
                    _studio = new PushmoStudio();
                    ((PushmoStudio)_studio).Dock = DockStyle.Fill;
                    pnlEditor.Controls.Add((PushmoStudio)_studio);
                    break;
            }            
        }

        
        #region Menu -> File

        private void EnableAfterOpen()
        {
            menuFileSave.Enabled = true;
            menuFileSaveAs.Enabled = true;
            menuFileImport.Enabled = true;
            menuQRCodeMake.Enabled = true;
            menuQRCodeMakeCard.Enabled = true;
        }

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Localization.GetString("AreYouSureNew"), Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            var gameSelect = new GameSelect();
            if (gameSelect.ShowDialog() == DialogResult.Cancel)
                return;
            NewFile(gameSelect.SelectedGame);
            EnableAfterOpen();
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterBinary") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var data = File.ReadAllBytes(ofd.FileName);
                ReadByteArray(data);
                EnableAfterOpen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Localization.GetString("ErrorLoading") + Environment.NewLine + ex.Message);
            }
            
        }

        private void SaveLevel()
        {
            var fs = File.OpenWrite(_filePath);
            var data = _studio.SaveData();
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                var sfd = new SaveFileDialog { Filter = Localization.GetString("FileFilterBinary") };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                _filePath = sfd.FileName;
            }
            if (!string.IsNullOrEmpty(_filePath))
                SaveLevel();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = Localization.GetString("FileFilterBinary") };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            _filePath = sfd.FileName;
            SaveLevel();
        }

        private void menuFileImport_Click(object sender, EventArgs e)
        {
            /* TODO: fix import to be general
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterImagesOpen") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var image = Image.FromFile(ofd.FileName);
            PushmoImageImport.Import(new Bitmap(image),gridControl.Bitmap,gridControl.Palette);
            RefreshGui();
            RefreshRadioButton();*/
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Menu -> QR Code

        private void menuQRCodeRead_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterImagesOpen") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var bmp = new Bitmap(Image.FromFile(ofd.FileName));
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));
                var reader = new QRCodeReader();
                var result = reader.decode(binary);
                if ((result.RawBytes[0] & 0xf0) != 0x40)
                    throw new Exception(Localization.GetString("ErrorQRPushmo"));
                var byteArray = (byte[]) ((ArrayList) result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                ReadByteArray(byteArray);
            }
            catch (ReaderException ex)
            {
                MessageBox.Show(Localization.GetString("ErrorLoading") + Environment.NewLine + ex.Message);
            }
        }

        private ByteMatrix GetQRMatrix(int size)
        {
            
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            var data = _studio.SaveData();
            var str = Encoding.GetEncoding(encoding).GetString(data);
            var hints = new Hashtable { { EncodeHintType.CHARACTER_SET, encoding } };
            return writer.encode(str, BarcodeFormat.QR_CODE, size, size, hints); 
        }

        private void menuQRCodeMake_Click(object sender, EventArgs e)
        {
            var matrix = GetQRMatrix(100);
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
            /* TODO: card
            const int cardWidth = 400;
            const int cardHeight = 240;
            const int cardRadius = 20;
            const int qrPositionX = 200;
            const int qrPositionY = 34;
            const int pushmoPositionX = 4;
            const int pushmoPositionY = 34;
            var cardColor = new SolidBrush(Color.LightGoldenrodYellow);

            var img = new Bitmap(cardWidth, cardHeight);
            var g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);
            //card style
            g.FillEllipse(cardColor, 0, 0, cardRadius, cardRadius);
            g.FillEllipse(cardColor, cardWidth - cardRadius - 1, 0, cardRadius, cardRadius);
            g.FillEllipse(cardColor, 0, cardHeight - cardRadius - 1, cardRadius, cardRadius);
            g.FillEllipse(cardColor, cardWidth - cardRadius - 1, cardHeight - cardRadius - 1, cardRadius, cardRadius);
            g.DrawEllipse(Pens.Black, 0, 0, cardRadius, cardRadius);
            g.DrawEllipse(Pens.Black, cardWidth - cardRadius - 1, 0, cardRadius, cardRadius);
            g.DrawEllipse(Pens.Black, 0, cardHeight - cardRadius - 1, cardRadius, cardRadius);
            g.DrawEllipse(Pens.Black, cardWidth - cardRadius - 1, cardHeight - cardRadius - 1, cardRadius, cardRadius);
            g.FillRectangle(cardColor, cardRadius / 2, 1, cardWidth - cardRadius, cardHeight - 2);
            g.FillRectangle(cardColor, 1, cardRadius / 2, cardWidth - 2, cardHeight - cardRadius);
            g.DrawLine(Pens.Black, cardRadius/2, 0, cardWidth - cardRadius/2, 0);
            g.DrawLine(Pens.Black, cardRadius/2, cardHeight - 1, cardWidth - cardRadius/2, cardHeight - 1);
            g.DrawLine(Pens.Black, 0, cardRadius/2, 0, cardHeight - cardRadius/2);
            g.DrawLine(Pens.Black, cardWidth - 1, cardRadius/2, cardWidth - 1, cardHeight - cardRadius/2);
            //draw strawberry
            g.DrawImage(Resources.strawberry,cardWidth-80,qrPositionY-28);
            //frames for the data
            g.DrawRectangle(Pens.Black, qrPositionX, qrPositionY, 193, 193);
            g.DrawRectangle(Pens.Black, pushmoPositionX, pushmoPositionY, 193, 193);
            //write name of pushmo
            var font = new Font("Arial", 18.0f, FontStyle.Bold);
            g.DrawString(_data.Name, font, Brushes.Black, cardWidth / 2, pushmoPositionY / 2, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            //draw pushmo
            g.FillRectangle(Brushes.White, pushmoPositionX + 1, pushmoPositionY + 1, 192, 192);
            g.DrawImage(gridControl.CreatePreview(192, 192), pushmoPositionX+1, pushmoPositionY+1);
            //draw qr code
            g.FillRectangle(Brushes.White, qrPositionX + 1,qrPositionY + 1, 192, 192);
            var matrix = GetQRMatrix(100);
            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                    if (matrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, qrPositionX - 3 + x * 2, qrPositionY - 3 + y * 2, 2, 2);
            g.Dispose();
            ImageBox.ShowDialog(img);*/
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
            (new AboutBox()).ShowDialog();
        }

        private void menuHelpLanguage_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if (Settings.Default.Culture.Equals(item.Tag)) return;
            Settings.Default.Culture = (string)item.Tag;
            Settings.Default.Save();
            foreach (var langMenuItem in menuHelpLanguage.DropDownItems)
                if (langMenuItem is ToolStripMenuItem)
                    (langMenuItem as ToolStripMenuItem).Checked = false;
            item.Checked = true;
            Localization.Load(Settings.Default.Culture);
            Localization.ApplyToContainer(this,"FormEditor");
        }

        #endregion

        #region Check for updates
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _remoteVer = Localization.GetString("ErrorVersion");
                var request = (HttpWebRequest)WebRequest.Create("http://pushmoleveleditor.googlecode.com/svn/trunk/PushmoLevelEditor/Properties/AssemblyInfo.cs");
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
                MessageBox.Show(string.Format(Localization.GetString("UpdatesOld"), Application.ProductVersion, _remoteVer));
            else if (_checkNow)
                MessageBox.Show(string.Format(Localization.GetString("UpdatesNew"), Application.ProductVersion));
        }
        #endregion

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterBinary") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var array = File.ReadAllBytes(ofd.FileName);
                var ins = new MemoryStream(array);
                var decompressed = new byte[System.Runtime.InteropServices.Marshal.SizeOf(typeof(Crashmo.CrashmoQrData))];
                var ms = new MemoryStream(decompressed);

                ins.Read(new byte[12], 0, 12);//drop 12 bytes

                var lz10 = new DSDecmp.Formats.Nitro.LZ10();
                try
                {
                    lz10.Decompress(ins, decompressed.Length, ms);
                }
                catch//(Exception ex)
                { }

                ms.Close();
                File.WriteAllBytes(ofd.FileName + ".dec",ms.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(Localization.GetString("ErrorLoading") + Environment.NewLine + ex.Message);
            }
            
        }
    }
}
