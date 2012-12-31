using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.HexEditor
{
    public partial class HexEditor : UserControl, IStudio
    {
        public static bool IsMatchingData(byte[] data)
        {
            if (data.Length <= 2953)
                return true;
            if (data.Length <= 4296)
                return !data.Where((t, i) => !(" $%*+-./:0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ").Contains(System.Text.Encoding.ASCII.GetString(data, i, 1))).Any();
            if (data.Length <= 7089)
                return !data.Where((t, i) => !("0123456789").Contains(System.Text.Encoding.ASCII.GetString(data, i, 1))).Any();
            return false;
        }

        public HexEditor()
        {
            InitializeComponent();
        }

        public void NewData()
        {
            LoadData(new byte[0]);
        }

        public void LoadData(byte[] data)
        {
            if (hexbox.ByteProvider != null)
            {
                var byteProvider = hexbox.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexbox.ByteProvider = null;
            }
            hexbox.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(data);
        }

        public byte[] SaveData()
        {
            if (hexbox.ByteProvider == null)
                return null;
            if (hexbox.ByteProvider.Length == 0)
                return null;
            var qrByteArray = new byte[0];
            for (var i = 0; i < hexbox.ByteProvider.Length; i++)
            {
                var data = new byte[qrByteArray.Length + 1];
                qrByteArray.CopyTo(data, 0);
                data[i] = hexbox.ByteProvider.ReadByte(i);
                qrByteArray = data;
            }
            return qrByteArray;
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            var img = new Bitmap(200, 200);
            var g = Graphics.FromImage(img);
            g.Clear(Color.White);
            for (var y = 0; y < qrMatrix.Height; ++y)
                for (var x = 0; x < qrMatrix.Width; ++x)
                    if (qrMatrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, x * 2, y * 2, 2, 2);
            return img;
        }
    }
}
