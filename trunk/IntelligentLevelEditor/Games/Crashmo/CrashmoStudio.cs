using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing.common;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Crashmo
{
    public partial class CrashmoStudio : UserControl, IStudio
    {
        private enum ToolMode
        {
            Pencil = 0,
            Pipette,
            FloodFill,
            Flag,
            Switch,
            Manhole,
            Door,
            Cloud
        }

        private Crashmo.CrashmoQrData _pData;
        private readonly CrashmoLevelData _data = new CrashmoLevelData();
        private ToolMode _mode;
        private byte _color, _manhole, _switch, _door, _cloud;
        private readonly StatusStrip _statusStrip;

        public CrashmoStudio(StatusStrip statusStrip)
        {
            InitializeComponent();
            _statusStrip = statusStrip;
            propertyGrid.SelectedObject = _data;
            _pData = Crashmo.EmptyCrashmoData();
            UpdateGui();
        }

        #region IStudio methods

        public void NewData()
        {
            Parent.Text = Application.ProductName + @" v." + Application.ProductVersion;
            _pData = Crashmo.EmptyCrashmoData();
            UpdateGui();
        }

        public void LoadData(byte[] data)
        {
            _pData = Crashmo.ReadFromByteArray(data);
            UpdateGui();
        }
        
        public byte[] SaveData()
        {
            UpdateCrashmoDataFromGui();

            var decompressed = MarshalUtil.StructureToByteArray(_pData);
            var lz10 = new DSDecmp.Formats.Nitro.LZ10();
            var ms = new MemoryStream(decompressed);
            var qrData = new byte[Crashmo.EmptyQrData.Length];
            Buffer.BlockCopy(Crashmo.EmptyQrData,0,qrData,0,qrData.Length);
            var outs = new MemoryStream(qrData);
            outs.Seek(12, SeekOrigin.Begin);
            //outs.Write(new byte[] {0xAD, 0x0A, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0}, 0, 12); //header
            var compressedSize = lz10.Compress(ms, decompressed.Length, outs);
            var compressedSizeBytes = BitConverter.GetBytes(compressedSize);
            Buffer.BlockCopy(compressedSizeBytes, 0, qrData, 8, 4);
            return qrData;
        }

        public byte[] GetPalette()
        {
            return gridControl.Palette;
        }

        public byte[][] GetBitmap()
        {
            return gridControl.Bitmap;
        }

        public void RefreshUI()
        {
            RefreshGui();
            RefreshRadioButton();
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            const int cardWidth = 400;
            const int cardHeight = 240;
            const int cardRadius = 20;
            const int qrPositionX = 200;
            const int qrPositionY = 34;
            const int crashmoPositionX = 4;
            const int crashmoPositionY = 34;
            const int difficultyPositionX = cardWidth * 25 / 32;
            const int difficultyPositionY = 3;
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
            //draw burger
            g.DrawImage(Resources.burger,cardWidth*5/8,qrPositionY-28);
            //frames for the data
            g.DrawRectangle(Pens.Black, qrPositionX, qrPositionY, 193, 193);
            g.DrawRectangle(Pens.Black, crashmoPositionX, crashmoPositionY, 193, 193);
            //write name of crashmo
            var font = new Font("Arial", 18.0f, FontStyle.Bold);
            g.DrawString(_data.Name, font, Brushes.Black, cardWidth * 11 / 32, crashmoPositionY / 2, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            //write name of author
            font = new Font("Arial", 12.0f, FontStyle.Regular);
            g.DrawString(_data.Author, font, Brushes.Black, cardWidth * 55 / 64, crashmoPositionY * 3 / 4, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            //draw difficulty
            for (var i = 0; i < 5; i++ )
                g.DrawImage(i < (byte)_data.Difficulty ? Resources.icon_star_yellow : Resources.icon_star_light, difficultyPositionX + i * 13, difficultyPositionY);
            if (_data.Locked)
                g.DrawImage(Resources.ico_lock, 4, 4);
            //draw crashmo
            g.FillRectangle(Brushes.White, crashmoPositionX + 1, crashmoPositionY + 1, 192, 192);
            g.DrawImage(gridControl.CreatePreview(192, 192), crashmoPositionX+1, crashmoPositionY+1);
            //draw qr code
            g.FillRectangle(Brushes.White, qrPositionX + 1,qrPositionY + 1, 192, 192);
            
            for (var y = 0; y < qrMatrix.Height; ++y)
                for (var x = 0; x < qrMatrix.Width; ++x)
                    if (qrMatrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, qrPositionX - 3 + x * 2, qrPositionY - 3 + y * 2, 2, 2);
            g.Dispose();
            return img;
        }

        public ColorPalette GetAvailableColorPalette()
        {
            return Crashmo.CrashmoColorPalette;
        }

        public int GetAvailableColorPaletteSize()
        {
            return Crashmo.CrashmoColorPaletteSize;
        }

        public byte GetTransparentIndex()
        {
            return 0;
        }

        #endregion

        private void RefreshRadioButton()
        {
            radColor1.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[0]];
            radColor2.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[1]];
            radColor3.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[2]];
            radColor4.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[3]];
            radColor5.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[4]];
            radColor6.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[5]];
            radColor7.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[6]];
            radColor8.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[7]];
            radColor9.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[8]];
            radColorA.BackColor = Crashmo.CrashmoColorPalette.Entries[_pData.PaletteData[9]];
        }

        private void UpdateGui()
        {
            _data.Name = Encoding.Unicode.GetString(_pData.LevelName);
            _data.Author = Encoding.Unicode.GetString(_pData.Author);
            _data.Difficulty = (Difficulty)_pData.Difficulty;
            _data.Locked = _pData.Protection == 4;

            gridControl.SetData(_pData);
            RefreshGui();
            RefreshRadioButton();
            if (!radColor1.Checked)
                radColor1.Checked = true;
            else
                RadioColorCheckedChange(radColor1, null); //update the colors
            propertyGrid.SelectedObject = _data;
        }

        private void UpdateCrashmoDataFromGui()
        {
            var encodedData = Crashmo.Encode(gridControl.Bitmap);
            Buffer.BlockCopy(encodedData, 0, _pData.LevelData, 0, encodedData.Length);
            var nameBytes = Encoding.Unicode.GetBytes(_data.Name);
            Buffer.BlockCopy(nameBytes, 0, _pData.LevelName, 0, nameBytes.Length);
            _pData.LevelName[nameBytes.Length] = 0;
            _pData.LevelName[nameBytes.Length + 1] = 0;
            var authorBytes = Encoding.Unicode.GetBytes(_data.Author);
            Buffer.BlockCopy(authorBytes, 0, _pData.Author, 0, authorBytes.Length);
            _pData.Author[authorBytes.Length] = 0;
            _pData.Author[authorBytes.Length + 1] = 0;

            _pData.Difficulty = (byte)_data.Difficulty;

            uint i = 0; //add utilities

            if (gridControl.Flag != null)
                _pData.Utilities[i++] = gridControl.Flag.UnFix();
            foreach (var utility in gridControl.Manholes.Where(utility => utility != null))
                _pData.Utilities[i++] = utility.UnFix();
            foreach (var utility in gridControl.Switches.Where(utility => utility != null))
                _pData.Utilities[i++] = utility.UnFix();
            foreach (var utility in gridControl.Doors.Where(utility => utility != null))
                _pData.Utilities[i++] = utility.UnFix();
            foreach (var utility in gridControl.Clouds.Where(utility => utility != null))
                _pData.Utilities[i++] = utility.UnFix();
            while (i < _pData.Utilities.Length)
                _pData.Utilities[i++] = new Crashmo.CrashmoPosition();

            _pData.UtilitiesLength = i;

            //wrapping up
            var data = MarshalUtil.StructureToByteArray(_pData);
            var crc = Crashmo.CustomCrc32(data, 0x8, 0x2C8);
            Buffer.BlockCopy(crc, 0, _pData.CustomCrc32, 0, 4);
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
            tbtnSwitchTool.Checked = mode == ToolMode.Switch;
            tbtnManholeTool.Checked = mode == ToolMode.Manhole;
            tbtnDoorTool.Checked = mode == ToolMode.Door;
            tbtnCloudTool.Checked = mode == ToolMode.Cloud;

            pnlColors.Visible = mode == ToolMode.Pencil || mode == ToolMode.FloodFill;
            pnlSwitches.Visible = mode == ToolMode.Switch;
            pnlManholes.Visible = mode == ToolMode.Manhole;
            pnlDoors.Visible = mode == ToolMode.Door;
            pnlClouds.Visible = mode == ToolMode.Cloud;
            lblToolMessage.Visible = mode == ToolMode.Pipette || mode == ToolMode.Flag;
            switch (mode)
            {
                case ToolMode.Flag:
                    lblToolMessage.Text =
                        @"Click on the" + Environment.NewLine +
                        @"map to place" + Environment.NewLine +
                        @"the flag." + Environment.NewLine +
                        Environment.NewLine +
                        @"Click on it" + Environment.NewLine +
                        @"again to delete.";
                    break;
                case ToolMode.Pipette:
                    lblToolMessage.Text =
                        @"Click on a pixel" + Environment.NewLine +
                        @"to get its color";
                    break;
            }

            _mode = mode;
        }


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
                    if (gridControl.Flag != null && gridControl.Flag.X == x && gridControl.Flag.Y == y)
                        gridControl.Flag = null; //erase flag
                    else
                        gridControl.Flag = new CrashmoGridControl.CrashmoFixedPosition((byte)x,(byte)y,(byte)Crashmo.PosType.Flag,0);
                    break;
                case ToolMode.FloodFill:
                    gridControl.FloodFill(x, y, _color);
                    break;
                case ToolMode.Manhole:
                    var manhole = _manhole * 2 + (chkManholeSelect.Checked ? 1 : 0);
                    gridControl.Manholes[manhole] = null; //collect
                    gridControl.Manholes[manhole] = new CrashmoGridControl.CrashmoFixedPosition((byte)x,(byte)y,(byte)Crashmo.PosType.Manhole,_manhole);
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
                case ToolMode.Switch:
                    var flag = (byte) (_switch == 3 ? 4 : _switch);
                    gridControl.Switches[_switch] = new CrashmoGridControl.CrashmoFixedPosition((byte)x, (byte)y, (byte)Crashmo.PosType.Switch, flag);
                    break;
                case ToolMode.Door:
                    var door = _door * 2 + (chkDoorSelect.Checked ? 1 : 0);
                    gridControl.Doors[door] = null; //collect
                    gridControl.Doors[door] = new CrashmoGridControl.CrashmoFixedPosition((byte)x, (byte)y, (byte)Crashmo.PosType.Door, _door);
                    break;
                case ToolMode.Cloud:
                    gridControl.Clouds[_cloud] = null; //collect
                    gridControl.Clouds[_cloud] = new CrashmoGridControl.CrashmoFixedPosition((byte)x, (byte)y, (byte)Crashmo.PosType.Cloud, 0);
                    break;
            }
            if (_mode != ToolMode.Pipette)
                RefreshGui();
        }

        private void GridControlGridCellHover(int x, int y)
        {
            _statusStrip.Text = @"Position: (" + x + @"," + y + @")";
        }

        #endregion

        private void RadioColorCheckedChange(object sender, EventArgs e)
        {
            _color = Byte.Parse((string)((RadioButton)sender).Tag);
            lblSelectedColor.BackColor = ((RadioButton)sender).BackColor;
        }

        private void TbtnToolClick(object sender, EventArgs e)
        {
            SetMode((ToolMode)(Int32.Parse(((ToolStripButton)sender).Tag.ToString())));
        }

        private void RadioSwitchCheckedChanged(object sender, EventArgs e)
        {
            _switch = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void RadioManholeCheckedChanged(object sender, EventArgs e)
        {
            _manhole = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void RadioDoorCheckedChanged(object sender, EventArgs e)
        {
            _door = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void RadioCloudCheckedChanged(object sender, EventArgs e)
        {
            _cloud = Byte.Parse((string)((RadioButton)sender).Tag);
        }

        private void btnEditPalette_Click(object sender, EventArgs e)
        {
            var pe = new PaletteEditor(_pData.PaletteData,_color);
            if (pe.ShowDialog() != DialogResult.OK) return;
            var result = pe.GetResult();
            Buffer.BlockCopy(result, 0, gridControl.Palette, 0, result.Length);
            RefreshRadioButton();
            RefreshGui();
        }

        private void btnDeleteSwitch_Click(object sender, EventArgs e)
        {
            gridControl.Switches[_switch] = null;
            gridControl.Redraw();
        }

        private void btnDeleteManhole_Click(object sender, EventArgs e)
        {
            gridControl.Manholes[_manhole * 2 + (chkManholeSelect.Checked ? 1 : 0)]= null;
            gridControl.Redraw();
        }

        private void btnDeleteDoor_Click(object sender, EventArgs e)
        {
            gridControl.Doors[_door * 2 + (chkDoorSelect.Checked ? 1 : 0)] = null;
            gridControl.Redraw();
        }

        private void btnDeleteCloud_Click(object sender, EventArgs e)
        {
            gridControl.Clouds[_cloud] = null;
            gridControl.Redraw();
        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.SetGrid(chkGrid.Checked);
            gridControl.Redraw();
        }

        private void ShiftButtonClick(object sender, EventArgs e)
        {
            if (sender == btnShiftUp)
            {
                //Data
                var tempArray = new byte[Crashmo.BitmapSize];
                Buffer.BlockCopy(gridControl.Bitmap[0], 0, tempArray, 0, Crashmo.BitmapSize);
                for (var y = 1; y < Crashmo.BitmapSize; y++)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y - 1], 0, Crashmo.BitmapSize);
                Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[Crashmo.BitmapSize - 1], 0, Crashmo.BitmapSize);
                //Objects
                var flag = gridControl.Flag;
                if (flag != null)
                    flag.Y = (byte)(flag.Y == 0 ? Crashmo.BitmapSize - 1 : flag.Y - 1);
                foreach (var utility in gridControl.Switches.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == 0 ? Crashmo.BitmapSize - 1 : utility.Y - 1);
                foreach (var utility in gridControl.Manholes.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == 0 ? Crashmo.BitmapSize - 1 : utility.Y - 1);
                foreach (var utility in gridControl.Doors.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == 0 ? Crashmo.BitmapSize - 1 : utility.Y - 1);
                foreach (var utility in gridControl.Clouds.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == 0 ? Crashmo.BitmapSize - 1 : utility.Y - 1);
            }
            else if (sender == btnShiftLeft) //thanks caitsith2
            {
                //Data
                for (var y = 0; y < Crashmo.BitmapSize; y++)
                {
                    byte tempbyte = gridControl.Bitmap[y][0];
                    Buffer.BlockCopy(gridControl.Bitmap[y], 1, gridControl.Bitmap[y], 0, Crashmo.BitmapSize - 1);
                    gridControl.Bitmap[y][Crashmo.BitmapSize - 1] = tempbyte;
                }
                //Objects
                var flag = gridControl.Flag;
                if (flag != null)
                    flag.X = (byte)(flag.X == 0 ? Crashmo.BitmapSize - 1 : flag.X - 1);
                foreach (var utility in gridControl.Switches.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == 0 ? Crashmo.BitmapSize - 1 : utility.X - 1);
                foreach (var utility in gridControl.Manholes.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == 0 ? Crashmo.BitmapSize - 1 : utility.X - 1);
                foreach (var utility in gridControl.Doors.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == 0 ? Crashmo.BitmapSize - 1 : utility.X - 1);
                foreach (var utility in gridControl.Clouds.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == 0 ? Crashmo.BitmapSize - 1 : utility.X - 1);
            }
            else if (sender == btnShiftRight) //thanks caitsith2
            {
                //Data
                for (var y = 0; y < Crashmo.BitmapSize; y++)
                {
                    var tempArray = new byte[Crashmo.BitmapSize];
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, tempArray, 0, Crashmo.BitmapSize);
                    Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[y], 1, Crashmo.BitmapSize - 1);
                    gridControl.Bitmap[y][0] = tempArray[Crashmo.BitmapSize - 1];
                }
                //Objects
                var flag = gridControl.Flag;
                if (flag != null)
                    flag.X = (byte)(flag.X == Crashmo.BitmapSize - 1 ? 0 : flag.X + 1);
                foreach (var utility in gridControl.Switches.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == Crashmo.BitmapSize - 1 ? 0 : utility.X + 1);
                foreach (var utility in gridControl.Manholes.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == Crashmo.BitmapSize - 1 ? 0 : utility.X + 1);
                foreach (var utility in gridControl.Doors.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == Crashmo.BitmapSize - 1 ? 0 : utility.X + 1);
                foreach (var utility in gridControl.Clouds.Where(utility => utility != null))
                    utility.X = (byte)(utility.X == Crashmo.BitmapSize - 1 ? 0 : utility.X + 1);
            }
            else if (sender == btnShiftDown)
            {
                //Data
                var tempArray = new byte[Crashmo.BitmapSize];
                Buffer.BlockCopy(gridControl.Bitmap[Crashmo.BitmapSize - 1], 0, tempArray, 0, Crashmo.BitmapSize);
                for (var y = Crashmo.BitmapSize - 2; y >= 0; y--)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y + 1], 0, Crashmo.BitmapSize);
                Buffer.BlockCopy(tempArray, 0, gridControl.Bitmap[0], 0, Crashmo.BitmapSize);
                //Objects
                var flag = gridControl.Flag;
                if (flag != null)
                    flag.Y = (byte)(flag.Y == Crashmo.BitmapSize - 1 ? 0 : flag.Y + 1);
                foreach (var utility in gridControl.Switches.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == Crashmo.BitmapSize - 1 ? 0 : utility.Y + 1);
                foreach (var utility in gridControl.Manholes.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == Crashmo.BitmapSize - 1 ? 0 : utility.Y + 1);
                foreach (var utility in gridControl.Doors.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == Crashmo.BitmapSize - 1 ? 0 : utility.Y + 1);
                foreach (var utility in gridControl.Clouds.Where(utility => utility != null))
                    utility.Y = (byte)(utility.Y == Crashmo.BitmapSize - 1 ? 0 : utility.Y + 1);
            }
            RefreshGui();
        }


    }
}
