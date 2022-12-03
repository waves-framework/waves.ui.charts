using System;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves point series.
/// </summary>
public class WavesPointSeries : WavesSeries
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="x">X Data.</param>
    /// <param name="y">Y Data.</param>
    public WavesPointSeries(double[] x, double[] y)
    {
        if (x == null)
        {
            throw new ArgumentNullException(nameof(x), "X points data were not set.");
        }

        if (y == null)
        {
            throw new ArgumentNullException(nameof(x), "X points data were not set.");
        }

        if (x.Length != y.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        var length = x.Length;
        Points = new WavesPoint[length];
        for (var i = 0; i < length; i++)
        {
            Points[i] = new WavesPoint(x[i], y[i]);
        }
    }

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
    ///     Gets or sets point.
    /// </summary>
    public WavesPoint[] Points { get; protected set; }

    /// <summary>
    /// Updates data.
    /// </summary>
    /// <param name="x">X array.</param>
    /// <param name="y">Y array.</param>
    public void Update(double[] x, double[] y)
    {
        if (x == null)
        {
            throw new ArgumentNullException(nameof(x), "X points data were not set.");
        }

        if (y == null)
        {
            throw new ArgumentNullException(nameof(x), "X points data were not set.");
        }

        if (x.Length != y.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        var length = x.Length;
        Points = new WavesPoint[length];
        for (var i = 0; i < length; i++)
        {
            Points[i] = new WavesPoint(x[i], y[i]);
        }

        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public override void Draw(IWavesChart chart)
    {
        throw new NotImplementedException();
    }
}
