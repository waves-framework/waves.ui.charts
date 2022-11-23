using System.Globalization;
using Avalonia.Media;
using Waves.UI.Avalonia.Charts.Extensions;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Avalonia.Charts.Renderer;

/// <summary>
/// Avalonia drawing renderer.
/// </summary>
public class AvaloniaDrawingRenderer : IWavesDrawingRenderer
{
    private DrawingContext _context;

    /// <inheritdoc />
    public void Dispose()
    {
        _context.Dispose();
    }

    /// <inheritdoc />
    public void Update(object element, IEnumerable<IWavesDrawingObject> objects)
    {
        if (element is not DrawingContext context)
        {
            return;
        }

        if (_context == null)
        {
            _context = context;
        }

        _context.PlatformImpl.Clear(Color.FromArgb(0, 0, 0, 0));

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
            Brush = line.Fill.ToAvaloniaSolidColorBrush(),
            Thickness = line.StrokeThickness,
            DashStyle = new DashStyle(line.DashPattern, 0),
        };

        _context.DrawLine(pen, line.Point1.ToAvaloniaPoint(), line.Point2.ToAvaloniaPoint());
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
            text.Fill.ToAvaloniaSolidColorBrush());

        _context.DrawText(formattedText, text.Location.ToAvaloniaPoint());
    }

    /// <inheritdoc />
    public WavesSize MeasureText(string text, IWavesTextStyle style)
    {
        var formattedText = new FormattedText(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface(
                new FontFamily(style.FontFamily),
                FontStyle.Normal,
                (FontWeight)((int)style.FontWeight),
                FontStretch.Normal),
            style.FontSize,
            text.Fill.ToAvaloniaSolidColorBrush());
    }
}
