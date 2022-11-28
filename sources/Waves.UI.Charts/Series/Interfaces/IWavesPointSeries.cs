using System;
using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Enums;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Point series.
/// </summary>
public interface IWavesPointSeries : IWavesSeries<WavesPoint>
{
    /// <summary>
    ///     Gets or sets series type.
    /// </summary>
    public WavesPointSeriesType Type { get; set; }

    /// <summary>
    /// Gets or sets color.
    /// </summary>
    public WavesColor Color { get; set; }

    /// <summary>
    ///     Gets or sets dash pattern.
    /// </summary>
    public double[] DashPattern { get; set; }

    /// <summary>
    /// Gets or sets stroke thickness.
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    /// Gets or sets dot type.
    /// </summary>
    public WavesDotType DotType { get; set; }

    /// <summary>
    /// Gets or sets dot type.
    /// </summary>
    public double DotSize { get; set; }

    /// <summary>
    /// Updates point series.
    /// </summary>
    /// <param name="x">X.</param>
    /// <param name="y">Y.</param>
    void Update(double[] x, double[] y);
}
