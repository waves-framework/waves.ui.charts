using System;
using System.Collections.Generic;
using SkiaSharp;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
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
    private SKCanvas _canvas;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    /// <inheritdoc />
    public void Update(object element, IEnumerable<IWavesDrawingObject> objects, WavesColor background)
    {
        if (element is not SKCanvas canvas)
        {
            return;
        }

        if (_canvas == null || _canvas.Handle == IntPtr.Zero)
        {
            _canvas = canvas;
        }

        _canvas.Clear(background.ToSkColor());

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
            Color = line.Color.ToSkColor((float)line.Opacity),
            StrokeWidth = (float)line.Thickness,
            IsAntialias = line.IsAntialiased,
            IsStroke = Math.Abs(line.Thickness) > float.Epsilon,
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
    public void Draw(WavesPolyline polyline)
    {
        using var skPaint = new SKPaint
        {
            Color = polyline.Color.ToSkColor((float)polyline.Opacity),
            StrokeWidth = (float)polyline.Thickness,
            IsAntialias = polyline.IsAntialiased,
            IsStroke = Math.Abs(polyline.Thickness) > float.Epsilon,
        };

        if (polyline.DashPattern != null)
        {
            var dashEffect = SKPathEffect.CreateDash(polyline.DashPattern.ToFloat(), 0);
            skPaint.PathEffect = skPaint.PathEffect != null
                ? SKPathEffect.CreateCompose(dashEffect, skPaint.PathEffect)
                : dashEffect;
        }

        var length = polyline.Points.Length;
        for (var i = 0; i < length - 1; i++)
        {
            _canvas.DrawLine(polyline.Points[i].ToSkPoint(), polyline.Points[i + 1].ToSkPoint(), skPaint);
        }
    }

    /// <inheritdoc />
    public void Draw(WavesRectangle rectangle)
    {
        using (var skPaint = new SKPaint
               {
                   Color = rectangle.Fill.ToSkColor((float)rectangle.Opacity),
                   IsAntialias = rectangle.IsAntialiased,
               })
        {
            _canvas.DrawRoundRect(
                (float)rectangle.Location.X,
                (float)rectangle.Location.Y,
                (float)rectangle.Width,
                (float)rectangle.Height,
                (float)rectangle.CornerRadius,
                (float)rectangle.CornerRadius,
                skPaint);
        }

        if (!(rectangle.StrokeThickness > 0))
        {
            return;
        }

        using (var skPaint = new SKPaint
               {
                   Color = rectangle.Stroke.ToSkColor((float)rectangle.Opacity),
                   IsAntialias = rectangle.IsAntialiased,
                   StrokeWidth = (float)rectangle.StrokeThickness,
                   IsStroke = true,
               })
        {
            _canvas.DrawRoundRect(
                (float)rectangle.Location.X,
                (float)rectangle.Location.Y,
                (float)rectangle.Width,
                (float)rectangle.Height,
                (float)rectangle.CornerRadius,
                (float)rectangle.CornerRadius,
                skPaint);
        }
    }

    /// <inheritdoc />
    public void Draw(WavesEllipse ellipse)
    {
        using (var skPaint = new SKPaint
               {
                   Color = ellipse.Fill.ToSkColor((float)ellipse.Opacity),
                   IsAntialias = ellipse.IsAntialiased,
               })
        {
            _canvas.DrawOval(
                (float)ellipse.Location.X,
                (float)ellipse.Location.Y,
                (float)ellipse.Width / 2,
                (float)ellipse.Height / 2,
                skPaint);
        }

        if (!(ellipse.StrokeThickness > 0))
        {
            return;
        }

        using (var skPaint = new SKPaint
               {
                   Color = ellipse.Stroke.ToSkColor((float)ellipse.Opacity),
                   IsAntialias = ellipse.IsAntialiased,
                   StrokeWidth = (float)ellipse.StrokeThickness,
                   IsStroke = true,
               })
        {
            _canvas.DrawOval(
                (float)ellipse.Location.X,
                (float)ellipse.Location.Y,
                (float)ellipse.Width / 2,
                (float)ellipse.Height / 2,
                skPaint);
        }
    }

    /// <inheritdoc />
    public void Draw(WavesText text)
    {
        using var skPaint = GetSkiaTextPaint(text);
        _canvas.DrawText(text.Value, text.Location.ToSkPoint(), skPaint);
    }

    /// <inheritdoc />
    public WavesSize MeasureText(WavesText text)
    {
        using var skPaint = GetSkiaTextPaint(text);

        var bounds = new SKRect();
        skPaint.MeasureText(text.Value, ref bounds);

        return new WavesSize(bounds.Width, bounds.Height);
    }

    /// <summary>
    ///     Dispose object.
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
    ///     Gets Skia text paint.
    /// </summary>
    /// <param name="text">Waves text.</param>
    /// <returns>Skia text paint.</returns>
    private SKPaint GetSkiaTextPaint(WavesText text)
    {
        return new SKPaint
        {
            TextSize = text.Style.FontSize,
            Color = text.Color.ToSkColor(),
            IsStroke = false,
            SubpixelText = true,
            IsAntialias = true,
            TextAlign = text.Style.TextAlignment.ToSkTextAlign(),
            Typeface = SKTypeface.FromFamilyName(text.Style.FontFamily),
        };
    }
}
