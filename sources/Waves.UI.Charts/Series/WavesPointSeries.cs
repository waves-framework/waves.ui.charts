using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves point series.
/// </summary>
public class WavesPointSeries : IWavesPointSeries
{
    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0d;

    /// <inheritdoc />
    public double[] DashPattern { get; set; } = new double[4] { 0, 0, 0, 0 };

    /// <inheritdoc />
    public double Thickness { get; set; } = 1.0d;

    /// <inheritdoc />
    public WavesColor Color { get; set; } = WavesColor.Red;

    /// <inheritdoc />
    public WavesPointSeriesType Type { get; set; } = WavesPointSeriesType.Line;

    /// <inheritdoc />
    public WavesPoint[] Points { get; set; }
}
