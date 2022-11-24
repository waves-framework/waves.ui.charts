using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Enums;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Point series.
/// </summary>
public interface IWavesPointSeries : IWavesSeries
{
    /// <summary>
    ///     Gets or sets series type.
    /// </summary>
    public WavesPointSeriesType Type { get; set; }

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public WavesPoint[] Points { get; set; }
}
