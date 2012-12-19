using System.IO;
//using System.Runtime.InteropServices;
//using IntelligentLevelEditor.Utils;

namespace IntelligentLevelEditor.Games.DenpaMen
{
    public class DenpaMen
    {
        public const uint RegionUs = 0x33346841;
        public const uint RegionJp = 0x30385862;
        public const uint RegionEu = 0x775A336A;

        /*
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DenapMenDataPacked : IBitField
        {
            public uint Region;
            public ushort Zeros;
            public ushort IDStart;
            [BitfieldLength(6)]
            public byte AntennaPower;
            [BitfieldLength(5)]
            public byte Stats;
            [BitfieldLength(5)]
            public byte Color;

            [BitfieldLength(5)]
            public byte HeadShape;
            [BitfieldLength(6)]
            public byte FaceShape;
            [BitfieldLength(2)]
            public byte FaceColor;
            [BitfieldLength(3)]
            public byte Unknown0;
            [BitfieldLength(5)]
            public byte HairColor;
            [BitfieldLength(5)]
            public byte Eyes;
            [BitfieldLength(1)]
            public byte Unknown1;
            [BitfieldLength(4)]
            public byte Nose;
            [BitfieldLength(1)]
            public byte Unknown2;

            [BitfieldLength(6)]
            public byte Mouth;
            [BitfieldLength(3)]
            public byte Eyebrows;
            [BitfieldLength(2)]
            public byte Unknown3;
            [BitfieldLength(5)]
            public byte Cheeks;
            [BitfieldLength(5)]
            public byte Glasses;
            [BitfieldLength(3)]
            public byte Unknown4;
            [BitfieldLength(4)]
            public byte StatsClass;
            [BitfieldLength(4)]
            public byte Unknown5;

            [BitfieldLength(7)]
            public byte ColorClass;
            [BitfieldLength(7)]
            public byte AntennaPowerClass;
            [BitfieldLength(7)]
            public byte HeadShapeClass;
            [BitfieldLength(7)]
            public byte FaceShapeClass;
            [BitfieldLength(4)]
            public byte Unknown6;

            [BitfieldLength(7)]
            public byte CheeksClass;
            [BitfieldLength(1)]
            public byte Unknown7;
            [BitfieldLength(6)]
            public byte GlassesClass;
            [BitfieldLength(2)]
            public byte Unknown8;
            [BitfieldLength(5)]
            public byte FaceColorClass;
            [BitfieldLength(3)]
            public byte Unknown9;
            public byte Unknown10;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] Name;

            [BitfieldLength(24)]
            public uint IDEnd;
        }*/

        public struct DenapMenData
        {
            public string Name;

            public uint Region;
            public ushort IDStart; //16 bit
            public uint IDEnd; //24 bit

            public byte AntennaPower;
            public byte Stats;
            public byte Color;
            public byte HeadShape;
            public byte FaceShape;
            public byte FaceColor;
            public byte HairColor;
            public byte Eyes;
            public byte Nose;
            public byte Mouth;
            public byte Eyebrows;
            public byte Cheeks;
            public byte Glasses;

            public byte[] Pack()
            {
                return new byte[53];
            }

            public void Unpack(byte[] input)
            {
                var ms = new MemoryStream(input);
                var br = new BinaryReader(ms);
                Region = br.ReadUInt32();
                br.ReadInt16(); //skip 2 bytes                
                IDStart = br.ReadUInt16();
                var b = br.ReadByte();
                AntennaPower = (byte)(b >> 2);
                //...
                ms.Close();
            }
        }

    }
}
