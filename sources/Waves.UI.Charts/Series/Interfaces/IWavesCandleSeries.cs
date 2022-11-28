using System;
using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Enums;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Candle series.
/// </summary>
public interface IWavesCandleSeries : IWavesSeries<WavesCandle>
{
    /// <summary>
    /// Gets or sets growing color.
    /// </summary>
    public WavesColor GrowingColor { get; set; }

    /// <summary>
    /// Gets or sets falling color.
    /// </summary>
    public WavesColor FallingColor { get; set; }

    /// <summary>
    /// Updates candle series.
    /// </summary>
    /// <param name="candles">Candles.</param>
    void Update(WavesCandle[] candles);
}
