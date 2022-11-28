using System;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves candle data.
/// </summary>
public struct WavesCandle
{
    /// <summary>
    /// Gets or sets open price.
    /// </summary>
    public decimal Open { get; set; }

    /// <summary>
    /// Gets or sets close price.
    /// </summary>
    public decimal Close { get; set; }

    /// <summary>
    /// Gets or sets highest price.
    /// </summary>
    public decimal High { get; set; }

    /// <summary>
    /// Gets or sets lowest price.
    /// </summary>
    public decimal Low { get; set; }

    /// <summary>
    /// Gets or sets volume.
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets date / time.
    /// </summary>
    public DateTime DateTime { get; set; }
}
