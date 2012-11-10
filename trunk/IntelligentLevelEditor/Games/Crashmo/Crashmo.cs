using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor.Games.Crashmo
{
    public class Crashmo
    {
        public enum CrashmoFlags : uint
        {
            Protected = 0x002,
            Constant = 0x004,
            UsesManholes = 0x100,
            UsesSwitches = 0x200,
            Large = 0x400
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CrashmoPosition
        {
            public ushort Xy; // x = bits 12..16 , y = bits 7..11 *negated*
            public byte Type; // 1 = flag, 2 = manhole, 3 = shiftswitches, 4 = doors, 5 = cloud
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

            public uint Flags;

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
            return (data[0] != 0xAD || data[1] != 0x0A);
        }

        public static CrashmoQrData ReadFromByteArray(byte[] array)
        {
            //The file start with 0xAD0A and then an uint32 and an lz10 compressed blob

            if (array[0] != 0xAD || array[1] != 0x0A)
                throw new System.Exception("Corrupt crashmo binary!");

            var ins = new MemoryStream(array);
            var decompressed = new byte[Marshal.SizeOf(typeof(CrashmoQrData))];
            var ms = new MemoryStream(decompressed);

            ins.Read(new byte[12], 0, 12);//drop 12 bytes

            var lz10 = new DSDecmp.Formats.Nitro.LZ10();
            try
            {
                lz10.Decompress(ins, decompressed.Length, ms);
            }
            catch//(Exception ex)
            { }

            ms.Close();  

            /*
            if (decompressed[0] != 0xAD || decompressed[1] != 0x0A)
                throw new System.Exception("Corrupt crashmo binary!");
            */
            return MarshalUtil.ByteArrayToStructure<CrashmoQrData>(decompressed);
        }
    }
}
