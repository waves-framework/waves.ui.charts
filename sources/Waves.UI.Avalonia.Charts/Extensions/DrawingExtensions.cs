using System.Drawing;
using Avalonia.Media;
using Color = System.Drawing.Color;

namespace Waves.UI.Avalonia.Charts.Extensions;

/// <summary>
/// Drawing extensions.
/// </summary>
public static class DrawingExtensions
{
    /// <summary>
    /// Converts <see cref="Color"/> to <see cref="SolidColorBrush"/>.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>Return <see cref="SolidColorBrush"/>.</returns>
    public static SolidColorBrush ToSolidColorBrush(this Color color)
    {
        return new SolidColorBrush((uint)color.ToArgb());
    }

    /// <summary>
    /// Converts <see cref="global::Avalonia.Point"/> to <see cref="Point"/>.
    /// </summary>
    /// <param name="point">Point.</param>
    /// <returns>Returns Avalonia point.</returns>
    public static global::Avalonia.Point ToAvaloniaPoint(this Point point)
    {
        return new global::Avalonia.Point(point.X, point.Y);
    }

    /// <summary>
    /// Converts float array to double.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <returns>Double.</returns>
    public static IEnumerable<double> ToDouble(this float[] array)
    {
        return Array.ConvertAll(array, x => (double)x);
    }
}
