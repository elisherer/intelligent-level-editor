using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Quantizers;

namespace SimplePaletteQuantizer.Extensions
{
    /// <summary>
    /// The utility extender class.
    /// </summary>
    public static partial class Extend
    {
        #region | Locking methods |

        /// <summary>
        /// Locks the image data in a given access mode.
        /// </summary>
        /// <param name="image">The source image containing the data.</param>
        /// <param name="lockMode">The lock mode (see <see cref="ImageLockMode"/> for more details).</param>
        /// <returns>The locked image data reference.</returns>
        public static BitmapData LockBits(this Image image, ImageLockMode lockMode)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot lock the bits for a null image.";
                throw new ArgumentNullException(message);
            }

            // determines the bounds of an image, and locks the data in a specified mode
            Bitmap bitmap = (Bitmap)image;
            Rectangle bounds = Rectangle.FromLTRB(0, 0, image.Width, image.Height);
            BitmapData result = bitmap.LockBits(bounds, lockMode, image.PixelFormat);
            return result;
        }

        /// <summary>
        /// Unlocks the data for a given image.
        /// </summary>
        /// <param name="image">The image containing the data.</param>
        /// <param name="data">The data belonging to the image.</param>
        public static void UnlockBits(this Image image, BitmapData data)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot unlock the bits for a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if data to be unlocked are valid
            if (data == null)
            {
                const String message = "Cannot unlock null image data.";
                throw new ArgumentNullException(message);
            }

            // releases a lock
            Bitmap bitmap = (Bitmap)image;
            bitmap.UnlockBits(data);
        }

        #endregion

        #region | Processing methods |

        /// <summary>
        /// Processes the image pixels in memory.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="accessMode">The access mode.</param>
        /// <param name="processPixel">The process pixel.</param>
        public static void ProcessImagePixels(this Image sourceImage, ImageLockMode accessMode, Action<Pixel> processPixel)
        {
            // checks whether a source image is valid
            if (sourceImage == null)
            {
                const String message = "Cannot process the pixels for a null image.";
                throw new ArgumentNullException("sourceImage", message);
            }

            // locks the image data
            BitmapData data = sourceImage.LockBits(accessMode);
            PixelFormat pixelFormat = sourceImage.PixelFormat;

            try
            {
                // store processing variables
                Boolean canRead = accessMode == ImageLockMode.ReadOnly || accessMode == ImageLockMode.ReadWrite;
                Boolean canWrite = accessMode == ImageLockMode.WriteOnly || accessMode == ImageLockMode.ReadWrite;

                // source image - calculates all the values necessary for enumeration
                Byte bitDepth = pixelFormat.GetBitDepth();
                Int32 byteLength = data.Stride < 0 ? -data.Stride : data.Stride;
                Int32 byteCount = Math.Max(1, bitDepth >> 3);
                Int32 imageWidth = sourceImage.Width;
                Int32 imageHeight = sourceImage.Height;
                Int32 size = byteLength * imageHeight;
                Int32 offset = 0;

                // initializes the transfer buffers, and current pixel offset
                Byte[] buffer = new Byte[size];
                Byte[] value = new Byte[byteCount];

                // transfers whole image to a memory
                Marshal.Copy(data.Scan0, buffer, 0, size);

                // enumerates the pixels row by row
                for (Int32 y = 0; y < imageHeight; y++)
                {
                    // aquires the pointer to the first row pixel
                    Int32 index = 0;

                    // enumerates the buffer per pixel
                    for (Int32 x = 0; x < imageWidth; x++)
                    {
                        Int32 indexOffset = index >> 3;

                        // when read is allowed, retrieves current value (in bytes)
                        if (canRead)
                        {
                            for (Int32 valueIndex = 0; valueIndex < byteCount; valueIndex++)
                            {
                                value[valueIndex] = buffer[offset + valueIndex + indexOffset];
                            }
                        }

                        // enumerates the pixel, and returns the control to the outside
                        Pixel pixel = new Pixel(value, x, y, index % 8, pixelFormat);

                        processPixel(pixel);

                        // when write is allowed, copies the value back to the row buffer
                        if (canWrite)
                        {
                            for (Int32 valueIndex = 0; valueIndex < byteCount; valueIndex++)
                            {
                                buffer[offset + valueIndex + indexOffset] = value[valueIndex];
                            }
                        }

                        index += bitDepth;
                    }

                    // increases offset by a row
                    offset += byteLength;
                }

                Marshal.Copy(buffer, 0, data.Scan0, size);
            }
            finally
            {
                // releases the lock on the image data
                sourceImage.UnlockBits(data);
            }
        }

        /// <summary>
        /// Processes the image pixels in memory.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="targetImage">The target image.</param>
        /// <param name="processPixel">The process pixel.</param>
        public static void ProcessImagePixels(this Image sourceImage, Image targetImage, Action<Pixel, Pixel> processPixel)
        {
            // checks whether a source image is valid
            if (sourceImage == null)
            {
                const String message = "Cannot process the pixels for a null image.";
                throw new ArgumentNullException("sourceImage", message);
            }

            // checks whether a target image is valid
            if (targetImage == null)
            {
                const String message = "Cannot output the pixels to a null image.";
                throw new ArgumentNullException("targetImage", message);
            }

            // locks the image data
            BitmapData sourceData = sourceImage.LockBits(ImageLockMode.ReadOnly);
            BitmapData targetData = targetImage.LockBits(ImageLockMode.WriteOnly);
            
            // determines pixels formats
            PixelFormat sourcePixelFormat = sourceImage.PixelFormat;
            PixelFormat targetPixelFormat = targetImage.PixelFormat;

            try
            {
                // source image - calculates all the values necessary for enumeration
                Byte sourceBitDepth = sourcePixelFormat.GetBitDepth();
                Int32 sourceByteLength = sourceData.Stride < 0 ? -sourceData.Stride : sourceData.Stride;
                Int32 sourceByteCount = Math.Max(1, sourceBitDepth >> 3);
                Int32 sourceImageWidth = sourceImage.Width;
                Int32 sourceImageHeight = sourceImage.Height;
                Int32 sourceSize = sourceByteLength * sourceImageHeight;
                Int32 sourceOffset = 0;

                // target image - calculates all the values necessary for enumeration
                Byte targetBitDepth = targetPixelFormat.GetBitDepth();
                Int32 targetByteLength = targetData.Stride < 0 ? -targetData.Stride : targetData.Stride;
                Int32 targetByteCount = Math.Max(1, targetBitDepth >> 3);
                Int32 targetImageHeight = targetImage.Height;
                Int32 targetSize = targetByteLength * targetImageHeight;
                Int32 targetOffset = 0;

                // initializes the transfer buffers, and current pixel offset
                Byte[] sourceBuffer = new Byte[sourceSize];
                Byte[] targetBuffer = new Byte[targetSize];
                Byte[] sourceValue = new Byte[sourceByteCount];
                Byte[] targetValue = new Byte[targetByteCount];

                // transfers whole image to a memory
                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceSize);

                // enumerates the pixels row by row
                for (Int32 y = 0; y < sourceImageHeight; y++)
                {
                    // aquires the pointer to the first row pixel
                    Int32 sourceIndex = 0, targetIndex = 0;

                    // enumerates the buffer per pixel
                    for (Int32 x = 0; x < sourceImageWidth; x++)
                    {
                        Int32 sourceIndexOffset = sourceIndex >> 3;
                        Int32 targetIndexOffset = targetIndex >> 3;

                        // when read is allowed, retrieves current value (in bytes)
                        for (Int32 valueIndex = 0; valueIndex < sourceByteCount; valueIndex++)
                        {
                            sourceValue[valueIndex] = sourceBuffer[sourceOffset + valueIndex + sourceIndexOffset];
                        }

                        // enumerates the pixel, and returns the control to the outside
                        Pixel sourcePixel = new Pixel(sourceValue, x, y, sourceIndex % 8, sourcePixelFormat);
                        Pixel targetPixel = new Pixel(targetValue, x, y, targetIndex % 8, targetPixelFormat);
                        
                        processPixel(sourcePixel, targetPixel);

                        // when write is allowed, copies the value back to the row buffer
                        for (Int32 valueIndex = 0; valueIndex < targetByteCount; valueIndex++)
                        {
                            targetBuffer[targetOffset + valueIndex + targetIndexOffset] = targetValue[valueIndex];
                        }

                        sourceIndex += sourceBitDepth;
                        targetIndex += targetBitDepth;
                    }

                    // increases offset by a row
                    sourceOffset += sourceByteLength;
                    targetOffset += targetByteLength;
                }

                Marshal.Copy(targetBuffer, 0, targetData.Scan0, targetSize);
            }
            finally
            {
                // releases the lock on the image data
                sourceImage.UnlockBits(sourceData);
                targetImage.UnlockBits(targetData);
            }
        }

        #endregion

        #region | Quantizer scan |

        /// <summary>
        /// Adds all the colors from a source image to a given color quantizer.
        /// </summary>
        /// <param name="image">The image to be processed.</param>
        /// <param name="quantizer">The target color quantizer.</param>
        public static void AddColorsToQuantizer(this Image image, IColorQuantizer quantizer)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot add colors from a null image.";
                throw new ArgumentNullException(message);
            }

            // checks whether the quantizer is valid
            if (quantizer == null)
            {
                const String message = "Cannot add colors to a null quantizer.";
                throw new ArgumentNullException(message);
            }

            // determines which method of color retrieval to use
            Boolean isImageIndexed = image.PixelFormat.IsIndexed();
            ColorPalette palette = image.Palette;

            // use different scanning method depending whether the image format is indexed
            Action<Pixel> scan = isImageIndexed ? (Action<Pixel>)
                (pixel => quantizer.AddColor(palette.Entries[pixel.Index])) : 
                (pixel => quantizer.AddColor(pixel.Color));

            // performs the image scan, using a chosen method
            image.ProcessImagePixels(ImageLockMode.ReadOnly, scan);
        }

        #endregion

        #region | Change format |

        /// <summary>
        /// Changes the pixel format.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="targetFormat">The target format.</param>
        /// <param name="quantizer">The color quantizer.</param>
        /// <returns>The converted image in a target format.</returns>
        public static Image ChangePixelFormat(this Image image, PixelFormat targetFormat, IColorQuantizer quantizer)
        {
            // checks for image validity
            if (image == null)
            {
                const String message = "Cannot change a pixel format for a null image.";
                throw new ArgumentNullException(message);
            }

            // checks whether a target format is supported
            if (!targetFormat.IsSupported())
            {
                String message = string.Format("A pixel format '{0}' is not supported.", targetFormat);
                throw new NotSupportedException(message);
            }

            // checks whether there is a quantizer for a indexed format
            if (targetFormat.IsIndexed() && quantizer == null)
            {
                String message = string.Format("A quantizer is cannot be null for indexed pixel format '{0}'.", targetFormat);
                throw new NotSupportedException(message);
            }

            // creates an image with the target format
            Bitmap result = new Bitmap(image.Width, image.Height, targetFormat);
            ColorPalette imagePalette = image.Palette;

            // gathers some information about the target format
            Boolean hasSourceAlpha = image.PixelFormat.HasAlpha();
            Boolean hasTargetAlpha = targetFormat.HasAlpha();
            Boolean isSourceIndexed = image.PixelFormat.IsIndexed();
            Boolean isTargetIndexed = targetFormat.IsIndexed();
            Boolean isSourceDeepColor = image.PixelFormat.IsDeepColor();
            Boolean isTargetDeepColor = targetFormat.IsDeepColor();

            // if palette is needed create one first
            if (isTargetIndexed)
            {
                quantizer.Prepare(image);
                image.AddColorsToQuantizer(quantizer);
                Int32 targetColorCount = result.GetPaletteColorCount();
                List<Color> palette = quantizer.GetPalette(targetColorCount);
                result.SetPalette(palette);
            }

            Action<Pixel, Pixel> changeFormat = (sourcePixel, targetPixel) =>
            {
                // if both source and target formats are deep color formats, copies a value directly
                if (isSourceDeepColor && isTargetDeepColor)
                {
                    UInt64 value = sourcePixel.Value;
                    targetPixel.SetValue(value);
                }
                else
                {
                    // retrieves a source image color
                    Color color = isSourceIndexed ? imagePalette.Entries[sourcePixel.Index] : sourcePixel.Color;

                    // if alpha is not present in the source image, but is present in the target, make one up
                    if (!hasSourceAlpha && hasTargetAlpha)
                    {
                        Int32 argb = 255 << 24 | color.R << 16 | color.G << 8 | color.B;
                        color = Color.FromArgb(argb);
                    }

                    // sets the color to a target pixel
                    if (isTargetIndexed)
                    {
                        // for the indexed images, determines a color from the octree
                        Byte paletteIndex = (Byte) quantizer.GetPaletteIndex(color);
                        targetPixel.SetIndex(paletteIndex);
                    }
                    else
                    {
                        // for the non-indexed images, sets the color directly
                        targetPixel.SetColor(color);
                    }
                }
            };

            // process image -> changes format
            image.ProcessImagePixels(result, changeFormat);

            // returns the image in the target format
            return result;
        }

        #endregion

        #region | Palette methods |

        /// <summary>
        /// Gets the palette color count.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static Int32 GetPaletteColorCount(this Image image)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has an indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot retrieve a color count from a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // returns the color count
            return image.Palette.Entries.Length;
        }

        /// <summary>
        /// Gets the palette of an indexed image.
        /// </summary>
        /// <param name="image">The source image.</param>
        public static List<Color> GetPalette(this Image image)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has an indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot retrieve a palette from a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // retrieves and returns the palette
            return image.Palette.Entries.ToList();
        }

        /// <summary>
        /// Sets the palette of an indexed image.
        /// </summary>
        /// <param name="image">The target image.</param>
        /// <param name="palette">The palette.</param>
        public static void SetPalette(this Image image, List<Color> palette)
        {
            // checks whether a palette is valid
            if (palette == null)
            {
                const String message = "Cannot assign a null palette.";
                throw new ArgumentNullException(message);
            }

            // checks whether a target image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot store a palette to a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // retrieves a target image palette
            ColorPalette imagePalette = image.Palette;

            // checks if the palette can fit into the image palette
            if (palette.Count > imagePalette.Entries.Length)
            {
                String message = string.Format("Cannot store a palette with '{0}' colors intto an image palette where only '{1}' colors are allowed.", palette.Count, imagePalette.Entries.Length);
                throw new ArgumentOutOfRangeException(message);
            }

            // copies all color entries
            for (Int32 index = 0; index < palette.Count; index++)
            {
                imagePalette.Entries[index] = palette[index];
            }

            // assigns the palette to the target image
            image.Palette = imagePalette;
        }

        #endregion
    }
}