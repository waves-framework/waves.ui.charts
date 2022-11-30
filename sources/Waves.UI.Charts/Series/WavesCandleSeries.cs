using System;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Candle series.
/// </summary>
public class WavesCandleSeries : WavesSeries<WavesCandle>, IWavesCandleSeries
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesCandleSeries" />.
    /// </summary>
    public WavesCandleSeries()
        : base()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesCandleSeries" />.
    /// </summary>
    /// <param name="data">Data.</param>
    public WavesCandleSeries(WavesCandle[] data)
        : base(data)
    {
    }

    /// <inheritdoc />
    public WavesColor GrowingColor { get; set; }

    /// <inheritdoc />
    public WavesColor FallingColor { get; set; }
}
