using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.Pyramids
{
    public partial class PyramidsStudio : UserControl, IStudio
    {
        private const byte ObjectKey = 0x09;
        private const byte ObjectPlayer = 0x12;
        private const byte ObjectDoor = 0x13;
        private const byte ObjectTnt = 0x2F;
        private const byte ObjectDetonator = 0x30;

        private const byte TntMax = 40;

        private byte _selected;
        private Pyramids _levelData = new Pyramids();

        private readonly StatusStrip _statusStrip;

        public PyramidsStudio(StatusStrip statusStrip)
        {
            InitializeComponent();
            _statusStrip = statusStrip;
        }

        #region IStudio methods

        public void NewData()
        {
            _levelData.New();
            gridControl.SetData(_levelData);
            DataToGui();
        }

        public void LoadData(byte[] data)
        {
            _levelData.Read(data);
            gridControl.SetData(_levelData);
            DataToGui();
        }

        public byte[] SaveData()
        {
            _levelData.SetBackground((byte)cmbBackground.SelectedIndex);
            _levelData.SetParTime((byte)numPar.Value);

            if (!CheckLevel())
                return null;
            var mems = new MemoryStream();
            _levelData.Write(mems);
            mems.Close();
            return mems.ToArray();
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            string strName = "", strCreator = "";
            if (MakeQRCardForm.ShowDialog(ref strName, ref strCreator) == DialogResult.Cancel) return null;
            const int imgX = 4, imgY = 2;
            var img = new Bitmap(360, 430);
            var g = Graphics.FromImage(img);
            g.Clear(Color.Black);
            g.DrawImage(pnlLevel.BackgroundImage, imgX, imgY, 352, 220);
            //conceil sand treasures
            for (var y = 0; y < Pyramids.LevelHeight; y++)
                for (var x = 0; x < Pyramids.LevelWidth; x++)
                {
                    var b = _levelData.Get(x, y);
                    var byteStr = b.ToString("X2");
                    if (Pyramids.IsSand(b)) byteStr = "01";
                    var icon = Properties.Resources.ResourceManager.GetObject("pyramids_sprite_" + byteStr);
                    if (icon != null)
                        g.DrawImage((Image)icon, imgX + x * 22, imgY + y * 22, 22, 22);
                }
            g.FillRectangle(Brushes.White, 80, 224, 200, 200);
            for (var y = 0; y < qrMatrix.Height; ++y)
                for (var x = 0; x < qrMatrix.Width; ++x)
                    if (qrMatrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, 80 + x * 2, 224 + y * 2, 2, 2);
            var font = new Font("Arial", 16.0f, FontStyle.Bold);
            g.DrawString(strName, font, Brushes.Black, 180, 236, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawString("by " + strCreator, font, Brushes.Black, 180, 414, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawImage(Properties.Resources.pyramids_logo, 15, 370, 50, 50);
            g.DrawImage(Properties.Resources.pyramids_logo, 295, 370, 50, 50);

            var smlfont = new Font("Arial", 7.0f, FontStyle.Bold);
            g.DrawString("made with intelligentleveleditor by elisherer", smlfont, new SolidBrush(Color.FromArgb(0x50, 0x50, 0x50)), 0, 430, new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.DirectionVertical });
            g.Dispose();
            return img;
        }

        #endregion

        private void DataToGui()
        {
            cmbBackground.SelectedIndex = _levelData.GetBackground();
            numPar.Value = _levelData.GetParTime();
            gridControl.Redraw();
        }

        private bool CheckLevel()
        {
            var tntCount = 0;
            bool playerSeen = false, keySeen = false, doorSeen = false, detonSeen = false;
            for (var y = 0; y < Pyramids.LevelHeight; y++)
                for (var x = 0; x < Pyramids.LevelWidth; x++)
                {
                    byte obj = _levelData.Get(x, y);

                    if (obj == ObjectDoor)
                    {
                        if (!doorSeen)
                            doorSeen = true;
                        else
                        {
                            MessageBox.Show("More than one door in the level!");
                            return false;
                        }
                    }
                    else if (obj == ObjectPlayer)
                    {
                        if (!playerSeen)
                            playerSeen = true;
                        else
                        {
                            MessageBox.Show("More than one player in the level!");
                            return false;
                        }
                    }
                    else if (obj == ObjectKey)
                    {
                        if (!keySeen)
                            keySeen = true;
                        else
                        {
                            MessageBox.Show("More than one key in the level!");
                            return false;
                        }
                    }
                    else if (obj == ObjectTnt)
                    {
                        tntCount++;
                        if (tntCount > TntMax)
                        {
                            MessageBox.Show("TNT's maximum amount have been reached (" + TntMax + ")!");
                            return false;
                        }
                    }
                    else if (obj == ObjectDetonator)
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

        private void cmbBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlLevel.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("pyramids_back" + (cmbBackground.SelectedIndex + 1));
        }

        private void ToolButtonClicked(object sender, EventArgs e)
        {
            _selected = (byte)((RadioButton)sender).TabIndex;
        }

        #region Grid Events

        private void GridControlGridCellClick(int x, int y)
        {
            _levelData.Set(x, y, _selected);
            gridControl.Redraw();
        }

        private void GridControlGridCellHoverDown(int x, int y)
        {
            _levelData.Set(x, y, _selected);
            gridControl.Redraw();
        }

        private void GridControlGridCellHover(int x, int y)
        {
            _statusStrip.Items[1].Text = @"Position: (" + x + @"," + y + @")";
        }

        #endregion

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.SetGrid(chkGrid.Checked);
            gridControl.Redraw();
        }
    }
}
