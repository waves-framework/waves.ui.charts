using System;
using System.Collections.Generic;
using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Utils;

/// <summary>
///     Chart signatures generation.
/// </summary>
public static class Signatures
{
    /// <summary>
    /// Generates signatures drawing objects.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="renderer">Renderer.</param>
    /// <param name="axisTicks">Axis ticks.</param>
    /// <param name="cache">Axis signatures drawing objects cache.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <returns>Returns cache.</returns>
    public static ICollection<IWavesDrawingObject> GenerateAxisSignaturesDrawingObjects(
        this IWavesChart chart,
        IWavesDrawingRenderer renderer,
        List<WavesAxisTick> axisTicks,
        List<IWavesDrawingObject> cache,
        double width,
        double height)
    {
        if (chart.DrawingObjects is null)
        {
            throw new Exception("Collection of drawing object has not been initialized.");
        }

        foreach (var obj in cache)
        {
            chart.DrawingObjects.Remove(obj);
        }

        cache.Clear();

        var horizontalTextStyle = new WavesTextStyle { TextAlignment = WavesTextAlignment.Center };
        var verticalTextStyle = new WavesTextStyle { TextAlignment = WavesTextAlignment.Center };

        foreach (var tick in axisTicks)
        {
            if (tick.Type is WavesAxisTickType.Additional)
            {
                continue;
            }

            WavesText signature = null;
            WavesRectangle rectangle = null;
            WavesSize size;
            switch (tick.Orientation)
            {
                case WavesAxisTickOrientation.Horizontal:
                    (signature, size) = GetXAxisSignature(
                        renderer,
                        tick.Value,
                        tick.Description,
                        chart.TextColor,
                        horizontalTextStyle,
                        chart.CurrentXMin,
                        chart.CurrentXMax,
                        width,
                        height,
                        chart.HorizontalSignatureAlignment);
                    rectangle = GetXAxisSignatureRectangle(
                        tick.Value,
                        chart.BackgroundColor,
                        chart.TextColor,
                        chart.CurrentXMin,
                        chart.CurrentXMax,
                        width,
                        height,
                        size,
                        chart.HorizontalSignatureAlignment);
                    break;
                case WavesAxisTickOrientation.Vertical:
                    (signature, size) = GetYAxisSignature(
                        renderer,
                        tick.Value,
                        tick.Description,
                        chart.TextColor,
                        verticalTextStyle,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        width,
                        height,
                        chart.VerticalSignatureAlignment);
                    rectangle = GetYAxisSignatureRectangle(
                        tick.Value,
                        chart.BackgroundColor,
                        chart.TextColor,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        width,
                        height,
                        size,
                        chart.VerticalSignatureAlignment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (signature == null)
            {
                continue;
            }

            chart.DrawingObjects.Add(rectangle);
            cache.Add(rectangle);
            chart.DrawingObjects.Add(signature);
            cache.Add(signature);
        }

        return cache;
    }

    /// <summary>
    /// Generates X axis signature.
    /// </summary>
    /// <param name="renderer">Renderer.</param>
    /// <param name="value">Value.</param>
    /// <param name="description">Description.</param>
    /// <param name="fill">Fill.</param>
    /// <param name="style">Text style.</param>
    /// <param name="xMin">xMin.</param>
    /// <param name="xMax">xMax.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="horizontalSignatureAlignment">Horizontal signature alignment.</param>
    /// <param name="opacity">Opacity.</param>
    /// <returns>Returns X axis signature drawing object.</returns>
    public static (WavesText, WavesSize) GetXAxisSignature(
        IWavesDrawingRenderer renderer,
        double value,
        string description,
        WavesColor fill,
        WavesTextStyle style,
        double xMin,
        double xMax,
        double width,
        double height,
        WavesAxisHorizontalSignatureAlignment horizontalSignatureAlignment = WavesAxisHorizontalSignatureAlignment.Bottom,
        double opacity = 1.0f)
    {
        var obj = new WavesText
        {
            Style = style,
            Value = description,
            IsVisible = true,
            IsAntialiased = true,
            Opacity = opacity,
            Fill = fill,
        };
        var size = renderer.MeasureText(obj);
        double x, y;
        switch (horizontalSignatureAlignment)
        {
            case WavesAxisHorizontalSignatureAlignment.Top:
                x = Valuation.NormalizePointX2D(value, width, xMin, xMax) - size.Width / 2;
                y = size.Height * 2;
                break;
            case WavesAxisHorizontalSignatureAlignment.Bottom:
                x = Valuation.NormalizePointX2D(value, width, xMin, xMax) - size.Width / 2;
                y = height - size.Height * 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(horizontalSignatureAlignment), horizontalSignatureAlignment, null);
        }

        obj.Location = new WavesPoint(x, y);
        return (obj, size);
    }

    /// <summary>
    /// Generates Y axis signature.
    /// </summary>
    /// <param name="renderer">Renderer.</param>
    /// <param name="value">Value.</param>
    /// <param name="description">Description.</param>
    /// <param name="fill">Fill.</param>
    /// <param name="style">Text style.</param>
    /// <param name="yMin">yMin.</param>
    /// <param name="yMax">yMax.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="verticalSignatureAlignment">Vertical signature alignment.</param>
    /// <param name="opacity">Opacity.</param>
    /// <returns>Returns Y axis signature drawing object.</returns>
    public static (WavesText, WavesSize) GetYAxisSignature(
        IWavesDrawingRenderer renderer,
        double value,
        string description,
        WavesColor fill,
        WavesTextStyle style,
        double yMin,
        double yMax,
        double width,
        double height,
        WavesAxisVerticalSignatureAlignment verticalSignatureAlignment,
        double opacity = 1)
    {
        var obj = new WavesText
        {
            Style = style,
            Value = description,
            IsVisible = true,
            IsAntialiased = true,
            Opacity = opacity,
            Fill = fill,
        };

        var size = renderer.MeasureText(obj);
        double x, y;
        switch (verticalSignatureAlignment)
        {
            case WavesAxisVerticalSignatureAlignment.Left:
                x = size.Width;
                y = Valuation.NormalizePointY2D(value, height, yMin, yMax) - size.Height / 2;
                break;
            case WavesAxisVerticalSignatureAlignment.Right:
                x = width - size.Width * 2;
                y = Valuation.NormalizePointY2D(value, height, yMin, yMax) - size.Height / 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(verticalSignatureAlignment), verticalSignatureAlignment, null);
        }

        obj.Location = new WavesPoint(x, y);
        return (obj, size);
    }

