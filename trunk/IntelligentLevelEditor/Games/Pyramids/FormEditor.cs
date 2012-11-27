using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace IntelligentLevelEditor.Games.Pyramids
{
    public partial class FormEditor : Form
    {
        private const byte OBJ_KEY = 0x09;
        private const byte OBJ_PLAYER = 0x12;
        private const byte OBJ_DOOR = 0x13;
        private const byte OBJ_TNT = 0x2F;
        private const byte OBJ_DET = 0x30;

        private const byte TntMax = 40;

        private string _filePath, _remoteVer;
        private byte _selected;
        private bool _checkNow;

        public FormEditor()
        {
            InitializeComponent();
            NewLevel();
        }

        private void DataToGui()
        {
            cmbBackground.SelectedIndex = gridControl.LevelData.GetBackground();
            numPar.Value = gridControl.LevelData.GetParTime();
            gridControl.Redraw();
        }

        private bool CheckLevel()
        {
            var tntCount = 0;
            bool playerSeen = false, keySeen = false, doorSeen = false, detonSeen = false;
            for (var y = 0; y < Pyramids.LevelHeight; y++)
                for (var x = 0; x < Pyramids.LevelWidth; x++)
                {
                    byte obj = gridControl.LevelData.Get(x, y);

                    if (obj == OBJ_DOOR)
                    {
                        if (!doorSeen)
                            doorSeen = true;
                        else
                        {
                            MessageBox.Show("More than one door in the level!");
                            return false;
                        }
                    }
                    else if (obj == OBJ_PLAYER)
                    {
                        if (!playerSeen)
                            playerSeen = true;
                        else
                        {
                            MessageBox.Show("More than one player in the level!");
                            return false;
                        }
                    }
                    else if (obj == OBJ_KEY)
                    {
                        if (!keySeen)
                            keySeen = true;
                        else
                        {
                            MessageBox.Show("More than one key in the level!");
                            return false;
                        }
                    }
                    else if (obj == OBJ_TNT)
                    {
                        tntCount++;
                        if (tntCount > TntMax)
                        {
                            MessageBox.Show("TNT's maximum amount have been reached (" + TntMax + ")!");
                            return false;
                        }
                    }
                    else if (obj == OBJ_DET)
                    {
                        detonSeen = true;
                    }
                }
            if (!doorSeen)
            {
                MessageBox.Show("No door in the level!");
                return false;
            }
            if (!playerSeen)
            {
                MessageBox.Show("No player in the level!");
                return false;
            }
            if (!keySeen)
            {
                MessageBox.Show("No key in the level!");
                return false;
            }
            if (tntCount > 0 && !detonSeen)
                if (MessageBox.Show("There are TNT in the level but no detonator, Are you sure you want to continue?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                    return false;
            if (tntCount == 0 && detonSeen)
                if (MessageBox.Show("There is a TNT detonator but not TNT, Are you sure you want to continue?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                    return false;
            return true;
        }

        private void NewLevel()
        {
            gridControl.LevelData.New();
            _filePath = string.Empty;
            DataToGui();
        }

        private void OpenLevel(string fileName)
        {
            var fs = File.OpenRead(fileName);
            gridControl.LevelData.Read(fs);
            fs.Close();
            DataToGui();
        }

        private void SaveChanges()
        {
            gridControl.LevelData.SetBackground((byte)cmbBackground.SelectedIndex);
            gridControl.LevelData.SetParTime((byte)numPar.Value);
        }

        private void SaveLevel()
        {
            SaveChanges();
            if (!CheckLevel())
                return;
            var fs = File.OpenWrite(_filePath);
            gridControl.LevelData.Write(fs);
            fs.Close();
        }

        #region Menu -> File

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to discard the current level and create a new one?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            NewLevel();
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"Binary Files|*.bin" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            OpenLevel(ofd.FileName);
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

        #region Menu -> View

        private void menuViewShowGrid_Click(object sender, EventArgs e)
        {
            gridControl.SetGrid(menuViewShowGrid.Checked);
            gridControl.Redraw();
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
                var byteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                var ms = new MemoryStream(byteArray);
                gridControl.LevelData.Read(ms);
                ms.Close();
                DataToGui();
            }
            catch (ReaderException ex)
            {
                MessageBox.Show(@"Error Loading:" + Environment.NewLine + ex.Message);
            }
        }


        private ByteMatrix GetQRMatrix(int size)
        {
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            var ms = new MemoryStream();
            SaveChanges();
            gridControl.LevelData.Write(ms);
            var data = ms.ToArray();
            ms.Close();
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
            g.Dispose();
            ImageBox.ShowDialog(img);
        }

        private void menuQRCodeMakeCard_Click(object sender, EventArgs e)
        {
            string strName = "", strCreator = "";
            if (MakeQRCardForm.ShowDialog(ref strName, ref strCreator) == DialogResult.Cancel) return;
            const int imgX = 4, imgY = 2;
            var matrix = GetQRMatrix(100);
            var img = new Bitmap(360, 430);
            var g = Graphics.FromImage(img);
            g.Clear(Color.Black);
            g.DrawImage(pnlLevel.BackgroundImage, imgX, imgY, 352, 220);
            //conceil sand treasures
            for (var y = 0; y < Pyramids.LevelHeight; y++)
                for (var x = 0; x < Pyramids.LevelWidth; x++)
                {
                    var b = gridControl.LevelData.Get(x, y);
                    var byteStr = b.ToString("X2");
                    if (Pyramids.IsSand(b)) byteStr = "01";
                    var icon = Resources.ResourceManager.GetObject("pyramids_sprite_" + byteStr);
                    if (icon != null)
                        g.DrawImage((Image)icon, imgX + x * 22, imgY + y * 22, 22, 22);
                }
            g.FillRectangle(Brushes.White, 80,224,200,200);
            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                    if (matrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, 80 + x * 2, 224 + y * 2, 2, 2);
            var font = new Font("Arial", 16.0f, FontStyle.Bold);
            g.DrawString(strName, font, Brushes.Black, 180, 236, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawString("by " + strCreator, font, Brushes.Black, 180, 414, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawImage(Resources.pyramids_logo, 15, 370,50,50);
            g.DrawImage(Resources.pyramids_logo, 295, 370, 50, 50);

            var smlfont = new Font("Arial", 7.0f, FontStyle.Bold);
            g.DrawString("made with pyramidsleveleditor by elisherer", smlfont, new SolidBrush(Color.FromArgb(0x50,0x50,0x50)), 0, 430, new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.DirectionVertical});
            g.Dispose();
            ImageBox.ShowDialog(img);
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

        }

        #endregion

        private void cmbBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlLevel.BackgroundImage = (Image)Resources.ResourceManager.GetObject("pyramids_back" + (cmbBackground.SelectedIndex + 1));            
        }

        private void ToolButtonClicked(object sender, EventArgs e)
        {
            _selected = (byte)((RadioButton) sender).TabIndex;
        }

        private void gridControl_GridCellClick(int x, int y)
        {
            gridControl.LevelData.Set(x,y,_selected);
            gridControl.Redraw();
        }

        private void gridControl_GridCellHoverDown(int x, int y)
        {
            gridControl.LevelData.Set(x, y, _selected);
            gridControl.Redraw();
        }

        #region Check for updates
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _remoteVer = "<Error: Couldn't parse the version number>";
                var request = (HttpWebRequest)WebRequest.Create("http://pyramidsleveleditor.googlecode.com/svn/trunk/pyrale/Properties/AssemblyInfo.cs");
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
                MessageBox.Show(string.Format("This version is v{0}\nThe version on the server is v{1}\nYou might want to download a newer version", Application.ProductVersion, _remoteVer));
            else if (_checkNow)
                MessageBox.Show(string.Format("v{0} is the latest version.", Application.ProductVersion));
        }
        #endregion
    }
}
