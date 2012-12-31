using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using IntelligentLevelEditor.Properties;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.HexEditor
{
    public partial class HexEditor : UserControl, IStudio
    {
    
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
                IDisposable ByteProvider = hexbox.ByteProvider as IDisposable;
                if (ByteProvider != null)
                    ByteProvider.Dispose();
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
            var QRByteArray = new byte[0];
            for (int i = 0; i < hexbox.ByteProvider.Length; i++)
            {
                var data = new byte[QRByteArray.Length + 1];
                QRByteArray.CopyTo(data, 0);
                data[i] = hexbox.ByteProvider.ReadByte(i);
                QRByteArray = data;
            }
            return QRByteArray;
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            return null;
        }
    }
}
