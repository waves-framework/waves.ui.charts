using System;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Candle series.
/// </summary>
public class WavesCandleSeries : WavesSeries<WavesCandle>, IWavesCandleSeries
{
    /// <inheritdoc />
    public WavesColor GrowingColor { get; set; }

    /// <inheritdoc />
    public WavesColor FallingColor { get; set; }
}
