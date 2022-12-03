using System;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives.Data;

/// <summary>
/// Waves candle data.
/// </summary>
public struct WavesCandle : IWavesSeriesData
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
    /// Gets or sets open date / time.
    /// </summary>
    public DateTime OpenDateTime { get; set; }

    /// <summary>
    /// Gets or sets close date / time.
    /// </summary>
    public DateTime CloseDateTime { get; set; }

    /// <summary>
    ///     ToString method overriden for easy printing/debugging.
    /// </summary>
    /// <returns>The string representation of the vector.</returns>
    public override string ToString()
    {
        return $"(O: {Open} ({OpenDateTime}); C: {Close} ({CloseDateTime}); L: {Low}; H: {High}; V: {Volume})";
    }
}
