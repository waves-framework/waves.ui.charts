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
    /// Gets or sets dot type.
    /// </summary>
    public WavesDotType DotType { get; set; }

    /// <summary>
    /// Gets or sets dot type.
    /// </summary>
    public double DotSize { get; set; }

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public WavesPoint[] Points { get; }

    /// <summary>
    /// Updates point series.
    /// </summary>
    void Update();

    /// <summary>
    /// Updates point series.
    /// </summary>
    /// <param name="points">Points.</param>
    void Update(WavesPoint[] points);

    /// <summary>
    /// Updates point series.
    /// </summary>
    /// <param name="x">X.</param>
    /// <param name="y">Y.</param>
    void Update(double[] x, double[] y);
}
