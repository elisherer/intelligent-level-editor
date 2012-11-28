using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.Pushmo
{
    public partial class PushmoStudio : UserControl, IStudio
    {
        private enum ToolMode
        {
            Pencil = 0,
            Pipette,
            FloodFill,
            Flag,
            Switches,
            Manhole
        }

        private Pushmo.PushmoQrData _pData;
        private readonly PushmoLevelData _data = new PushmoLevelData();
        private ToolMode _mode;
        private byte _color = 1, _manhole, _switch;
        private readonly StatusStrip _statusStrip;

        public PushmoStudio(StatusStrip statusStrip)
        {
            InitializeComponent();
            _statusStrip = statusStrip;
            propertyGrid.SelectedObject = _data;
            /*_pData = Pushmo.EmptyPushmoData();
            UpdateGui();*/
        }

        #region IStudio methods

        public void NewData()
        {
            _pData = Pushmo.EmptyPushmoData();
            UpdateGui();
        }

        public void LoadData(byte[] data)
        {
            _pData = Pushmo.ReadFromByteArray(data);
            UpdateGui();
        }
        
        public byte[] SaveData()
        {
            UpdatePushmoDataFromGui();
            return MarshalUtil.StructureToByteArray(_pData);
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            const int cardWidth = 400;
            const int cardHeight = 240;
            const int cardRadius = 20;
            const int qrPositionX = 200;
            const int qrPositionY = 34;
            const int pushmoPositionX = 4;
            const int pushmoPositionY = 34;
            const int difficultyPositionX = cardWidth * 25 / 32;
            const int difficultyPositionY = pushmoPositionY / 2 - 6;
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
            g.DrawImage(Resources.strawberry, cardWidth * 5 / 8, qrPositionY - 28);
            //frames for the data
            g.DrawRectangle(Pens.Black, qrPositionX, qrPositionY, 193, 193);
            g.DrawRectangle(Pens.Black, pushmoPositionX, pushmoPositionY, 193, 193);
            //write name of pushmo
            var font = new Font("Arial", 18.0f, FontStyle.Bold);
            g.DrawString(_data.Name, font, Brushes.Black, cardWidth * 11 / 32, pushmoPositionY / 2, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            //draw difficulty
            for (var i = 0; i < 5; i++)
                g.DrawImage(i < (byte)_data.Difficulty ? Resources.icon_star_yellow : Resources.icon_star_light, difficultyPositionX + i * 13, difficultyPositionY);
            if (_data.Locked)
                g.DrawImage(Resources.ico_lock, 4, 4);
            //draw pushmo
            g.FillRectangle(Brushes.White, pushmoPositionX + 1, pushmoPositionY + 1, 192, 192);
            g.DrawImage(gridControl.CreatePreview(192, 192), pushmoPositionX+1, pushmoPositionY+1);
            //draw qr code
            g.FillRectangle(Brushes.White, qrPositionX + 1,qrPositionY + 1, 192, 192);
            
            for (var y = 0; y < qrMatrix.Height; ++y)
                for (var x = 0; x < qrMatrix.Width; ++x)
                    if (qrMatrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, qrPositionX - 3 + x * 2, qrPositionY - 3 + y * 2, 2, 2);
            g.Dispose();
            return img;
        }

        #endregion

        private void RefreshUI()
        {
            RefreshGui();
            RefreshRadioButton();
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
            _data.Difficulty = (Difficulty)_pData.Level;
            _data.Locked = (_pData.Flags & (uint)Pushmo.PushmoFlags.Protected) > 0;
            _data.Large = (_pData.Flags & (uint)Pushmo.PushmoFlags.Large) > 0;

            gridControl.SetData(_pData);
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
            Buffer.BlockCopy(encodedData, 0, _pData.LevelData, 0, encodedData.Length);
            var nameBytes = Encoding.Unicode.GetBytes(_data.Name);
            Buffer.BlockCopy(nameBytes, 0, _pData.LevelName, 0, nameBytes.Length);
            _pData.LevelName[nameBytes.Length] = 0;
            _pData.LevelName[nameBytes.Length + 1] = 0;
            _pData.Level = (byte)_data.Difficulty;
            _pData.FlagPosition = gridControl.Flag;
            _pData.FlagPosition.Icon = (byte)(_pData.FlagPosition.X != 0xFF ? 0 : 0xFF);
            _pData.FlagPosition.Flags = (byte)(_pData.FlagPosition.X != 0xFF ? 0 : 0xFF);
            bool usesSwitches = false, usesManholes = false;
            for (var i = 0; i < gridControl.PulloutSwitches.Length; i++)
            {
                _pData.PulloutSwitches[i] = gridControl.PulloutSwitches[i];
                _pData.PulloutSwitches[i].Icon = (byte)(_pData.PulloutSwitches[i].X != 0xFF ? i + 1 : 0xFF);
                if (_pData.PulloutSwitches[i].X == 0xFF)
                    _pData.PulloutSwitches[i].Flags = 0xFF;
                else
                    usesSwitches = true;
                _pData.Manholes[i] = gridControl.Manholes[i];
                _pData.Manholes[i].Icon = (byte)(_pData.Manholes[i].X != 0xFF ? i + 0x0B : 0xFF);
                if (_pData.Manholes[i].X == 0xFF)
                    _pData.Manholes[i].Flags = 0xFF;
                else
                    usesManholes = true;
            }
            //flags
            _pData.Flags = (uint)Pushmo.PushmoFlags.Constant;
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
            tbtnSwitchTool.Checked = mode == ToolMode.Switches;
            tbtnManholeTool.Checked = mode == ToolMode.Manhole;

            pnlColors.Visible = mode == ToolMode.Pencil || mode == ToolMode.FloodFill;
            pnlSwitches.Visible = mode == ToolMode.Switches;
            pnlManholes.Visible = mode == ToolMode.Manhole;
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
                    gridControl.FloodFill(x, y, _color);
                    break;
                case ToolMode.Manhole:
                    var manhole = _manhole * 2 + (chkManholeSelect.Checked ? 1 : 0);
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
            _statusStrip.Items[1].Text = @"Position: (" + x + @"," + y + @")";
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

        #region Shift routines

        private void ShiftButtonClick(object sender, EventArgs e)
        {
            if (sender == btnShiftUp)
            {
                //Data
                var tempArray = new byte[Pushmo.BitmapSize];
                Buffer.BlockCopy(gridControl.Bitmap[0], 0, tempArray, 0, Pushmo.BitmapSize);
                for (var y = 1; y < Pushmo.BitmapSize; y++)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y - 1], 0, Pushmo.BitmapSize);
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
                for (var i = 0; i < gridControl.PulloutSwitches.Length; i++)
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
                Buffer.BlockCopy(gridControl.Bitmap[Pushmo.BitmapSize - 1], 0, tempArray, 0, Pushmo.BitmapSize);
                for (var y = Pushmo.BitmapSize - 2; y >= 0; y--)
                    Buffer.BlockCopy(gridControl.Bitmap[y], 0, gridControl.Bitmap[y + 1], 0, Pushmo.BitmapSize);
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

        #endregion

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.SetGrid(chkGrid.Checked);
            gridControl.Redraw();
        }

        private void tbtnImportTool_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = ImageImporter.SupportedFiles };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var image = Image.FromFile(ofd.FileName);
            ImageImporter.Import(
                new Bitmap(image),
                Pushmo.PushmoColorPalette, Pushmo.PushmoColorPaletteSize,
                gridControl.Bitmap, gridControl.Palette, Pushmo.TransparentIndex
            );
            RefreshUI();
        }
    }
}
