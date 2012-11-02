using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
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
        private Pushmo.PushmoQrData _pData;
        private readonly PushmoLevelData _data = new PushmoLevelData();
        private string _filePath, _remoteVer;
        private ToolMode _mode;
        private byte _color;
        private byte _manhole;
        private byte _switch;
        private bool _checkNow;

        private enum ToolMode
        {
            Pencil = 0,
            Pipette,
            FloodFill,
            Flag,
            Switches,
            Manhole
        }

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
            NewPushmo();
            propertyGrid.SelectedObject = _data;
            menuHelpCheckUpdates.Checked = Settings.Default.CheckForUpdatesOnStartup;
            if (Settings.Default.CheckForUpdatesOnStartup)
                bwCheckForUpdates.RunWorkerAsync();
        }

        private void NewPushmo()
        {
            Text = Application.ProductName + @" v." + Application.ProductVersion;
            _pData = Pushmo.EmptyPushmoData();
            _filePath = string.Empty;
            UpdateGui();
        }

        private void RefreshRadioButton()
        {
            radColor0.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[0]];
            radColor1.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[1]];
            radColor2.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[2]];
            radColor3.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[3]];
            radColor4.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[4]];
            radColor5.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[5]];
            radColor6.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[6]];
            radColor7.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[7]];
            radColor8.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[8]];
            radColor9.BackColor = Pushmo.PushmoColorPalette.Entries[_pData.PaletteData[9]];

            gridControl.InitializeSwitches();
            radSwitch0.BackgroundImage = gridControl.SwitchBitmaps[0];
            radSwitch1.BackgroundImage = gridControl.SwitchBitmaps[1];
            radSwitch2.BackgroundImage = gridControl.SwitchBitmaps[2];
            radSwitch3.BackgroundImage = gridControl.SwitchBitmaps[3];
            radSwitch4.BackgroundImage = gridControl.SwitchBitmaps[4];
            radSwitch5.BackgroundImage = gridControl.SwitchBitmaps[5];
            radSwitch6.BackgroundImage = gridControl.SwitchBitmaps[6];
            radSwitch7.BackgroundImage = gridControl.SwitchBitmaps[7];
            radSwitch8.BackgroundImage = gridControl.SwitchBitmaps[8];
            radSwitch9.BackgroundImage = gridControl.SwitchBitmaps[9];
        }
        
        private void UpdateGui()
        {
            _data.Name = Encoding.Unicode.GetString(_pData.LevelName);
            _data.Difficulty = (Games.Pushmo.Difficulty)_pData.Level;
            _data.Locked = (_pData.Flags & (uint)Pushmo.PushmoFlags.Protected) > 0;
            _data.Large = (_pData.Flags & (uint)Pushmo.PushmoFlags.Large) > 0;
            
            gridControl.SetData(Pushmo.DecodeTiled(_pData.LevelData), _pData);
            RefreshGui();
            RefreshRadioButton();
            if (!radColor0.Checked)
                radColor0.Checked = true;
            else
                RadioColorCheckedChange(radColor0, null); //update the colors
            propertyGrid.SelectedObject = _data;
        }

        private void UpdatePushmoDataFromGui()
        {
            var encodedData = Pushmo.EncodeTiled(gridControl.Bitmap);
            Buffer.BlockCopy(encodedData,0,_pData.LevelData,0,encodedData.Length);
            var nameBytes = Encoding.Unicode.GetBytes(_data.Name);
            Buffer.BlockCopy(nameBytes, 0, _pData.LevelName, 0, nameBytes.Length);
            _pData.LevelName[nameBytes.Length] = 0;
            _pData.LevelName[nameBytes.Length+1] = 0;
            _pData.Level = (byte)_data.Difficulty;
            _pData.FlagPosition = gridControl.Flag;
            _pData.FlagPosition.Icon = (byte)(_pData.FlagPosition.X != 0xFF ? 0 : 0xFF);
            _pData.FlagPosition.Flags = (byte)(_pData.FlagPosition.X != 0xFF ? 0 : 0xFF);
            bool usesSwitches = false, usesManholes = false;
            for (var i=0 ;i<gridControl.PulloutSwitches.Length;i++)
            {
                _pData.PulloutSwitches[i] = gridControl.PulloutSwitches[i];
                _pData.PulloutSwitches[i].Icon = (byte) (_pData.PulloutSwitches[i].X != 0xFF ? i+1 : 0xFF);
                if (_pData.PulloutSwitches[i].X == 0xFF)
                    _pData.PulloutSwitches[i].Flags = 0xFF;
                else
                    usesSwitches = true;
                _pData.Manholes[i] = gridControl.Manholes[i];
                _pData.Manholes[i].Icon = (byte)(_pData.Manholes[i].X != 0xFF ? i+0x0B : 0xFF);
                if (_pData.Manholes[i].X == 0xFF)
                    _pData.Manholes[i].Flags = 0xFF;
                else
                    usesManholes = true;
            }
            //flags
            _pData.Flags = (uint) Pushmo.PushmoFlags.Constant;
            if (_data.Locked)
                _pData.Flags += (uint)Pushmo.PushmoFlags.Protected;
            if (_data.Large)
                _pData.Flags += (uint)Pushmo.PushmoFlags.Large;
            if (usesSwitches)
                _pData.Flags += (uint)Pushmo.PushmoFlags.UsesSwitches;
            if (usesManholes)
                _pData.Flags += (uint)Pushmo.PushmoFlags.UsesManholes;
            //wrapping up
            var data = MarshalUtil.StructureToByteArray(_pData);
            var crc = Pushmo.CustomCrc32(data, 0xC, 0x2C2);
            Buffer.BlockCopy(crc,0,_pData.CustomCrc32,0,4);
        }

        private void ReadByteArray(byte[] array)
        {
            if (array[0] == 0x8D && array[1] == 0x06)
            {
                _pData = Pushmo.ReadFromByteArray(array);
                UpdateGui();
            }
            else
            {
                var x = Crashmo.ReadFromByteArray(array);
                MessageBox.Show("TEST");
            }
                
        }

        private void RefreshGui()
        {
            gridControl.Redraw();
            picShow.Image = gridControl.GetThumb();
        }

        private void SetMode(ToolMode mode)
        {
            tbtnPencilTool.Checked = mode == ToolMode.Pencil;          
            tbtnPipetteTool.Checked = mode == ToolMode.Pipette;
            tbtnFillTool.Checked = mode == ToolMode.FloodFill;
            tbtnFlagTool.Checked = mode == ToolMode.Flag;
            tbtnSwitchTool.Checked = mode == ToolMode.Switches;
            tbtnManholeTool.Checked = mode == ToolMode.Manhole;

            pnlColors.Visible = mode == ToolMode.Pencil || mode == ToolMode.FloodFill;
            pnlSwitches.Visible = mode == ToolMode.Switches;
            pnlManholes.Visible = mode == ToolMode.Manhole;
            lblToolMessage.Visible = mode == ToolMode.Pipette || mode == ToolMode.Flag;
            switch (mode)
            {
                case ToolMode.Flag:
                    lblToolMessage.Text = Localization.GetString("TipFlagTool");
                    break;
                case ToolMode.Pipette:
                    lblToolMessage.Text = Localization.GetString("TipPipetteTool");
                    break;
            }

            _mode = mode;
        }
        
        #region Menu -> File


        private void menuFileNew_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("What level would you like to make? (Pushmo=Yes, Crashmo=No, or cancel)",
                                      "Level Editor", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
                menuFileNewPushmo_Click(menuFileNewPushmo, e);
            else if (res == DialogResult.No)
                menuFileNewCrashmo_Click(menuFileNewCrashmo, e);
        }

        private void menuFileNewPushmo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Localization.GetString("AreYouSureNew"), Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            NewPushmo();
        }

        private void menuFileNewCrashmo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "Sorry");
            /*
            if (MessageBox.Show(Localization.GetString("AreYouSureNew"), Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            */
        }


        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterBinary") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                ReadByteArray(File.ReadAllBytes(ofd.FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Localization.GetString("ErrorLoading") + Environment.NewLine + ex.Message);
            }
            
        }

        private void SavePushmo()
        {
            var fs = File.OpenWrite(_filePath);
            UpdatePushmoDataFromGui();
            var data = MarshalUtil.StructureToByteArray(_pData);
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
                SavePushmo();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = Localization.GetString("FileFilterBinary") };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            _filePath = sfd.FileName;
            SavePushmo();
        }

        private void menuFileImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Localization.GetString("FileFilterImagesOpen") };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var image = Image.FromFile(ofd.FileName);
            PushmoImageImport.Import(new Bitmap(image),gridControl.Bitmap,gridControl.Palette);
            RefreshGui();
            RefreshRadioButton();
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
            UpdatePushmoDataFromGui();
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            var data = MarshalUtil.StructureToByteArray(_pData);
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

        #region Grid Events

        private void GridControlGridCellHoverDown(int x, int y)
        {
            if (_mode != ToolMode.Pencil) return;
            gridControl.Bitmap[y][x] = _color;
            RefreshGui();
        }

        private void GridControlGridCellClick(int x, int y)
        {
            switch (_mode)
            {
                case ToolMode.Flag:
                    var flag = gridControl.Flag;
                    if (x == flag.X && y == flag.Y)
                    {
                        flag.X = 0xFF;
                        flag.Y = 0xFF;
                    }
                    else
                    {
                        flag.X = (byte)x;
                        flag.Y = (byte)y;
                    }
                    gridControl.Flag = flag;
                    break;
                case ToolMode.FloodFill:
                    gridControl.FloodFill(x,y,_color);
                    break;
                case ToolMode.Manhole:
                    var manhole = _manhole*2 + (chkManholeSelect.Checked ? 1 : 0);
                    gridControl.Manholes[manhole].X = (byte)x;
                    gridControl.Manholes[manhole].Y = (byte)y;
                    gridControl.Manholes[manhole].Flags = (byte)(_manhole << 4);
                    break;
                case ToolMode.Pencil:
                    gridControl.Bitmap[y][x] = _color;
                    break;
                case ToolMode.Pipette:
                    foreach (Control ctl in pnlColors.Controls)
                        if ((ctl is RadioButton) && (ctl.Tag.ToString() == gridControl.Bitmap[y][x].ToString()))
                        {
                            ((RadioButton)ctl).Checked = true;
                            SetMode(ToolMode.Pencil);
                            break;
                        }
                    break;
                case ToolMode.Switches:
                    gridControl.PulloutSwitches[_switch].X = (byte)x;
                    gridControl.PulloutSwitches[_switch].Y = (byte)y;
                    gridControl.PulloutSwitches[_switch].Flags = (byte)(_switch << 4);
                    break;
            }
            if (_mode != ToolMode.Pipette)
                RefreshGui();
        }

        private void GridControlGridCellHover(int x, int y)
        {
            stripPosition.Text = Localization.GetString("LabelPosition") + @" (" + x + @"," + y + @")";
        }

        #endregion

        private void RadioColorCheckedChange(object sender, EventArgs e)
        {
            _color = Byte.Parse((string)((RadioButton)sender).Tag);
            lblSelectedColor.BackColor = ((RadioButton)sender).BackColor;
        }
        
        private void TbtnToolClick(object sender, EventArgs e)
        {
            SetMode((ToolMode)(Int32.Parse(((ToolStripButton) sender).Tag.ToString())));
        }

        private void btnEditPalette_Click(object sender, EventArgs e)
        {
            var pe = new PaletteEditor(_pData);
            if (pe.ShowDialog()==DialogResult.OK)
            {
                var result = pe.GetResult();
                Buffer.BlockCopy(result,0,gridControl.Palette,0,result.Length);
                RefreshRadioButton();
                RefreshGui();
            }
        }

        private void btnDeleteSwitch_Click(object sender, EventArgs e)
        {
            gridControl.PulloutSwitches[_switch].X = 0xFF;
            gridControl.PulloutSwitches[_switch].Y = 0xFF;
            gridControl.Redraw();
        }

        private void btnDeleteManhole_Click(object sender, EventArgs e)
        {
            var manhole = _manhole * 2 + (chkManholeSelect.Checked ? 1 : 0);
            gridControl.Manholes[manhole].X = 0xFF;
            gridControl.Manholes[manhole].Y = 0xFF;
            gridControl.Redraw();
        }

        private void RadioSwitchCheckedChanged(object sender, EventArgs e)
        {
            _switch = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void RadioManholeCheckedChanged(object sender, EventArgs e)
        {
            _manhole = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void ShiftButtonClick(object sender, EventArgs e)
        {
            if (sender == btnShiftUp)
            {
                //Data
                var tempArray = new byte[Pushmo.BitmapSize];
                Buffer.BlockCopy(gridControl.Bitmap[0],0,tempArray,0,Pushmo.BitmapSize);
                for (var y = 1; y < Pushmo.BitmapSize; y++)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y-1], 0, Pushmo.BitmapSize);
                Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[Pushmo.BitmapSize - 1], 0, Pushmo.BitmapSize);
                //Objects
                var flag = gridControl.Flag;
                if (flag.X != 0xFF)
                {
                    if (flag.Y == 0)
                        flag.Y = Pushmo.BitmapSize - 1;
                    else
                        flag.Y--; 
                }
                gridControl.Flag = flag;
                for (var i=0;i<gridControl.PulloutSwitches.Length;i++)
                {
                    if (gridControl.PulloutSwitches[i].X != 0xFF)
                    {
                        if (gridControl.PulloutSwitches[i].Y == 0)
                            gridControl.PulloutSwitches[i].Y = Pushmo.BitmapSize - 1;
                        else
                            gridControl.PulloutSwitches[i].Y--;                       
                    }
                    if (gridControl.Manholes[i].X != 0xFF)
                    {
                        if (gridControl.Manholes[i].Y == 0)
                            gridControl.Manholes[i].Y = Pushmo.BitmapSize - 1;
                        else
                            gridControl.Manholes[i].Y--;

                    }
                }
            }
            else if (sender == btnShiftLeft) //thanks caitsith2
            {
                //Data
                for (var y = 0; y < Pushmo.BitmapSize; y++)
                {
                    byte tempbyte = gridControl.Bitmap[y][0];
                    Buffer.BlockCopy(gridControl.Bitmap[y], 1, gridControl.Bitmap[y], 0, Pushmo.BitmapSize - 1);
                    gridControl.Bitmap[y][Pushmo.BitmapSize - 1] = tempbyte;
                }
                //Objects
                var flag = gridControl.Flag;
                if (flag.X != 0xFF)
                {
                    if (flag.X == 0)
                        flag.X = Pushmo.BitmapSize - 1;
                    else
                        flag.X--;
                }
                gridControl.Flag = flag;
                for (var i = 0; i < gridControl.PulloutSwitches.Length; i++)
                {
                    if (gridControl.PulloutSwitches[i].X != 0xFF)
                    {
                        if (gridControl.PulloutSwitches[i].X == 0)
                            gridControl.PulloutSwitches[i].X = Pushmo.BitmapSize - 1;
                        else
                            gridControl.PulloutSwitches[i].X--;
                    }
                    if (gridControl.Manholes[i].X != 0xFF)
                    {
                        if (gridControl.Manholes[i].X == 0)
                            gridControl.Manholes[i].X = Pushmo.BitmapSize - 1;
                        else
                            gridControl.Manholes[i].X--;

                    }
                }
            }
            else if (sender == btnShiftRight) //thanks caitsith2
            {
                //Data
                for (var y = 0; y < Pushmo.BitmapSize; y++)
                {
                    var tempArray = new byte[Pushmo.BitmapSize];
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, tempArray, 0, Pushmo.BitmapSize);
                    Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[y], 1, Pushmo.BitmapSize - 1);
                    gridControl.Bitmap[y][0] = tempArray[Pushmo.BitmapSize - 1];
                }
                //Objects
                var flag = gridControl.Flag;
                if (flag.X != 0xFF)
                {
                    if (flag.X == Pushmo.BitmapSize - 1)
                        flag.X = 0;
                    else
                        flag.X++;
                }
                gridControl.Flag = flag;
                for (var i = 0; i < gridControl.PulloutSwitches.Length; i++)
                {
                    if (gridControl.PulloutSwitches[i].X != 0xFF)
                    {
                        if (gridControl.PulloutSwitches[i].X == Pushmo.BitmapSize - 1)
                            gridControl.PulloutSwitches[i].X = 0;
                        else
                            gridControl.PulloutSwitches[i].X++;
                    }
                    if (gridControl.Manholes[i].X != 0xFF)
                    {
                        if (gridControl.Manholes[i].X == Pushmo.BitmapSize - 1)
                            gridControl.Manholes[i].X = 0;
                        else
                            gridControl.Manholes[i].X++;

                    }
                }
            }
            else if (sender == btnShiftDown)
            {
                //Data
                var tempArray = new byte[Pushmo.BitmapSize];
                Buffer.BlockCopy(gridControl.Bitmap[Pushmo.BitmapSize-1], 0, tempArray, 0, Pushmo.BitmapSize);
                for (var y = Pushmo.BitmapSize - 2; y >= 0 ; y--)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y+1], 0, Pushmo.BitmapSize);
                Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[0], 0, Pushmo.BitmapSize);
                //Objects
                var flag = gridControl.Flag;
                if (flag.X != 0xFF)
                {
                    if (flag.Y == Pushmo.BitmapSize - 1)
                        flag.Y = 0;
                    else
                        flag.Y++;
                }
                gridControl.Flag = flag;
                for (var i = 0; i < gridControl.PulloutSwitches.Length; i++)
                {
                    if (gridControl.PulloutSwitches[i].X != 0xFF)
                    {
                        if (gridControl.PulloutSwitches[i].Y == Pushmo.BitmapSize - 1)
                            gridControl.PulloutSwitches[i].Y = 0;
                        else
                            gridControl.PulloutSwitches[i].Y++;
                    }
                    if (gridControl.Manholes[i].X != 0xFF)
                    {
                        if (gridControl.Manholes[i].Y == Pushmo.BitmapSize - 1)
                            gridControl.Manholes[i].Y = 0;
                        else
                            gridControl.Manholes[i].Y++;

                    }
                }
            }
            RefreshGui();
        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.SetGrid(chkGrid.Checked);
            gridControl.Redraw();
        }

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
