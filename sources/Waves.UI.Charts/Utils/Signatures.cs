using System;
using System.Drawing;
using System.Globalization;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Utils;

/// <summary>
///     Chart signatures generation.
/// </summary>
public static class Signatures
{
    // public static WavesText GetXAxisSignature(
    //     double value,
    //     string description,
    //     Color fill,
    //     WavesTextStyle style,
    //     double opacity,
    //     double xMin,
    //     double xMax,
    //     double width,
    //     double height)
    // {
    //     return new WavesText
    //     {
    //         Location = new WavesPoint(Valuation.NormalizePointX2D(value, width, xMin, xMax), height),
    //         Style = style,
    //         Value = description,
    //         IsVisible = true,
    //         IsAntialiased = true,
    //         Opacity = opacity,
    //         Fill = fill
    //     };
    // }
    //
    // public static WavesText GetXAxisSignature(
    //     double value,
    //     string description,
    //     WavesTextStyle style,
    //     Color fill,
    //     double xMin,
    //     double xMax,
    //     double width,
    //     double height,
    //     double opacity)
    // {
    //     return new WavesText
    //     {
    //         Location = new WavesPoint(Valuation.NormalizePointX2D(value, width, xMin, xMax), height - 14),
    //         Style = style,
    //         Value = description,
    //         IsVisible = true,
    //         IsAntialiased = true,
    //         Fill = fill,
    //         Opacity = opacity
    //     };
    // }
    //
    // public static WavesText GetYAxisSignature(
    //     double value,
    //     string description,
    //     WavesTextStyle style,
    //     Color fill,
    //     double opacity,
    //     double yMin,
    //     double yMax,
    //     double height)
    // {
    //     return new WavesText()
    //     {
    //         Location = new Point(12, Valuation.NormalizePointY2D(value, height, yMin, yMax)),
    //         Style = style,
    //         Value = description,
    //         IsVisible = true,
    //         IsAntialiased = true,
    //         Opacity = opacity,
    //         Fill = fill
    //     };
    // }
    //
    // public static WavesText GetYAxisSignature(
    //     double value,
    //     string description,
    //     TextPaint paint,
    //     double yMin,
    //     double yMax,
    //     double height)
    // {
    //     return new WavesText
    //     {
    //         Location = new Point(13, Valuation.NormalizePointY2D(value, height, yMin, yMax)),
    //         Style = paint.TextStyle,
    //         Value = description,
    //         IsVisible = true,
    //         IsAntialiased = true,
    //         Fill = paint.Fill,
    //         Opacity = paint.Opacity
    //     };
    // }
    //
    // public static Rectangle GetXAxisSignatureRectangle(
    //     double value,
    //     Color fill,
    //     Color stroke,
    //     double strokeThickness,
    //     double opacity,
    //     double cornerRadius,
    //     double innerTextWidth,
    //     double innerTextHeight,
    //     double xMin,
    //     double xMax,
    //     double width,
    //     double height)
    // {
    //     return new Rectangle
    //     {
    //         Location = new Point(
    //             Valuation.NormalizePointX2D(value, width, xMin, xMax) - innerTextWidth / 2 - 6,
    //             height - innerTextHeight - 12 - 6),
    //         CornerRadius = cornerRadius,
    //         Opacity = opacity,
    //         Fill = fill,
    //         Stroke = stroke,
    //         StrokeThickness = strokeThickness,
    //         Width = innerTextWidth + 12,
    //         Height = innerTextHeight + 12,
    //         IsVisible = true,
    //         IsAntialiased = true
    //     };
    // }
    //
    // public static Rectangle GetYAxisSignatureRectangle(
    //     double value,
    //     Color fill,
    //     Color stroke,
    //     double strokeThickness,
    //     double opacity,
    //     double cornerRadius,
    //     double innerTextWidth,
    //     double innerTextHeight,
    //     double yMin,
    //     double yMax,
    //     double height)
    // {
    //     return new Rectangle
    //     {
    //         Location = new Point(
    //             6,
    //             Valuation.NormalizePointY2D(value, height, yMin, yMax) - innerTextHeight / 2 - 6),
    //         CornerRadius = cornerRadius,
    //         Fill = fill,
    //         Stroke = stroke,
    //         StrokeThickness = strokeThickness,
    //         Width = innerTextWidth + 12,
    //         Height = innerTextHeight + 12,
    //         IsVisible = true,
    //         IsAntialiased = true,
    //         Opacity = opacity
    //     };
    // }
    //
    // /// <summary>
    // ///     Optimizes value for view.
    // /// </summary>
    // /// <param name="value">Value.</param>
    // /// <returns>Optimized value.</returns>
    // public static string OptimizeNumericString(double value)
    // {
    //     var numbers = value.ToString(CultureInfo.InvariantCulture).Split('.');
    //
    //     if (numbers.Length > 1)
    //     {
    //         if (numbers[1].Length >= 0 && numbers.Length < 12)
    //             return Math.Round(value, 3).ToString(CultureInfo.InvariantCulture);
    //
    //         if (numbers[1].Length >= 12 && numbers.Length < 24)
    //             return Math.Round(value, 6).ToString(CultureInfo.InvariantCulture);
    //     }
    //
    //     return value.ToString(CultureInfo.InvariantCulture);
    // }
}
