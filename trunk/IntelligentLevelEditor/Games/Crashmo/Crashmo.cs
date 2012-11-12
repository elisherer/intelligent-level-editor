using System;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Crashmo
{
    public class Crashmo
    {
        public enum PosType : byte
        {
            Flag = 1,
            Manhole = 2,
            Switch = 3,
            Door = 4,
            Cloud = 5
        }

        public enum CrashmoFlags : uint //TODO: Flags
        {
            Constant = 0x001,
            Large = 0x002
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CrashmoPosition
        {
            public ushort Xy; // x = bits 12..16 , y = bits 7..11 *negated*
            public byte Type; // 1 = flag, 2 = manhole, 3 = switch, 4 = door, 5 = cloud
            public byte Flags;
            // for manholes & doors it's the color 0=red, 1=yellow...
            // for shiftswitches it's the color (1st nibble) from the palette, 2nd nibble = direction (push, pull, left, right).
            // for flag & clouds it's nothing
        }

        public const int BitmapSize = 32;
        public const int CrashmoColorPaletteSize = 184;
        public static readonly ColorPalette CrashmoColorPalette;

        static Crashmo()
        {
            CrashmoColorPalette = Resources.pushmoPalette.Palette;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CrashmoQrData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Magic; //MTUA

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] CustomCrc32;

            public uint Unknown1; // = 7

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x10)]
            public byte[] Zeros0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)]
            public byte[] Author;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x22)]
            public byte[] LevelName;

            public byte Zero1; // ??

            public uint Difficulty;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Unknown3; // = 04 2C 09 20 01 00 00

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] PaletteData;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] Zeros2;

            public uint UtilitiesLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)] //in the flag - size 2=16x16 3=32x32 ??
            public CrashmoPosition[] Utilities;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x200)]
            public byte[] LevelData;

            public byte Protection; // 4-locked 3-open
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Footer; // = FAFF0F
        } //4+4+4+0x10+0x16+0x22+1+4+7+10+6+4+4+21*4+0x200+1+3 = 720 bytes

        public static bool IsMatchingData(byte[] data)
        {
            return (data[0] == 0xAD && data[1] == 0x0A);
        }

        public static byte[][] DecodeTiled(byte[] levelData)
        {
            //prepare a 2D array
            var bmp = new byte[BitmapSize][];
            for (var i = 0; i < bmp.Length; i++)
                bmp[i] = new byte[BitmapSize];
            var two = 0;
            for (var y = BitmapSize - 1; y >= 0; y--)
                for (var x = 0; x < BitmapSize; x += 2, two++)
                {
                    bmp[y][x] = (byte) (levelData[two] >> 4);
                    bmp[y][x+1] = (byte) (levelData[two] & 0xf);
                }
            return bmp;
        }

        public static byte[] EncodeTiled(byte[][] bmp)
        {
            var mem = new MemoryStream();
            for (var y = BitmapSize - 1; y >= 0 ; y--)
                for (var x = 0; x < BitmapSize; x += 2)
                    mem.WriteByte((byte)((bmp[y][x] << 4) + bmp[y][x+1]));
            return mem.ToArray();
        }

        public static CrashmoQrData ReadFromByteArray(byte[] array)
        {
            //The file start with 0xAD0A and then an uint32 and an lz10 compressed blob

            if (array[0] != 0xAD || array[1] != 0x0A)
                throw new Exception("Corrupt crashmo binary!");

            var ins = new MemoryStream(array);
            var fourBytes = new byte[4];
            ins.Read(fourBytes, 0, 4);
            if (array[0] != 0xAD || array[1] != 0x0A || array[2] != 0 || array[3] != 0)
                throw new Exception("Corrupt crashmo binary!");
            ins.Read(fourBytes, 0, 4);
            var version = BitConverter.ToUInt32(fourBytes,0);
            if (version != 1)
                throw new Exception("Unsupported level version!");
            
            ins.Read(fourBytes, 0, 4);
            var compressedSize = BitConverter.ToUInt32(fourBytes, 0);

            var decompressed = new byte[Marshal.SizeOf(typeof(CrashmoQrData))];
            var ms = new MemoryStream(decompressed);

            var lz10 = new DSDecmp.Formats.Nitro.LZ10();
            try
            {
                lz10.Decompress(ins, compressedSize, ms);
            }
            catch//(Exception ex)
            { }

            ms.Close();  

            return MarshalUtil.ByteArrayToStructure<CrashmoQrData>(decompressed);
        }

        public static CrashmoQrData EmptyCrashmoData()
        {
            var dummyArray = new byte[Marshal.SizeOf(typeof(CrashmoQrData))];
            var pd = MarshalUtil.ByteArrayToStructure<CrashmoQrData>(dummyArray);

            Buffer.BlockCopy(
                new [] { (byte)'M', (byte)'T', (byte)'U', (byte)'A' }, 
                0, pd.Magic, 0, 4);
            pd.Unknown1 = 0x07; //Always 7

            var authorBytes = Encoding.Unicode.GetBytes("NoBody");
            Buffer.BlockCopy(authorBytes, 0, pd.Author, 0, authorBytes.Length);
            var nameBytes = Encoding.Unicode.GetBytes("NoName");
            Buffer.BlockCopy(nameBytes, 0, pd.LevelName, 0, nameBytes.Length);
            
            pd.Difficulty = 1;
            Buffer.BlockCopy(
                new byte[] { 0x04, 0x2C, 0x09, 0x20, 0x01, 0x00, 0x00 },
                0,pd.Unknown3,0,pd.Unknown3.Length);
            Buffer.BlockCopy(
                new byte[] { 0x1F, 0x20, 0x77, 0x21, 0x3D, 0x2A, 0x2E, 0x23, 0x51, 0x1C },
                0, pd.PaletteData, 0, pd.PaletteData.Length);

            pd.Protection = 3;
            pd.Footer[0] = 0xFA; pd.Footer[1] = 0xFF; pd.Footer[2] = 0x0F;
            return pd;
        }

        //todo: crashmo crc32 
        public static byte[] CustomCrc32(byte[] data, int start, int len)
        {
            const ulong poly = 0x04C11DB7;
            const ulong xorout = 0xD87A2314;
            ulong crc = 0;

            for (var i = start; i < start + len; i++)
            {
                var temp = (((crc >> 24) ^ data[i]) & 0xFF) << 24;
                for (var j = 0; j < 8; j++)
                    temp = (temp & 0x80000000) > 0 ? (temp << 1) ^ poly : temp << 1;
                crc = (crc << 8) ^ temp;
            }

            return BitConverter.GetBytes(crc ^ xorout);
        }
    }
}
