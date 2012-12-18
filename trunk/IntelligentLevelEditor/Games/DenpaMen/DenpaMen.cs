using System.Runtime.InteropServices;
using IntelligentLevelEditor.Utils;

namespace IntelligentLevelEditor.Games.DenpaMen
{
    public class DenpaMen
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DenapMenData : IBitField
        {
            public uint Region;
            public ushort Zeros;
            public ushort IDStart;
            [BitfieldLength(6)]
            public ushort AntennaPower;
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
        }
    }
}
