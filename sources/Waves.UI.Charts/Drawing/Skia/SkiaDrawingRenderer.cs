using System;
using System.Collections.Generic;
using SkiaSharp;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Drawing.Skia.Extensions;
using static System.GC;

namespace Waves.UI.Charts.Drawing.Skia;

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

        _canvas.Clear(SKColor.Empty);

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
            Color = line.Fill.ToSkColor(line.Opacity),
            StrokeWidth = line.StrokeThickness,
            IsAntialias = line.IsAntialiased,
            IsStroke = Math.Abs(line.StrokeThickness) > float.Epsilon,
        };

        if (line.DashPattern != null)
        {
            var dashEffect = SKPathEffect.CreateDash(line.DashPattern, 0);
            skPaint.PathEffect = skPaint.PathEffect != null
                ? SKPathEffect.CreateCompose(dashEffect, skPaint.PathEffect)
                : dashEffect;
        }

        _canvas.DrawLine(line.Point1.ToSkPoint(), line.Point1.ToSkPoint(), skPaint);
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
}
