using System;
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

        public static bool IsMatchingData(byte[] data)
        {
            if (data.Length != 106)
                return false;
            var decipheredData = Crypt.Decrypt(data);
            var ms = new MemoryStream(decipheredData);
            var br = new BinaryReader(ms);
            var region = br.ReadUInt32();
            if (region == RegionUs)
                return true;
            if (region == RegionJp)
                return true;
            if (region == RegionEu)
                return true;
            return false;
        }

        public struct DenapMenData
        {
// ReSharper disable MemberCanBePrivate.Global
            public string Name;

            public uint Region;
// ReSharper disable InconsistentNaming
            public ushort IDStart; //16 bit
            public uint IDEnd; //24 bit
// ReSharper restore InconsistentNaming

            public byte AntennaPower;
            public ushort Stats;
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
// ReSharper restore MemberCanBePrivate.Global

            private byte _writtenBits;
            private byte _writeBits;

            private void WriteBits(BinaryWriter bw, byte data, byte bitCount)
            {
                if (bw == null)
                    return;
                if (bitCount > 8) bitCount = 8;
                while (bitCount > 0)
                {
                    _writeBits >>= 1;
                    if((data & 1) == 1)
                        _writeBits |= 0x80;
                    data >>= 1;
                    bitCount--;
                    _writtenBits++;
                    if (_writtenBits != 8) continue;
                    bw.Write(_writeBits);
                    _writtenBits = 0;
                    _writeBits = 0;
                }
            }

            public byte[] Pack()
            {
                if (String.IsNullOrEmpty(Name)) //The name cannot be completely empty or the game crashes hard.
                    return null;
                var ba = new byte[53];
                var ms = new MemoryStream(ba);
                var bw = new BinaryWriter(ms);
                bw.Write(Region);
                bw.Write((ushort)0);
                bw.Write(IDStart);
                #region ColorClass
                byte colorClass = 0;
                byte color = Color;
                if (color <= 6)
                {
                    if ((((AntennaPower >= 7) && (AntennaPower <= 12)) || ((AntennaPower >= 19) && (AntennaPower <= 24))))
                    {
                        var colorIndexTable = new[]
                            {
                            new byte[] {6,1,0,2,3,4,5},
                            new byte[] {6,1,2,0,3,4,5},
                            new byte[] {6,1,2,3,0,4,5},
                            new byte[] {6,1,2,3,4,0,5},
                            new byte[] {6,1,2,3,4,5,0},
                            new byte[] {6,0,1,2,3,4,5}
                        };
                        color = colorIndexTable[(AntennaPower <= 12) ? AntennaPower - 7 : AntennaPower - 19][color];
                    }
                    if (color != 0)
                    {
                        color--;
                        colorClass = 0x28;
                    }
                }
                else
                {
                    color -= 7;
                    colorClass = 0x5F;
                }
                #endregion
                #region AntennaPowerClass
                byte antennaPowerClass = 0;
                byte antennapower = AntennaPower;
                if ((antennapower >= 1) && (antennapower <= 12))
                {
                    antennapower--;
                    antennaPowerClass = 0x46;
                }
                else if ((antennapower >= 13) && (antennapower <= 24))
                {
                    antennapower -= 13;
                    antennaPowerClass = 0x5A;
                }
                else if (antennapower >= 25)
                {
                    antennapower -= 25;
                    antennaPowerClass = 0x5D;
                }
                #endregion
                #region HeadShapeClass
                byte headShapeClass = 0;
                byte headshape = HeadShape;
                if ((headshape >= 11) && (headshape <= 17))
                {
                    headshape -= 11;
                    headShapeClass = 0x50;
                }
                else if (headshape >= 18)
                {
                    headshape -= 18;
                    headShapeClass = 0x5F;
                }
                #endregion
                #region FaceShapeClass
                byte faceShapeClass = 0;
                byte faceshape = FaceShape;
                if (faceshape >= 9)
                {
                    faceshape -= 9;
                    faceShapeClass = 0x5A;
                }
                #endregion
                #region FaceColorClass
                byte faceColorClass = 0x0C;
                byte facecolor = FaceColor;
                if (facecolor >= 2)
                {
                    facecolor -= 2;
                    faceColorClass = 0;
                }
                #endregion
                #region GlassesClass
                byte glassesClass = 0;
                byte glasses = Glasses;
                if (glasses > 0)
                {
                    glasses--;
                    glassesClass = 0x2D;
                }
                #endregion
                #region CheeksClass
                byte cheeksClass = 0;
                byte cheeks = Cheeks;
                if (cheeks > 0)
                {
                    cheeks--;
                    cheeksClass = 0x5A;
                }
                #endregion
                WriteBits(bw, antennapower, 6);
                WriteBits(bw, (byte)(Stats & 0x1F), 5);
                WriteBits(bw, color, 5);
                WriteBits(bw, headshape, 5);
                WriteBits(bw, faceshape, 6);
                WriteBits(bw, facecolor, 2);
                WriteBits(bw, 0, 3);    //Unknown0
                WriteBits(bw, HairColor, 5);
                WriteBits(bw, Eyes, 5);
                WriteBits(bw, 0, 1);    //Unknown1
                WriteBits(bw, Nose, 4);
                WriteBits(bw, 0, 1);    //Unknown2
                WriteBits(bw, Mouth, 6);
                WriteBits(bw, Eyebrows, 3);
                WriteBits(bw, 0, 2);    //Unknown3
                WriteBits(bw, cheeks, 5);
                WriteBits(bw, glasses, 5);
                WriteBits(bw, 0, 3);    //Unknown4
                WriteBits(bw, (byte)(Stats >> 5), 4);
                WriteBits(bw, 0, 4);    //Unknown5
                WriteBits(bw, colorClass, 7);
                WriteBits(bw, headShapeClass, 7);
                WriteBits(bw, antennaPowerClass, 7);
                WriteBits(bw, faceShapeClass, 7);
                WriteBits(bw, 0, 4);    //Unknown6
                WriteBits(bw, cheeksClass, 7);
                WriteBits(bw, 0, 1);    //Unknown7
                WriteBits(bw, glassesClass, 6);
                WriteBits(bw, 0, 2);    //Unknown8
                WriteBits(bw, faceColorClass, 5);
                WriteBits(bw, 0, 3);    //Unknown9
                WriteBits(bw, 0, 8);    //Unknown10
                var namebytes = new byte[24];
                System.Text.Encoding.Unicode.GetBytes(Name).CopyTo(namebytes,0);
                bw.Write(namebytes, 0, 24);
                //IDEnd is ignored by the game, and when the game generates QR codes, it randomly fills these bytes.
                ms.Close();
                return ba;
            }

            private byte _readBitsLeft;
            private byte _readBits;

            private byte ReadBits(BinaryReader br, byte bitCount)
            {
                byte result = 0;
                if (br == null)
                    return result;
                byte resultMask = 1;
                if (bitCount > 8) bitCount = 8;
                while (bitCount > 0)
                {
                    if (_readBitsLeft == 0)
                    {
                        _readBitsLeft = 8;
                        _readBits = br.ReadByte();
                    }
                    if ((_readBits & 1) == 1)
                        result |= resultMask;
                    resultMask <<= 1;
                    _readBits >>= 1;
                    _readBitsLeft--;
                    bitCount--;
                }
                return result;
            }

            public void Unpack(byte[] input)
            {
                var ms = new MemoryStream(input);
                var br = new BinaryReader(ms);
                Region = br.ReadUInt32();
                br.ReadInt16(); //skip 2 bytes                
                IDStart = br.ReadUInt16();
                AntennaPower = ReadBits(br, 6);
                Stats = ReadBits(br, 5);
                Color = ReadBits(br, 5);
                HeadShape = ReadBits(br, 5);
                FaceShape = ReadBits(br, 6);
                FaceColor = ReadBits(br, 2);
                ReadBits(br, 3);    //Unknwon0
                HairColor = ReadBits(br, 5);
                Eyes = ReadBits(br, 5);
                ReadBits(br, 1); //Unknown1
                Nose = ReadBits(br, 4);
                ReadBits(br, 1); //Unknown2
                Mouth = ReadBits(br, 6);
                Eyebrows = ReadBits(br, 3);
                ReadBits(br, 2); //Unknown3
                Cheeks = ReadBits(br, 5);
                Glasses = ReadBits(br, 5);
                ReadBits(br, 3); //Unknown4

                Stats |= (ushort)(ReadBits(br, 4) << 5);
                ReadBits(br, 4); //Unknown5
                var colorClass = ReadBits(br, 7);
                int colorShift = -1;

                #region HeadShapeClass
                var Class = ReadBits(br, 7);
                if ((Class >= 0x50) && (Class <= 0x5E))
                {
                    HeadShape %= 7;
                    HeadShape += 11;
                }
                else if ((Class >= 0x5F) && (Class <= 0x63))
                {
                    HeadShape %= 6;
                    HeadShape += 18;
                }
                else
                    HeadShape %= 11;
                #endregion
                #region AntennaPowerClass
                Class = ReadBits(br, 7);
                if ((Class >= 0x46) && (Class <= 0x59))
                {
                    AntennaPower %= 12;
                    if (AntennaPower >= 6)
                        colorShift = AntennaPower - 6;
                    AntennaPower++;
                }
                else if ((Class > 0x59) && (Class <= 0x5C))
                {
                    AntennaPower %= 12;
                    if (AntennaPower >= 6)
                        colorShift = AntennaPower - 6;
                    AntennaPower += 13;
                }
                else if ((Class > 0x5C) && (Class <= 0x63))
                {
                    AntennaPower %= 21;
                    AntennaPower += 25;
                }
                else
                    AntennaPower = 0;
                #endregion
                #region ColorClass
                //AntennaPowerClass affects the Color, if the ColorClass is for a single color.
                if ((colorClass >= 0x5F) && (colorClass <= 0x63))
                {
                    Color %= 15;
                    Color += 7;
                }
                else
                {
                    colorShift++;
                    var colorIndexTable = new[]
                        {
                        new byte[] { 1,2,3,4,5,6,1 },
                        new byte[] { 1,3,4,5,6,0,1 },
                        new byte[] { 1,2,4,5,6,0,1 },
                        new byte[] { 1,2,3,5,6,0,1 },
                        new byte[] { 1,2,3,4,6,0,1 },
                        new byte[] { 1,2,3,4,5,0,1 },
                        new byte[] { 2,3,4,5,6,0,1 }
                    };
                    var defaultColor = new byte[] { 0, 2, 3, 4, 5, 6, 1 };
                    if ((colorClass >= 0x28) && (colorClass <= 0x5E))
                        Color = colorIndexTable[colorShift][Color % 7];
                    else
                        Color = defaultColor[colorShift];
                }
                #endregion

                #region FaceShapeClass
                Class = ReadBits(br, 7);
                if ((Class >= 0x5A) && (Class <= 0x63))
                {
                    FaceShape %= 23;
                    FaceShape += 9;
                }
                else
                    FaceShape %= 9;
                #endregion
                ReadBits(br, 4);    //Unknown6

                #region CheeksClass
                Class = ReadBits(br, 7);
                if ((Class <= 0x5A) && (Class <= 0x63))
                {
                    Cheeks %= 7;
                    Cheeks++;
                }
                else
                    Cheeks = 0;
                #endregion
                ReadBits(br, 1);    //Unknown7
                #region GlassesClass
                Class = ReadBits(br, 6);
                if ((Class <= 0x2D) && (Class <= 0x31))
                {
                    Glasses %= 15;
                    Glasses++;
                }
                else
                    Glasses = 0;
                #endregion
                ReadBits(br, 2);    //Unknown8
                #region FaceColorClass
                Class = ReadBits(br, 5);
                if ((Class >= 0x0C) && (Class <= 0x18))
                {
                    FaceColor %= 2;
                }
                else
                {
                    FaceColor %= 4;
                    FaceColor += 2;
                }
                #endregion
                ReadBits(br, 3);    //Unknown9
                ReadBits(br, 8);    //Unknown10
                var bytes = br.ReadBytes(24);
                Name = System.Text.Encoding.Unicode.GetString(bytes);
                //IDEnd is just 3 random values generated by the game, that is ignored anyways.
                ms.Close();
            }
        }

    }
}
