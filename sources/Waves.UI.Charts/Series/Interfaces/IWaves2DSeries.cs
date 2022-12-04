using System;
using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Interface of 2D series.
/// </summary>
public interface IWaves2DSeries : IWavesSeries
{
    /// <summary>
    /// Gets X min.
    /// </summary>
    public object XMin { get; }

    /// <summary>
    /// Gets X max.
    /// </summary>
    public object XMax { get; }

    /// <summary>
    /// Gets Y min.
    /// </summary>
    public double YMin { get; }

    /// <summary>
    /// Gets Y max.
    /// </summary>
    public double YMax { get; }
}
