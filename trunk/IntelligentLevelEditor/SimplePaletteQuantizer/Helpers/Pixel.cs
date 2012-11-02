using System;
using System.Drawing;
using System.Drawing.Imaging;
using SimplePaletteQuantizer.Extensions;

namespace SimplePaletteQuantizer.Helpers
{
    /// <summary>
    /// This is a pixel format independent pixel.
    /// </summary>
    public struct Pixel
    {
        #region | Constants |

        private static readonly float[,] Rgb2Xyz = 
        { 
            { 0.41239083F, 0.35758433F, 0.18048081F },
	        { 0.21263903F, 0.71516865F, 0.072192319F },
	        { 0.019330820F, 0.11919473F, 0.95053220F }
        };

        private static readonly float[,] Xyz2Rgb = 
        {
	        { 3.2409699F, -1.5373832F, -0.49861079F },
	        { -0.96924376F, 1.8759676F, 0.041555084F },
	        { 0.055630036F, -0.20397687F, 1.0569715F }
        };

        #endregion

        #region | Properties |

        private Byte[] Data { get; set; }
        private Int32 BitOffset { get; set; }
        private Int32 BitDepth { get; set; }
        private PixelFormat Format { get; set; }

        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }

        #endregion

        #region | Calculated properties |

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public UInt64 Value
        {
            get { return GetValue(); }
            set { SetValue(value);}
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public Byte Index
        {
            get { return GetIndex(); }
            set { SetIndex(value); }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get 
            { return GetColor(); }
            set { SetColor(value); }
        }

        #endregion

        #region | Bit methods |

        private Boolean GetBit(Int32 offset)
        {
            return (Data[offset >> 3] & 1 << offset % 8) != 0;
        }

        private void SetBit(Int32 offset, Boolean value)
        {
            if (value)
            {
                Data[offset >> 3] |= (Byte) (1 << (offset % 8));
            }
            else
            {
                Data[offset >> 3] &= (Byte) (~(1 << offset % 8));
            }
        }

        private TType GetBit<TType>(Byte offset, TType value)
        {
            return GetBit(offset) ? value : default(TType);
        }

        private Int32 GetBitRange(Byte startOffset, Byte endOffset)
        {
            Int32 result = 0;
            Byte index = 0;

            for (Byte offset = startOffset; offset <= endOffset; offset++)
            {
                result += GetBit(offset, 1 << index);
                index++;
            }

            return result;
        }

        private void SetBitRange(Byte startOffset, Byte endOffset, Int32 value)
        {
            Byte index = 0;

            for (Byte offset = startOffset; offset <= endOffset; offset++)
            {
                Int32 bitValue = 1 << index;
                SetBit(offset, (value & bitValue) == bitValue);
                index++;
            }
        }

        #endregion

        #region | Value methods |

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public UInt64 GetValue()
        {
            UInt64 result;

            switch (BitDepth)
            {
                case 1:
                    result = GetValueAsBit();
                    break;

                case 2:
                    result = GetValueAsTwoBits();
                    break;

                case 4:
                    result = GetValueAsFourBits();
                    break;

                case 8:
                    result = GetValueAsByte();
                    break;

                case 16:
                    result = GetValueAsTwoBytes();
                    break;

                case 24:
                    result = GetValueAsThreeBytes();
                    break;

                case 32:
                    result = GetValueAsFourBytes();
                    break;

                case 48:
                    result = GetValueAsSixBytes();
                    break;

                case 64:
                    result = GetValueAsEightBytes();
                    break;

                default:
                    String message = string.Format("A bit depth of '{0}' is not supported", BitDepth);
                    throw new NotSupportedException(message);
            }

            return result;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public void SetValue(UInt64 value)
        {
            switch (BitDepth)
            {
                case 1:
                    SetValueAsBit((Byte) value);
                    break;

                case 2:
                    SetValueAsTwoBits((Byte) value);
                    break;

                case 4:
                    SetValueAsFourBits((Byte) value);
                    break;

                case 8:
                    SetValueAsByte((Byte) value);
                    break;

                case 16:
                    SetValueAsTwoBytes((UInt16) value);
                    break;

                case 24:
                    SetValueAsThreeBytes((UInt32)value);
                    break;

                case 32:
                    SetValueAsFourBytes((UInt32) value);
                    break;

                case 48:
                    SetValueAsSixBytes(value);
                    break;

                case 64:
                    SetValueAsEightBytes(value);
                    break;

                default:
                    String message = string.Format("A bit depth of '{0}' is not supported", BitDepth);
                    throw new NotSupportedException(message);
            }
        }

        #endregion

        #region | Index methods |

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <returns></returns>
        public Byte GetIndex()
        {
            if (!Format.IsIndexed())
            {
                String message = string.Format("Cannot retrieve index for a non-indexed format '{0}'. Please use Color (or Value) property instead.", Format);
                throw new NotSupportedException(message);
            }

            Byte result;

            switch (Format)
            {
                case PixelFormat.Format1bppIndexed:
                    result = GetValueAsBit();
                    break;

                case PixelFormat.Format4bppIndexed:
                    result = GetValueAsFourBits();
                    break;

                case PixelFormat.Format8bppIndexed:
                    result = GetValueAsByte();
                    break;

                default:
                    String message = string.Format("This pixel format '{0}' is not supported.", Format);
                    throw new NotSupportedException(message);
            }

            return result;
        }

        /// <summary>
        /// Sets the index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SetIndex(Byte index)
        {
            if (!Format.IsIndexed())
            {
                String message = string.Format("Cannot set index for a non-indexed format '{0}'. Please use Color (or Value) property instead.", Format);
                throw new NotSupportedException(message);
            }

            switch (Format)
            {
                case PixelFormat.Format1bppIndexed:
                    SetValueAsBit(index);
                    break;

                case PixelFormat.Format4bppIndexed:
                    SetValueAsFourBits(index);
                    break;

                case PixelFormat.Format8bppIndexed:
                    SetValueAsByte(index);
                    break;

                default:
                    String message = string.Format("This pixel format '{0}' is not supported.", Format);
                    throw new NotSupportedException(message);
            }
        }

        #endregion

        #region | Color methods |

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <returns></returns>
        public Color GetColor()
        {
            //if (Format.IsIndexed())
            //{
            //    String message = string.Format("Cannot retrieve color for an indexed format '{0}'. Please use Index (or Value) property instead.", Format);
            //    throw new NotSupportedException(message);
            //}

            Int32 alpha, red, green, blue;

            switch (Format)
            {
                case PixelFormat.Format16bppArgb1555:
                    alpha = GetBit(15, 255);
                    red = GetBitRange(10, 14);
                    green = GetBitRange(5, 9);
                    blue = GetBitRange(0, 4);
                    break;

                case PixelFormat.Format16bppGrayScale:
                    alpha = 255;
                    red = green = blue = GetBitRange(0, 15);
                    break;

                case PixelFormat.Format16bppRgb555:
                    alpha = 255;
                    red = GetBitRange(10, 14);
                    green = GetBitRange(5, 9);
                    blue = GetBitRange(0, 4);
                    break;

                case PixelFormat.Format16bppRgb565:
                    alpha = 255;
                    red = GetBitRange(11, 15);
                    green = GetBitRange(5, 10);
                    blue = GetBitRange(0, 4);
                    break;

                case PixelFormat.Format24bppRgb:
                    alpha = 255;
                    red = Data[2];
                    green = Data[1];
                    blue = Data[0];
                    break;

                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                    alpha = Data[3];
                    red = Data[2];
                    green = Data[1];
                    blue = Data[0];
                    break;

                case PixelFormat.Format32bppRgb:
                    alpha = 255;
                    red = Data[2];
                    green = Data[1];
                    blue = Data[0];
                    break;

                case PixelFormat.Format48bppRgb:
                    alpha = 255;
                    
                    red = Data[4] + (Data[5] << 8);
                    green = Data[2] + (Data[3] << 8);
                    blue = Data[0] + (Data[1] << 8);

                    DoCalculate(ref red, ref green, ref blue);
                    break;

                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    alpha = (Data[6] + (Data[7] << 8)) >> 5;
                    red = (Data[4] + (Data[5] << 8)) >> 5;
                    green = (Data[2] + (Data[3] << 8)) >> 5;
                    blue = (Data[0] + (Data[1] << 8)) >> 5;
                    break;

                default:
                    String message = string.Format("This pixel format '{0}' is not supported.", Format);
                    throw new NotSupportedException(message);
            }

            Int32 argb = alpha << 24 | red << 16 | green << 8 | blue;
            Color result = Color.FromArgb(argb);
            return result;
        }

        private static void DoCalculate(ref Int32 red, ref Int32 green, ref Int32 blue)
        {
            Single redfloatValue = red/8192.0f;
            Single greenfloatValue = green/8192.0f;
            Single bluefloatValue = blue/8192.0f;

            Single[] result = new Single[3];

            for (Int32 index = 0; index < 3; index++)
            {
                result[index] += Rgb2Xyz[index, 0] * redfloatValue;
                result[index] += Rgb2Xyz[index, 1] * greenfloatValue;
                result[index] += Rgb2Xyz[index, 2] * bluefloatValue;
            }

            Single x = result[0] + result[1] + result[2];
            Single y = result[1];

            if (x > 0)
            {
                redfloatValue = y;
                greenfloatValue = result[0]/x;
                bluefloatValue = result[1]/x;
            }
            else
            {
                redfloatValue = 0.0f;
                greenfloatValue = 0.0f;
                bluefloatValue = 0.0f;
            }

            Single bias = (Single) (Math.Log(0.85f)/-0.693147f);
            Single exposure = 1.0f;
            Single lumAvg = redfloatValue;
            Single lumMax = redfloatValue;
            Single lumNormal = lumMax/lumAvg;
            Single divider = (Single) Math.Log10(lumNormal + 1.0);

            Double yw = (redfloatValue/lumAvg) * exposure;
            Double interpol = Math.Log(2 + Math.Pow(yw/lumNormal, bias)*8);
            Double l = PadeLog(yw);
            redfloatValue = (Single) ((l/interpol)/divider);

            Single z;
            y = redfloatValue;
            Array.Clear(result, 0, 3);

            result[1] = greenfloatValue;
            result[2] = bluefloatValue;

            if ((y > 1e-06f) && (result[1] > 1e-06F) && (result[2] > 1e-06F))
            {
                x = (result[1] * y) / result[2];
                z = (x / result[1]) - x - y;
            }
            else
            {
                x = z = 1e-06F;
            }

            redfloatValue = x;
            greenfloatValue = y;
            bluefloatValue = z;

            Array.Clear(result, 0, 3);

            for (int i = 0; i < 3; i++)
            {
                result[i] += Xyz2Rgb[i, 0] * redfloatValue;
                result[i] += Xyz2Rgb[i, 1] * greenfloatValue;
                result[i] += Xyz2Rgb[i, 2] * bluefloatValue;
            }

            redfloatValue = result[0] > 1 ? 1 : result[0];
            greenfloatValue = result[1] > 1 ? 1 : result[1];
            bluefloatValue = result[2] > 1 ? 1 : result[2];

            red = (Byte) (255*redfloatValue + 0.5);
            green = (Byte) (255*greenfloatValue + 0.5);
            blue = (Byte) (255*bluefloatValue + 0.5);
        }

        private static Double PadeLog(Double value)
        {
            if (value < 1)
            {
                return (value*(6 + value)/(6 + 4*value));
            }

            if (value < 2)
            {
                return (value*(6 + 0.7662*value)/(5.9897 + 3.7658*value));
            }

            return Math.Log(value + 1);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetColor(Color value)
        {
            if (Format.IsIndexed())
            {
                String message = string.Format("Cannot set color for an indexed format '{0}'. Please use Index (or Value) property instead.", Format);
                throw new NotSupportedException(message);
            }

            Int32 alpha = value.A;
            Int32 red = value.R;
            Int32 green = value.G;
            Int32 blue = value.B;

            switch (Format)
            {
                case PixelFormat.Format16bppArgb1555:
                    SetBit(15, alpha > 0);
                    SetBitRange(10, 14, red >> 3);
                    SetBitRange(5, 9, green >> 3);
                    SetBitRange(0, 4, blue >> 3);
                    break;

                case PixelFormat.Format16bppGrayScale:
                    SetBitRange(0, 15, red << 8 + red);
                    break;

                case PixelFormat.Format16bppRgb555:
                    SetBitRange(10, 14, red >> 3);
                    SetBitRange(5, 9, green >> 3);
                    SetBitRange(0, 4, blue >> 3);
                    break;

                case PixelFormat.Format16bppRgb565:
                    SetBitRange(11, 15, red >> 3);
                    SetBitRange(5, 10, green >> 2);
                    SetBitRange(0, 4, blue >> 3);
                    break;

                case PixelFormat.Format24bppRgb:
                    Data[2] = (Byte) red;
                    Data[1] = (Byte) green;
                    Data[0] = (Byte) blue;
                    break;

                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                    Data[3] = (Byte) alpha;
                    Data[2] = (Byte) red;
                    Data[1] = (Byte) green;
                    Data[0] = (Byte) blue;
                    break;

                case PixelFormat.Format32bppRgb:
                    Data[3] = 0;
                    Data[2] = (Byte) red;
                    Data[1] = (Byte) green;
                    Data[0] = (Byte) blue;
                    break;

                case PixelFormat.Format48bppRgb:
                    Data[5] = (Byte) (red >> 3);
                    Data[4] = (Byte) ((red << 5) % 256);
                    Data[3] = (Byte) (green >> 3);
                    Data[2] = (Byte) ((green << 5) % 256);
                    Data[1] = (Byte) (blue >> 3);
                    Data[0] = (Byte) ((blue << 5) % 256);
                    break;

                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    Data[7] = (Byte) (alpha >> 3);
                    Data[6] = (Byte) ((alpha << 5) % 256);
                    Data[5] = (Byte) (red >> 3);
                    Data[4] = (Byte) ((red << 5) % 256);
                    Data[3] = (Byte) (green >> 3);
                    Data[2] = (Byte) ((green << 5) % 256);
                    Data[1] = (Byte) (blue >> 3);
                    Data[0] = (Byte) ((blue << 5) % 256);
                    break;

                default:
                    String message = string.Format("This pixel format '{0}' is not supported.", Format);
                    throw new NotSupportedException(message);
            }
        }

        #endregion

        #region | Helper get methods |

        private Byte GetValueAsBit()
        {
            return (Byte)(GetBit(BitOffset) ? 1 : 0);
        }

        private Byte GetValueAsTwoBits()
        {
            Byte lowBit = (Byte)(GetBit(BitOffset) ? 1 : 0);
            Byte highBit = (Byte)(GetBit(BitOffset + 1) ? 2 : 0);
            return (Byte)(lowBit + highBit);
        }

        private Byte GetValueAsFourBits()
        {
            Byte firstBit = (Byte)(GetBit(BitOffset) ? 1 : 0);
            Byte secondBit = (Byte)(GetBit(BitOffset + 1) ? 2 : 0);
            Byte thirdBit = (Byte)(GetBit(BitOffset + 2) ? 4 : 0);
            Byte fourthBit = (Byte)(GetBit(BitOffset + 3) ? 8 : 0);
            return (Byte)(firstBit + secondBit + thirdBit + fourthBit);
        }

        private Byte GetValueAsByte()
        {
            return Data[0];
        }

        private UInt16 GetValueAsTwoBytes()
        {
            UInt16 result = Data[0];
            result += Convert.ToUInt16(Data[1] << 8);
            return result;
        }

        private UInt32 GetValueAsThreeBytes()
        {
            UInt32 result = Data[0];
            result += (UInt32)Data[1] << 8;
            result += (UInt32)Data[2] << 16;
            return result;
        }

        private UInt32 GetValueAsFourBytes()
        {
            UInt32 result = Data[0];
            result += (UInt32)Data[1] << 8;
            result += (UInt32)Data[2] << 16;
            result += (UInt32)Data[3] << 24;
            return result;
        }

        private UInt64 GetValueAsSixBytes()
        {
            UInt64 result = Data[0];
            result += (UInt64)Data[1] << 8;
            result += (UInt64)Data[2] << 16;
            result += (UInt64)Data[3] << 24;
            result += (UInt64)Data[4] << 32;
            result += (UInt64)Data[5] << 40;
            result += (UInt64)255 << 48;
            result += (UInt64)31 << 56;
            return result;
        }

        private UInt64 GetValueAsEightBytes()
        {
            UInt64 result = Data[0];
            result += (UInt64)Data[1] << 8;
            result += (UInt64)Data[2] << 16;
            result += (UInt64)Data[3] << 24;
            result += (UInt64)Data[4] << 32;
            result += (UInt64)Data[5] << 40;
            result += (UInt64)Data[6] << 48;
            result += (UInt64)Data[7] << 56;
            return result;
        }

        #endregion

        #region | Helper set methods |

        private void SetValueAsBit(Byte value)
        {
            SetBit(7 - BitOffset, value > 0);
        }

        private void SetValueAsTwoBits(Byte value)
        {
            SetBitRange((Byte) BitOffset, (Byte) (BitOffset + 1), value);
        }

        private void SetValueAsFourBits(Byte value)
        {
            SetBitRange((Byte) (8 - BitOffset - BitDepth), (Byte) (7 - BitOffset), value);
        }

        private void SetValueAsByte(Byte value)
        {
            Data[0] = value;
        }

        private void SetValueAsTwoBytes(UInt16 value)
        {
            Data[0] = (Byte)(value % 256);
            Data[1] = (Byte)(value >> 8);
        }

        private void SetValueAsThreeBytes(UInt32 value)
        {
            UInt32 skipped = value >> 24;
            if (skipped > 0) value -= skipped;
            Data[2] = (Byte) (value >> 16);
            value -= (UInt32) Data[2] << 16;
            Data[1] = (Byte) (value >> 8);
            value -= (UInt32) Data[1] << 8;
            Data[0] = (Byte) value;
        }

        private void SetValueAsFourBytes(UInt32 value)
        {
            Data[3] = (Byte) (value >> 24);
            value -= (UInt32) Data[3] >> 24;
            Data[2] = (Byte) (value >> 16);
            value -= (UInt32) Data[2] << 16;
            Data[1] = (Byte) (value >> 8);
            value -= (UInt32) Data[1] << 8;
            Data[0] = (Byte) value;
        }

        private void SetValueAsSixBytes(UInt64 value)
        {
            Data[5] = (Byte) (value >> 40);
            value -= (UInt64) Data[5] << 40;
            Data[4] = (Byte) (value >> 32);
            value -= (UInt64) Data[4] << 32;
            Data[3] = (Byte) (value >> 24);
            value -= (UInt64) Data[3] << 24;
            Data[2] = (Byte) (value >> 16);
            value -= (UInt64) Data[2] << 16;
            Data[1] = (Byte) (value >> 8);
            value -= (UInt64) Data[1] << 8;
            Data[0] = (Byte) value;
        }

        private void SetValueAsEightBytes(UInt64 value)
        {
            Data[7] = (Byte) (value >> 56);
            value -= (UInt64) Data[7] << 56;
            Data[6] = (Byte) (value >> 48);
            value -= (UInt64) Data[6] << 48;
            Data[5] = (Byte) (value >> 40);
            value -= (UInt64) Data[5] << 40;
            Data[4] = (Byte) (value >> 32);
            value -= (UInt64) Data[4] << 32;
            Data[3] = (Byte) (value >> 24);
            value -= (UInt64) Data[3] << 24;
            Data[2] = (Byte) (value >> 16);
            value -= (UInt64) Data[2] << 16;
            Data[1] = (Byte) (value >> 8);
            value -= (UInt64) Data[1] << 8;
            Data[0] = (Byte) value;
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> struct.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="bitOffset">The bit offset.</param>
        /// <param name="pixelFormat">The pixel format.</param>
        public Pixel(Byte[] data, Int32 x, Int32 y, Int32 bitOffset, PixelFormat pixelFormat) : this()
        {
            X = x;
            Y = y;
            Data = data;
            BitOffset = bitOffset;
            BitDepth = pixelFormat.GetBitDepth();
            Format = pixelFormat;
        }

        #endregion
    }
}


