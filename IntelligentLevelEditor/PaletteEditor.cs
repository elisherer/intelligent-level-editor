using System;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.Games.Pushmo;

namespace IntelligentLevelEditor
{
    public partial class PaletteEditor : Form
    {
        private readonly byte[] _palette = new byte[10];
        private RadioButton _selectedRadio;

        public PaletteEditor(byte[] paletteData)
        {
            InitializeComponent();
            Buffer.BlockCopy(paletteData, 0, _palette, 0, 10);
            radColor0.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[0]];
            radColor1.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[1]];
            radColor2.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[2]];
            radColor3.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[3]];
            radColor4.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[4]];
            radColor5.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[5]];
            radColor6.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[6]];
            radColor7.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[7]];
            radColor8.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[8]];
            radColor9.BackColor = Pushmo.PushmoColorPalette.Entries[_palette[9]];

            _selectedRadio = radColor0;

            DrawPalettes(_palette[_selectedRadio.TabIndex]);
        }

        public byte[] GetResult()
        {
            return _palette;
        }

        private void radColor0_CheckedChanged(object sender, EventArgs e)
        {
            _selectedRadio = (RadioButton)sender;
            DrawPalettes(_palette[_selectedRadio.TabIndex]);
        }

        private void DrawPalettes(byte selectedColor)
        {
            var bmp = new Bitmap(208, 128);
            var g = Graphics.FromImage(bmp);
            int x = 0, y = 0;
            for (var i = 0; i < 64; i++)
            {
                g.FillRectangle(new SolidBrush(Pushmo.PushmoColorPalette.Entries[i]), x, y, 26, 16);
                if (i == selectedColor)
                    g.DrawRectangle(new Pen(Color.Gold, 2), x, y, 25, 15);
                x += 26;
                if (x < 208) continue;
                x = 0;
                y += 16;
            }
            g.Dispose();
            picBasic1.Image = bmp;
            bmp = new Bitmap(208, 128);
            g = Graphics.FromImage(bmp);
            x = 0;
            y = 0;
            for (var i = 64; i < 128; i++)
            {
                g.FillRectangle(new SolidBrush(Pushmo.PushmoColorPalette.Entries[i]), x, y, 26, 16);
                if (i == selectedColor)
                    g.DrawRectangle(new Pen(Color.Gold, 2), x, y, 25, 15);
                x += 26;
                if (x < 208) continue;
                x = 0;
                y += 16;
            }
            g.Dispose();
            picBasic2.Image = bmp;
            bmp = new Bitmap(208, 128);
            g = Graphics.FromImage(bmp);
            x = 0;
            y = 0;
            for (var i = 128; i < Pushmo.PushmoColorPaletteSize; i++)
            {
                g.FillRectangle(new SolidBrush(Pushmo.PushmoColorPalette.Entries[i]), x, y, 29, 16);
                if (i == selectedColor)
                    g.DrawRectangle(new Pen(Color.Gold, 2), x, y, 25, 15);
                x += 29;
                if (x < 203) continue;
                x = 0;
                y += 16;
            }
            g.Dispose();
            picRetro.Image = bmp;
            tabControl.SelectedIndex = selectedColor / 64;
        }

        private void picBasic1_MouseDown(object sender, MouseEventArgs e)
        {
            int xpos = e.X / 26, ypos = e.Y / 16;
            _selectedRadio.BackColor = Pushmo.PushmoColorPalette.Entries[xpos + ypos * 8];
            _palette[_selectedRadio.TabIndex] = (byte)(xpos + ypos * 8);
            DrawPalettes(_palette[_selectedRadio.TabIndex]);
        }

        private void picBasic2_MouseDown(object sender, MouseEventArgs e)
        {
            int xpos = e.X / 26, ypos = e.Y / 16;
            _selectedRadio.BackColor = Pushmo.PushmoColorPalette.Entries[64 + xpos + ypos * 8];
            _palette[_selectedRadio.TabIndex] = (byte)(64 + xpos + ypos * 8);
            DrawPalettes(_palette[_selectedRadio.TabIndex]);
        }

        private void picRetro_MouseDown(object sender, MouseEventArgs e)
        {
            int xpos = e.X / 29, ypos = e.Y / 16;
            _selectedRadio.BackColor = Pushmo.PushmoColorPalette.Entries[128 + xpos + ypos * 7];
            _palette[_selectedRadio.TabIndex] = (byte)(128 + xpos + ypos * 7);
            DrawPalettes(_palette[_selectedRadio.TabIndex]);
        }

    }
}
