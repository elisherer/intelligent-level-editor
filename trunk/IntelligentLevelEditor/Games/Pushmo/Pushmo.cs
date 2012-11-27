using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Pushmo
{
    public static class Pushmo
    {   
        public enum PushmoFlags : uint
        {
            Protected = 0x002,
            Constant = 0x004,
            UsesManholes = 0x100,
            UsesSwitches = 0x200,
            Large = 0x400
        }

        public const int TransparentIndex = 0xA;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PushmoPosition
        {
            public byte X;
            public byte Y;
            public byte Icon; //0x00:flag 0x01-0x0A:switches 0x0B-0x14:manholes
            public byte Flags; //two nibbles: Flag:[none:none], PulloutSwitches:[color_num:none], Manholes:[color:upsidedown]
            //Manhole colors: 0-red, 1-blue. 2-yellow, 3-green, 4-purple
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PushmoQrData
        {
            public ulong Unknown0;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 
            public byte[] CustomCrc32;

            public byte Level;
            public byte Unknown1; //Always 7 ??

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PaletteData;

            public PushmoPosition FlagPosition;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public PushmoPosition[] PulloutSwitches;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public PushmoPosition[] Manholes;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x22)]
            public byte[] LevelName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xE)]
            public byte[] Garbage0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x14)]
            public byte[] Garbage1;

            public uint Flags;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1A)]
            public byte[] UnknownFFs;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x200)]
            public byte[] LevelData;
        }

        public const int BitmapSize = 32;
        public const int PushmoColorPaletteSize = 184;
        public static readonly ColorPalette PushmoColorPalette;

        static Pushmo()
        {
            PushmoColorPalette = Resources.pushmoPalette.Palette;
        }

        #region Private Methods

        private static void FillValue(byte[] array,byte value)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = value;
        }

        private static void DecodeTile(int iconSize, int tileSize, int ax, int ay, byte[][] bmp, Stream fs)
        {
            if (tileSize == 0)
                bmp[ay][ax]= (byte)fs.ReadByte();
            else
                for (var y = 0; y < iconSize; y += tileSize)
                    for (var x = 0; x < iconSize; x += tileSize)
                        DecodeTile(tileSize, tileSize / 2, x + ax, y + ay, bmp, fs);
        }

        private static void EncodeTile(int iconSize, int tileSize, int ax, int ay, byte[][] bmp, Stream fs)
        {
            if (tileSize == 0)
                fs.WriteByte(bmp[ay][ax]);
            else
                for (var y = 0; y < iconSize; y += tileSize)
                    for (var x = 0; x < iconSize; x += tileSize)
                        EncodeTile(tileSize, tileSize / 2, x + ax, y + ay, bmp, fs);
        }

        #endregion

        public static bool IsMatchingData(byte[] data)
        {
            return (data[0] == 0x8D && data[1] == 0x06);
        }

        public static byte[][] DecodeTiled(byte[] levelData)
        {
            var mem = new MemoryStream();
            foreach (var twoPixels in levelData)
            {
                mem.WriteByte((byte)(twoPixels >> 4));
                mem.WriteByte((byte)(twoPixels & 0xf));
            }
            var bpp = mem.ToArray();
            mem.Close();

            var bmp = new byte[BitmapSize][];
            for (var i = 0; i < bmp.Length; i++)
                bmp[i] = new byte[BitmapSize];
            mem = new MemoryStream(bpp);
            for (var y = 0; y < 32; y += 8)
                for (var x = 0; x < 32; x += 8)
                    DecodeTile(8, 8, x, y, bmp, mem);
            mem.Close();
            return bmp;
        }

        public static byte[] EncodeTiled(byte[][] bmp)
        {
            var mem = new MemoryStream();
            for (var y = 0; y < 32; y += 8)
                for (var x = 0; x < 32; x += 8)
                    EncodeTile(8, 8, x, y, bmp, mem);

            var bpp = mem.ToArray();
            mem.Close();

            var encoded = new byte[bpp.Length/2];
            for (var i = 0; i < encoded.Length; i++)
                encoded[i] = (byte)(bpp[2*i + 1] + (bpp[2*i] << 4));
            mem.Close();
            return encoded;
        }

        public static PushmoQrData ReadFromByteArray(byte[] array)
        {
            if (array[0] != 0x8D || array[1] != 0x06)
                throw new Exception("Corrupt pushmo binary!");
            return MarshalUtil.ByteArrayToStructure<PushmoQrData>(array);
        }

        public static PushmoQrData EmptyPushmoData()
        {
            var dummyArray = new byte[Marshal.SizeOf(typeof(PushmoQrData))];
            var pd = MarshalUtil.ByteArrayToStructure<PushmoQrData>(dummyArray);
            var nameBytes = Encoding.Unicode.GetBytes("NoName");
            Buffer.BlockCopy(nameBytes, 0, pd.LevelName, 0, nameBytes.Length); //?Null
            pd.Unknown0 = 0x000000000000068D;
            pd.Level = 0x01;
            pd.Unknown1 = 0x07; //Always 7 ??
            Buffer.BlockCopy(
                new byte[] {  0x1F, 0x20, 0x77, 0x21, 0x3D, 0x2A, 0x2E, 0x23, 0x51, 0x1C },
                0, pd.PaletteData, 0, pd.PaletteData.Length);
            pd.FlagPosition.Icon = 0xFF;
            pd.FlagPosition.Flags = 0xFF;
            pd.FlagPosition.X = 0xff;
            pd.FlagPosition.Y = 0xff;
            for (var i = 0; i < 10 ;i++)
            {
                pd.PulloutSwitches[i] = pd.FlagPosition;
                pd.Manholes[i] = pd.FlagPosition;
            }
            Buffer.BlockCopy(
                new byte[] { 0x00, 0x04, 0x01, 0xC4, 0x4B, 0x2A, 0x00, 0xA0, 0xD4, 0x13, 0x08, 0x08, 0x00, 0x00 },
                0, pd.Garbage0, 0, pd.Garbage0.Length);
            pd.Flags = (uint)PushmoFlags.Constant + (uint)PushmoFlags.Large; //Open - 32x32/no manholes/no switches
            FillValue(pd.UnknownFFs,0xFF);
            FillValue(pd.LevelData,0xAA);
            return pd;
        }

        /*  Taken from:
            Pushmo Checksum Patcher
            By Virus - Game-Hackers.com, debugroom.com

            Special thanks:
                -   Vash - Game-Hackers.com
                -   http://www.cosc.canterbury.ac.nz/greg.ewing/essays/CRC-Reverse-Engineering.html
                -   http://miscel.dk/MiscEl/CRCcalculations.html
        */
        public static byte[] CustomCrc32(byte[] data, int start, int len)
        {
            const ulong poly = 0x04C11DB7;
            const ulong xorout = 0xD87A2314;
            ulong crc = 0;

            for(var i = start; i < start + len; i++) {
                var temp = (((crc >> 24) ^ data[i]) & 0xFF) << 24;
                for(var j = 0; j < 8; j++)
                    temp = (temp & 0x80000000) > 0 ? (temp << 1) ^ poly : temp << 1;
                crc = (crc << 8) ^ temp;
            }

            return BitConverter.GetBytes(crc ^ xorout);
        }

        public static double ColourDistance(Color e1, Color e2)
        {
            var rmean = (e1.R + e2.R) / 2;
            var r = e1.R - e2.R;
            var g = e1.G - e2.G;
            var b = e1.B - e2.B;
            return Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
        }

        
    }
}
