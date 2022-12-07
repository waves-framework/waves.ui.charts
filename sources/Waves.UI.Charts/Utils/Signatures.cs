using System;
using System.Collections.Generic;
using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
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
    /// <param name="ticks">Axis ticks.</param>
    /// <param name="cache">Axis signatures drawing objects cache.</param>
    /// <returns>Returns cache.</returns>
    public static ICollection<IWavesDrawingObject> GenerateAxisSignaturesDrawingObjects(
        this IWavesChart chart,
        IWavesDrawingRenderer renderer,
        List<WavesAxisTick> ticks,
        List<IWavesDrawingObject> cache)
    {
        if (chart.DrawingObjects is null)
        {
            throw new Exception("Collection of drawing object has not been initialized.");
        }

        var currentXMin = Values.GetValue(chart.CurrentXMin);
        var currentXMax = Values.GetValue(chart.CurrentXMax);
        var currentYMin = Values.GetValue(chart.CurrentYMin);
        var currentYMax = Values.GetValue(chart.CurrentYMax);

        foreach (var obj in cache)
        {
            chart.DrawingObjects.Remove(obj);
        }

        cache.Clear();

        var horizontalTextStyle = new WavesTextStyle { TextAlignment = WavesTextAlignment.Center };
        var verticalTextStyle = new WavesTextStyle { TextAlignment = WavesTextAlignment.Center };

        foreach (var tick in ticks)
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
                        currentXMin,
                        currentXMax,
                        chart.SurfaceWidth,
                        chart.SurfaceWidth,
                        chart.HorizontalSignatureAlignment);
                    rectangle = GetXAxisSignatureRectangle(
                        tick.Value,
                        chart.BackgroundColor,
                        chart.TextColor,
                        currentXMin,
                        currentXMax,
                        chart.SurfaceWidth,
                        chart.SurfaceWidth,
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
                        currentYMin,
                        currentYMax,
                        chart.SurfaceWidth,
                        chart.SurfaceWidth,
                        chart.VerticalSignatureAlignment);
                    rectangle = GetYAxisSignatureRectangle(
                        tick.Value,
                        chart.BackgroundColor,
                        chart.TextColor,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        chart.SurfaceWidth,
                        chart.SurfaceWidth,
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
    /// Generates point ticks.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="cache">Cache.</param>
    /// <param name="point">Pointer location.</param>
    /// <param name="dashArray">Dash array.</param>
    /// <param name="strokeThickness">Stroke thickness.</param>
    /// <param name="opacity">Opacity.</param>
    public static void GetPointerSignatures(
        this IWavesChart chart,
        List<IWavesDrawingObject> cache,
        WavesPoint point,
        double[] dashArray,
        double strokeThickness = 1d,
        double opacity = 0.5d)
    {
        var currentXMin = Values.GetValue(chart.CurrentXMin);
        var currentXMax = Values.GetValue(chart.CurrentXMax);
        var currentYMin = Values.GetValue(chart.CurrentYMin);
        var currentYMax = Values.GetValue(chart.CurrentYMax);

        var text1Value = Valuation.DenormalizeValueX(point.X, chart.SurfaceWidth, currentXMin, currentXMax);
        var text2Value = Valuation.DenormalizeValueY(point.Y, chart.SurfaceHeight, currentYMin, currentYMax);

        var (text1, text1Size) = GetXAxisSignature(
            chart.Renderer,
            text1Value,
            text1Value.ToString(),
            chart.TextColor,
            chart.TextStyle,
            currentXMin,
            currentXMax,
            chart.SurfaceWidth,
            chart.SurfaceHeight);

        var (text2, text2Size) = GetYAxisSignature(
            chart.Renderer,
            text2Value,
            text2Value.ToString(),
            chart.TextColor,
            chart.TextStyle,
            currentYMin,
            currentYMax,
            chart.SurfaceWidth,
            chart.SurfaceHeight);

        var text1Rectangle = GetXAxisSignatureRectangle(
            text1Value,
            chart.BackgroundColor,
            chart.TextColor,
            currentXMin,
            currentXMax,
            chart.SurfaceWidth,
            chart.SurfaceHeight,
            text1Size,
            WavesAxisHorizontalSignatureAlignment.Bottom);

        var text2Rectangle = GetYAxisSignatureRectangle(
            text2Value,
            chart.BackgroundColor,
            chart.TextColor,
            currentYMin,
            currentYMax,
            chart.SurfaceWidth,
            chart.SurfaceHeight,
            text1Size,
            WavesAxisVerticalSignatureAlignment.Right);

        chart.DrawingObjects?.Add(text1Rectangle);
        chart.DrawingObjects?.Add(text1);
        chart.DrawingObjects?.Add(text2Rectangle);
        chart.DrawingObjects?.Add(text2);
        cache.Add(text1Rectangle);
        cache.Add(text1);
        cache.Add(text2Rectangle);
        cache.Add(text2);
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
    private static (WavesText, WavesSize) GetXAxisSignature(
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
            Color = fill,
        };
        var size = renderer.MeasureText(obj);
        double x, y;
        switch (horizontalSignatureAlignment)
        {
            case WavesAxisHorizontalSignatureAlignment.Top:
                x = Valuation.NormalizeValueX(value, width, xMin, xMax) - size.Width / 2;
                y = size.Height * 2;
                break;
            case WavesAxisHorizontalSignatureAlignment.Bottom:
                x = Valuation.NormalizeValueX(value, width, xMin, xMax) - size.Width / 2;
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
        WavesAxisVerticalSignatureAlignment verticalSignatureAlignment = WavesAxisVerticalSignatureAlignment.Right,
        double opacity = 1)
    {
        var obj = new WavesText
        {
            Style = style,
            Value = description,
            IsVisible = true,
            IsAntialiased = true,
            Opacity = opacity,
            Color = fill,
        };

        var size = renderer.MeasureText(obj);
        double x, y;
        switch (verticalSignatureAlignment)
        {
            case WavesAxisVerticalSignatureAlignment.Left:
                x = size.Width;
                y = Valuation.NormalizeValueY(value, height, yMin, yMax) - size.Height / 2;
                break;
            case WavesAxisVerticalSignatureAlignment.Right:
                x = width - 1.5 * size.Width - 12;
                y = Valuation.NormalizeValueY(value, height, yMin, yMax) - size.Height / 2;
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
    private static WavesRectangle GetXAxisSignatureRectangle(
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
                x = Valuation.NormalizeValueX(value, width, xMin, xMax) - textSize.Width / 2;
                y = textSize.Height * 2;
                break;
            case WavesAxisHorizontalSignatureAlignment.Bottom:
                x = Valuation.NormalizeValueX(value, width, xMin, xMax) - textSize.Width / 2;
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
    private static WavesRectangle GetYAxisSignatureRectangle(
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
                y = Valuation.NormalizeValueY(value, height, yMin, yMax) - textSize.Height / 2;
                break;
            case WavesAxisVerticalSignatureAlignment.Right:
                x = width - 1.5 * textSize.Width - 12;
                y = Valuation.NormalizeValueY(value, height, yMin, yMax) - textSize.Height / 2;
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
