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
public class WavesPointSeries : WavesSeries<WavesPoint>, IWavesPointSeries
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
        Data = new WavesPoint[length];
        for (var i = 0; i < length; i++)
        {
            Data[i] = new WavesPoint(x[i], y[i]);
        }
    }

    /// <inheritdoc />
    public double[] DashPattern { get; set; } = new double[] { 0, 0, 0, 0 };

    /// <inheritdoc />
    public double Thickness { get; set; } = 1.0d;

    /// <inheritdoc />
    public WavesColor Color { get; set; } = WavesColor.Red;

    /// <inheritdoc />
    public WavesPointSeriesType Type { get; set; } = WavesPointSeriesType.Line;

    /// <inheritdoc />
    public WavesDotType DotType { get; set; } = WavesDotType.None;

    /// <inheritdoc />
    public double DotSize { get; set; } = 8;

    /// <inheritdoc />
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
        Data = new WavesPoint[length];
        for (var i = 0; i < length; i++)
        {
            Data[i] = new WavesPoint(x[i], y[i]);
        }

        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public override void Draw(IWavesChart chart)
    {
        throw new NotImplementedException();
    }
}
