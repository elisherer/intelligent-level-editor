using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using IntelligentLevelEditor.Properties;
using SimplePaletteQuantizer.Extensions;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Quantizers.XiaolinWu;

namespace IntelligentLevelEditor.Games.Pushmo
{
    class PushmoImageImport
    {
        public static void Import(Bitmap srcBitmap, byte[][] bitmap, byte[] palette)
        {
            if (srcBitmap.Width > Pushmo.BitmapSize || srcBitmap.Height > Pushmo.BitmapSize)
            {
                var ratio = (double)srcBitmap.Width / srcBitmap.Height;
                int newWidth, newHeight;
                if (ratio > 1.0) //width > height
                {
                    newWidth = Pushmo.BitmapSize;
                    newHeight = (int)(newWidth/ratio);
                }
                else //width <= height
                {
                    newHeight = Pushmo.BitmapSize;
                    newWidth = (int)(newHeight * ratio);
                }
                srcBitmap = new Bitmap(srcBitmap, newWidth, newHeight);
            }
            var bmp = Quantize(srcBitmap, 8);
            var plt = bmp.Palette;
            var pltImage = (Image)Resources.pushmoPalette.Clone();
            for (var i = 0; i < palette.Length; i++) //translate palette
                palette[i] = GetColorMatch(plt.Entries[i],Pushmo.PushmoColorPalette, Pushmo.PushmoColorPaletteSize);
            for (var y = 0; y < bitmap.Length; y++)
                for (var x = 0; x < bitmap[y].Length; x++)
                    bitmap[y][x] = 0xA;
            for (var y = 0; y < srcBitmap.Height; y++)
                for (var x = 0; x < srcBitmap.Width; x++)
                    if (srcBitmap.GetPixel(x, y).A > 0x7F)
                        bitmap[y][x] = ByteIndexOf(bmp.GetPixel(x, y), plt);
        }

        private static byte ByteIndexOf(Color clr, ColorPalette palette)
        {
            for (byte i = 0; i < 8; i++)
                if (palette.Entries[i].Equals(clr))
                    return i;
            return 0;
        }

        private static byte GetNearestBaseColor(Image plt, Color exactColor)
        {
            
            var g = Graphics.FromImage(plt);
            var myColor = g.GetNearestColor(exactColor);
            for (var i = 0; i < Pushmo.PushmoColorPalette.Entries.Length; i++)
                if (Pushmo.PushmoColorPalette.Entries[i].Equals(myColor))
                    return (byte)i;
            return 0;
        }

        private static byte GetColorMatch(Color col, ColorPalette palette, int maxColor)
        {
            byte ColorMatch = 0;

            int LeastDistance = int.MaxValue;

            int Alpha = col.A;
            int Red = col.R;
            int Green = col.G;
            int Blue = col.B;

            for (int i = 0; i < maxColor; i++)
            {
                Color PaletteColor = palette.Entries[i];

                int AlphaDistance = PaletteColor.A - Alpha;
                int RedDistance = PaletteColor.R - Red;
                int GreenDistance = PaletteColor.G - Green;
                int BlueDistance = PaletteColor.B - Blue;

                int Distance = (AlphaDistance * AlphaDistance) +
                    (RedDistance * RedDistance) +
                    (GreenDistance * GreenDistance) +
                    (BlueDistance * BlueDistance);

                if (Distance < LeastDistance)
                {
                    ColorMatch = (byte)i;
                    LeastDistance = Distance;

                    if (Distance == 0)
                        return (byte)i;
                }
            }

            return ColorMatch;
        }

        private static Bitmap Quantize(Image image,int colorCount)
        {
            var q = new WuColorQuantizer();
            q.Prepare(image);
            image.AddColorsToQuantizer(q);

            // creates a target bitmap in 8-bit format
            var isSourceIndexed = image.PixelFormat.IsIndexed();      

            var result = new Bitmap(image.Width, image.Height, PixelFormat.Format4bppIndexed);

            var sourcePalette = isSourceIndexed ? image.GetPalette() : new List<Color>();

            var palette = q.GetPalette(colorCount);
            result.SetPalette(palette);

            // moves to next pixel for both images
            Action<Pixel, Pixel> quantization = (sourcePixel, targetPixel) =>
            {
                var color = isSourceIndexed ? sourcePalette[sourcePixel.GetIndex()] : sourcePixel.Color;
                color = QuantizationHelper.ConvertAlpha(color);
                var paletteIndex = q.GetPaletteIndex(color);
                targetPixel.SetIndex((Byte)paletteIndex);
            };

            // processes the quantization
            image.ProcessImagePixels(result, quantization);
            return result;
        }
    }
}
