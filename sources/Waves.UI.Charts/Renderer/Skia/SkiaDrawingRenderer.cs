using System;
using System.Collections.Generic;
using System.Drawing;
using SkiaSharp;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Extensions;
using Waves.UI.Charts.Renderer.Skia.Extensions;
using static System.GC;

namespace Waves.UI.Charts.Renderer.Skia;

/// <summary>
///     Skia drawing element.
/// </summary>
public class SkiaDrawingRenderer : IWavesDrawingRenderer
{
    private SKSurface _surface;
    private SKCanvas _canvas;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    /// <inheritdoc />
    public void Update(object element, IEnumerable<IWavesDrawingObject> objects)
    {
        if (element is not SKCanvas canvas)
        {
            return;
        }

        if (_canvas == null || _canvas.Handle == IntPtr.Zero)
        {
            _canvas = canvas;
        }

        _canvas.Clear(new SKColor(0, 0, 0, 255));

        if (objects == null)
        {
            return;
        }

        foreach (var obj in objects)
        {
            obj.Draw(this);
        }
    }

    /// <inheritdoc />
    public void Draw(WavesLine line)
    {
        using var skPaint = new SKPaint
        {
            Color = line.Fill.ToSkColor((float)line.Opacity),
            StrokeWidth = (float)line.StrokeThickness,
            IsAntialias = line.IsAntialiased,
            IsStroke = Math.Abs(line.StrokeThickness) > float.Epsilon,
        };

        if (line.DashPattern != null)
        {
            var dashEffect = SKPathEffect.CreateDash(line.DashPattern.ToFloat(), 0);
            skPaint.PathEffect = skPaint.PathEffect != null
                ? SKPathEffect.CreateCompose(dashEffect, skPaint.PathEffect)
                : dashEffect;
        }

        _canvas.DrawLine(line.Point1.ToSkPoint(), line.Point2.ToSkPoint(), skPaint);
    }

    /// <inheritdoc />
    public void Draw(WavesText text)
    {
        using var skPaint = GetSkiaTextPaint(text);
        _canvas.DrawText(text.Value, text.Location.ToSkPoint(), skPaint);
    }

    /// <inheritdoc />
    public WavesSize MeasureText(string text, IWavesTextStyle style)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Dispose object.
    /// </summary>
    /// <param name="disposing">Disposing or not.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _canvas.Dispose();
        }
    }

    /// <summary>
    /// Measures text.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <returns>Returns size.</returns>
    private (float, float) MeasureText(WavesText text)
    {
        using var skPaint = GetSkiaTextPaint(text);

        var bounds = new SKRect();
        skPaint.MeasureText(text.Value, ref bounds);

        return new ValueTuple<float, float>(bounds.Width, bounds.Height);
    }

    /// <summary>
    ///     Gets Skia text paint.
    /// </summary>
    /// <param name="text">Waves text.</param>
    /// <returns>Skia text paint.</returns>
    private SKPaint GetSkiaTextPaint(WavesText text)
    {
        return new SKPaint
        {
            TextSize = text.Style.FontSize,
            Color = text.Fill.ToSkColor(),
            IsStroke = false,
            SubpixelText = true,
            IsAntialias = true,
            TextAlign = text.Style.TextAlignment.ToSkTextAlign(),
            Typeface = SKTypeface.FromFamilyName(text.Style.FontFamily),
        };
    }
}
