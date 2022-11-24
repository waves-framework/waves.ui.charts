using System;
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
    public WavesPoint[] Points { get; }

    /// <summary>
    ///     Gets or sets data set descriptions.
    /// </summary>
    string[] Description { get; }

    /// <summary>
    /// Updates point series.
    /// </summary>
    /// <param name="points">Points.</param>
    /// <param name="description">Description.</param>
    void Update(WavesPoint[] points, string[] description = null);

    /// <summary>
    /// Updates point series.
    /// </summary>
    /// <param name="x">X.</param>
    /// <param name="y">Y.</param>
    /// <param name="description">Description.</param>
    void Update(double[] x, double[] y, string[] description = null);
}