    /// <summary>
    /// Gets X axis signatures background rectangle.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="fill">Fill.</param>
    /// <param name="stroke">Stroke.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="textSize">Text size.</param>
    /// <param name="horizontalSignatureAlignment">Horizontal signature alignment.</param>
    /// <param name="strokeThickness">Stroke thickness.</param>
    /// <param name="opacity">Opacity.</param>
    /// <param name="cornerRadius">Corner radius.</param>
    /// <returns>Returns X axis signatures background.</returns>
    public static WavesRectangle GetXAxisSignatureRectangle(
        double value,
        WavesColor fill,
        WavesColor stroke,
        double xMin,
        double xMax,
        double width,
        double height,
        WavesSize textSize,
        WavesAxisHorizontalSignatureAlignment horizontalSignatureAlignment,
        double strokeThickness = 1.0d,
        double opacity = 0.8d,
        double cornerRadius = 6.0d)
    {
        double x, y;
        switch (horizontalSignatureAlignment)
        {
            case WavesAxisHorizontalSignatureAlignment.Top:
                x = Valuation.NormalizePointX2D(value, width, xMin, xMax) - textSize.Width / 2;
                y = textSize.Height * 2;
                break;
            case WavesAxisHorizontalSignatureAlignment.Bottom:
                x = Valuation.NormalizePointX2D(value, width, xMin, xMax) - textSize.Width / 2;
                y = height - textSize.Height * 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(horizontalSignatureAlignment), horizontalSignatureAlignment, null);
        }

        const int marginX = 12; // TODO
        const int marginY = 6;  // TODO

        return new WavesRectangle
        {
            Location = new WavesPoint(x - (marginX / 2), y - (marginY / 2)),
            CornerRadius = cornerRadius,
            Opacity = opacity,
            Fill = fill,
            Stroke = stroke,
            StrokeThickness = strokeThickness,
            Width = textSize.Width + marginX,
            Height = textSize.Height + marginY,
            IsVisible = true,
            IsAntialiased = true,
        };
    }

    /// <summary>
    /// Gets Y axis signatures background rectangle.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="fill">Fill.</param>
    /// <param name="stroke">Stroke.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="yMax">Y max.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="textSize">Text size.</param>
    /// <param name="verticalSignatureAlignment">Horizontal signature alignment.</param>
    /// <param name="strokeThickness">Stroke thickness.</param>
    /// <param name="opacity">Opacity.</param>
    /// <param name="cornerRadius">Corner radius.</param>
    /// <returns>Returns Y axis signatures background.</returns>
    public static WavesRectangle GetYAxisSignatureRectangle(
        double value,
        WavesColor fill,
        WavesColor stroke,
        double yMin,
        double yMax,
        double width,
        double height,
        WavesSize textSize,
        WavesAxisVerticalSignatureAlignment verticalSignatureAlignment,
        double strokeThickness = 1.0d,
        double opacity = 0.8d,
        double cornerRadius = 6.0d)
    {
        double x, y;
        switch (verticalSignatureAlignment)
        {
            case WavesAxisVerticalSignatureAlignment.Left:
                x = textSize.Width;
                y = Valuation.NormalizePointY2D(value, height, yMin, yMax) - textSize.Height / 2;
                break;
            case WavesAxisVerticalSignatureAlignment.Right:
                x = width - textSize.Width * 2;
                y = Valuation.NormalizePointY2D(value, height, yMin, yMax) - textSize.Height / 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(verticalSignatureAlignment), verticalSignatureAlignment, null);
        }

        const int marginX = 12; // TODO
        const int marginY = 6;  // TODO

        return new WavesRectangle
        {
            Location = new WavesPoint(x - (marginX / 2), y - (marginY / 2)),
            CornerRadius = cornerRadius,
            Opacity = opacity,
            Fill = fill,
            Stroke = stroke,
            StrokeThickness = strokeThickness,
            Width = textSize.Width + marginX,
            Height = textSize.Height + marginY,
            IsVisible = true,
            IsAntialiased = true,
        };
    }
}
