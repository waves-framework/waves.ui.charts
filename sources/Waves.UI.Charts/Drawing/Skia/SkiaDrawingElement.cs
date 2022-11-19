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
public class SkiaDrawingElement : IWavesDrawingElement
{
    private SKSurface _surface;
    private ICollection<IWavesDrawingObject> _drawingObjects;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }

    /// <inheritdoc />
    public void Update(object element)
    {
        if (element is not SKSurface surface)
        {
            return;
        }

        if (_surface == null || _surface.Handle == IntPtr.Zero)
        {
            _surface = surface;
        }

        _surface.Canvas.Clear(SKColor.Empty);

        if (_drawingObjects == null)
        {
            return;
        }

        foreach (var obj in _drawingObjects)
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

        _surface.Canvas.DrawLine(line.Point1.ToSkPoint(), line.Point1.ToSkPoint(), skPaint);
    }

    /// <summary>
    /// Dispose object.
    /// </summary>
    /// <param name="disposing">Disposing or not.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            SuppressFinalize(this);
        }

        _surface.Dispose();
    }
}
