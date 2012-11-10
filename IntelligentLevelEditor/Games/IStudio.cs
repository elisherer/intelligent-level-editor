using System.Drawing;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games
{
    interface IStudio
    {
        void NewData();
        void LoadData(byte[] data);
        byte[] SaveData();
        byte[] GetPalette();
        byte[][] GetBitmap();
        void RefreshUI();
        Image MakeQrCard(ByteMatrix qrMatrix);
    }
}
