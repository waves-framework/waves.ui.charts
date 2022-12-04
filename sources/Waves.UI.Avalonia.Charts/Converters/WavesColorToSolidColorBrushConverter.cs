using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Waves.UI.Avalonia.Charts.Extensions;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Avalonia.Charts.Converters;

/// <summary>
/// Waves color to Solid color brush converter.
/// </summary>
public class WavesColorToSolidColorBrushConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var color = (WavesColor)(value ?? throw new ArgumentNullException(nameof(value)));
        return new SolidColorBrush(color.ToUint());
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var brush = (SolidColorBrush)(value ?? throw new ArgumentNullException(nameof(value)));
        return brush.Color.ToWavesColor();
    }
}
