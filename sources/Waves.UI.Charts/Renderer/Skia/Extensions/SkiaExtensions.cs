using System;
using System.Drawing;
using SkiaSharp;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;

namespace Waves.UI.Charts.Renderer.Skia.Extensions;

/// <summary>
///     Skia extensions.
/// </summary>
public static class SkiaExtensions
{
    /// <summary>
    ///     Converts color to Skia color.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>Returns Skia color.</returns>
    public static SKColor ToSkColor(this WavesColor color)
    {
        return new SKColor(color.R, color.G, color.B, color.A);
    }

    /// <summary>
    ///     Converts color to Skia color with current opacity.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <param name="opacity">Opacity.</param>
    /// <returns>Returns Skia color.</returns>
    public static SKColor ToSkColor(this WavesColor color, float opacity)
    {
        var a = Convert.ToByte(opacity * color.A);
        return new SKColor(color.R, color.G, color.B, a);
    }

    /// <summary>
    ///     Converts point to Skia point.
    /// </summary>
    /// <param name="point">Point.</param>
    /// <returns>Skia point.</returns>
    public static SKPoint ToSkPoint(this WavesPoint point)
    {
        return new SKPoint((float)point.X, (float)point.Y);
    }

    /// <summary>
    ///     Converts text alignment to SKTextAlign.
    /// </summary>
    /// <param name="alignment">Text alignment.</param>
    /// <returns>SKTextAlign.</returns>
    public static SKTextAlign ToSkTextAlign(this WavesTextAlignment alignment)
    {
        return alignment switch
        {
            WavesTextAlignment.Left => SKTextAlign.Left,
            WavesTextAlignment.Right => SKTextAlign.Right,
            WavesTextAlignment.Center => SKTextAlign.Center,
            _ => SKTextAlign.Left
        };
    }
}
