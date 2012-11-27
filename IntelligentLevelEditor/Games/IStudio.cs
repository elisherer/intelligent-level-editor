using System.Drawing;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games
{
    public interface IStudio
    {
        void NewData();
        void LoadData(byte[] data);
        byte[] SaveData();
        Image MakeQrCard(ByteMatrix qrMatrix);
    }
}
