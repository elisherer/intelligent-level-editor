using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimplePaletteQuantizer.Quantizers
{
    /// <summary>
    /// This interface provides a color quantization capabilities.
    /// </summary>
    public interface IColorQuantizer
    {
        /// <summary>
        /// Prepares the quantizer for image processing.
        /// </summary>
        /// <param name="image">The image.</param>
        void Prepare(Image image);

        /// <summary>
        /// Adds the color to quantizer.
        /// </summary>
        /// <param name="color">The color to be added.</param>
        void AddColor(Color color);

        /// <summary>
        /// Gets the palette with specified count of the colors.
        /// </summary>
        /// <param name="colorCount">The color count.</param>
        /// <returns></returns>
        List<Color> GetPalette(Int32 colorCount);

        /// <summary>
        /// Gets the index of the palette for specific color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        Int32 GetPaletteIndex(Color color);

        /// <summary>
        /// Gets the color count.
        /// </summary>
        /// <returns></returns>
        Int32 GetColorCount();
    }
}