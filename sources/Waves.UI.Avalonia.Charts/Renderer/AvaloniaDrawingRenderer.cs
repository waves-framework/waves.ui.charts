using System.Globalization;
using Avalonia;
using Avalonia.Media;
using Waves.UI.Avalonia.Charts.Extensions;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Avalonia.Charts.Renderer;

/// <summary>
/// Avalonia drawing renderer.
/// </summary>
public class AvaloniaDrawingRenderer : IWavesDrawingRenderer
{
    private bool _disposed = false;

    private DrawingContext _context;

    /// <inheritdoc />
    public void Dispose()
    {
        //// if (_disposed)
        //// {
        ////     return;
        //// }
        ////
        //// _context.Dispose();
        //// _disposed = true;
    }

    /// <inheritdoc />
    public void Update(object element, IEnumerable<IWavesDrawingObject> objects, WavesColor background)
    {
        if (element is not DrawingContext context)
        {
            return;
        }

        if (_context == null)
        {
            _context = context;
        }

        _context.PlatformImpl.Clear(background.ToAvaloniaColor());

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
        var pen = new Pen()
        {
            Brush = line.Color.ToAvaloniaSolidColorBrush(),
            Thickness = line.Thickness,
            DashStyle = new DashStyle(line.DashPattern, 0),
        };

        _context.DrawLine(pen, line.Point1.ToAvaloniaPoint(), line.Point2.ToAvaloniaPoint());
    }

    /// <inheritdoc />
    public void Draw(WavesPolyline polyline)
    {
        var pen = new Pen()
        {
            Brush = polyline.Color.ToAvaloniaSolidColorBrush(),
            Thickness = polyline.Thickness,
            DashStyle = new DashStyle(polyline.DashPattern, 0),
        };

        var length = polyline.Points.Length;
        for (var i = 0; i < length - 1; i++)
        {
            _context.DrawLine(pen, polyline.Points[i].ToAvaloniaPoint(), polyline.Points[i + 1].ToAvaloniaPoint());
        }
    }

    /// <inheritdoc />
    public void Draw(WavesRectangle rectangle)
    {
        var pen = new Pen()
        {
            Brush = rectangle.Stroke.ToAvaloniaSolidColorBrush(),
            Thickness = rectangle.StrokeThickness,
        };

        var rect = new Rect(rectangle.Location.ToAvaloniaPoint(), new Size(rectangle.Width, rectangle.Height));

        _context.DrawRectangle(rectangle.Fill.ToAvaloniaSolidColorBrush(), pen, rect, rectangle.CornerRadius, rectangle.CornerRadius);
    }

    /// <inheritdoc />
    public void Draw(WavesEllipse ellipse)
    {
        var pen = new Pen
        {
            Brush = ellipse.Stroke.ToAvaloniaSolidColorBrush(),
            Thickness = ellipse.StrokeThickness,
        };

        _context.DrawEllipse(
            ellipse.Fill.ToAvaloniaSolidColorBrush(),
            pen,
            ellipse.Location.ToAvaloniaPoint(),
            ellipse.Width / 2,
            ellipse.Height / 2);
    }

    /// <inheritdoc />
    public void Draw(WavesText text)
    {
        var formattedText = new FormattedText(
            text.Value,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface(
                new FontFamily(text.Style.FontFamily),
                FontStyle.Normal,
                (FontWeight)((int)text.Style.FontWeight),
                FontStretch.Normal),
            text.Style.FontSize,
            text.Color.ToAvaloniaSolidColorBrush());

        _context.DrawText(formattedText, text.Location.ToAvaloniaPoint());
    }

    /// <inheritdoc />
    public WavesSize MeasureText(WavesText text)
    {
        var formattedText = new FormattedText(
            text.Value,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface(
                new FontFamily(text.Style.FontFamily),
                FontStyle.Normal,
                (FontWeight)((int)text.Style.FontWeight),
                FontStretch.Normal),
            text.Style.FontSize,
            text.Color.ToAvaloniaSolidColorBrush());

        return new WavesSize(formattedText.Width, formattedText.Height);
    }
}
