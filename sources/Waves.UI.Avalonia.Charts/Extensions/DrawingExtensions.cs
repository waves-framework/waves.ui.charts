using System.Drawing;
using Avalonia.Media;
using Waves.UI.Charts.Drawing.Primitives;
using Color = Avalonia.Media.Color;

namespace Waves.UI.Avalonia.Charts.Extensions;

/// <summary>
/// Drawing extensions.
/// </summary>
public static class DrawingExtensions
{
    /// <summary>
    /// Converts <see cref="WavesColor"/> to <see cref="SolidColorBrush"/>.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>Return <see cref="SolidColorBrush"/>.</returns>
    public static SolidColorBrush ToAvaloniaSolidColorBrush(this WavesColor color)
    {
        return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
    }

    /// <summary>
    /// Converts <see cref="WavesColor"/> to <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>Return <see cref="Color"/>.</returns>
    public static Color ToAvaloniaColor(this WavesColor color)
    {
        return new Color(color.A, color.R, color.G, color.B);
    }

    /// <summary>
    /// Converts <see cref="Color"/> to <see cref="WavesColor"/>.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>Return <see cref="WavesColor"/>.</returns>
    public static WavesColor ToWavesColor(this Color color)
    {
        return new WavesColor(color.A, color.R, color.G, color.B);
    }

    /// <summary>
    /// Converts <see cref="global::Avalonia.Point"/> to <see cref="Point"/>.
    /// </summary>
    /// <param name="point">Point.</param>
    /// <returns>Returns Avalonia point.</returns>
    public static global::Avalonia.Point ToAvaloniaPoint(this WavesPoint point)
    {
        return new global::Avalonia.Point(point.X, point.Y);
    }
}
