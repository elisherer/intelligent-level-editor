using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using SimplePaletteQuantizer.Extensions;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Quantizers.XiaolinWu;

namespace IntelligentLevelEditor.Games
{
    static class ImageImporter
    {
        public const string SupportedFiles =
            @"All Supported|*.png;*.jpg;*.bmp;*.gif|PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif";

        public static void Import(Bitmap srcBitmap, ColorPalette srcPalette, int srcPaletteSize, byte[][] dstBitmap, byte[] dstPalette, byte transparentIndex)
        {
            if (srcBitmap.Width > dstBitmap[0].Length || srcBitmap.Height > dstBitmap.Length)
            {
                var ratio = (double)srcBitmap.Width / srcBitmap.Height;
                int newWidth, newHeight;
                if (ratio > 1.0) //width > height
                {
                    newWidth = dstBitmap[0].Length;
                    newHeight = (int)(newWidth/ratio);
                }
                else //width <= height
                {
                    newHeight = dstBitmap.Length;
                    newWidth = (int)(newHeight * ratio);
                }
                srcBitmap = new Bitmap(srcBitmap, newWidth, newHeight);
            }
            var bmp = Quantize(srcBitmap, 8);
            var plt = bmp.Palette;
            //var pltImage = (Image)Resources.pushmoPalette.Clone();
            for (var i = 0; i < dstPalette.Length; i++) //translate palette
                dstPalette[i] = GetColorMatch(plt.Entries[i], srcPalette, srcPaletteSize);
            foreach (var cell in dstBitmap)
                for (var x = 0; x < cell.Length; x++)
                    cell[x] = transparentIndex;
            for (var y = 0; y < srcBitmap.Height; y++)
                for (var x = 0; x < srcBitmap.Width; x++)
                    if (srcBitmap.GetPixel(x, y).A > 0x7F)
                        dstBitmap[y][x] = (byte)(ByteIndexOf(bmp.GetPixel(x, y), plt) + (transparentIndex == 0 ? 1 : 0));
        }

        private static byte ByteIndexOf(Color clr, ColorPalette palette)
        {
            for (byte i = 0; i < 8; i++)
                if (palette.Entries[i].Equals(clr))
                    return i;
            return 0;
        }

        /*private static byte GetNearestBaseColor(Image plt, Color exactColor)
        {
            var g = Graphics.FromImage(plt);
            var myColor = g.GetNearestColor(exactColor);
            for (var i = 0; i < Pushmo.PushmoColorPalette.Entries.Length; i++)
                if (Pushmo.PushmoColorPalette.Entries[i].Equals(myColor))
                    return (byte)i;
            return 0;
        }*/

        private static byte GetColorMatch(Color col, ColorPalette palette, int maxColor)
        {
            byte colorMatch = 0;

            var leastDistance = int.MaxValue;

            var alpha = col.A;
            var red = col.R;
            var green = col.G;
            var blue = col.B;

            for (var i = 0; i < maxColor; i++)
            {
                var paletteColor = palette.Entries[i];

                var alphaDistance = paletteColor.A - alpha;
                var redDistance = paletteColor.R - red;
                var greenDistance = paletteColor.G - green;
                var blueDistance = paletteColor.B - blue;

                var distance = (alphaDistance * alphaDistance) +
                    (redDistance * redDistance) +
                    (greenDistance * greenDistance) +
                    (blueDistance * blueDistance);

                if (distance >= leastDistance) continue;
                colorMatch = (byte)i;
                leastDistance = distance;

                if (distance == 0)
                    return (byte)i;
            }

            return colorMatch;
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
