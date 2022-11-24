using System;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves point series.
/// </summary>
public class WavesPointSeries : IWavesPointSeries
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    public WavesPointSeries()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="points">Data.</param>
    /// <param name="description">Descriptions.</param>
    public WavesPointSeries(WavesPoint[] points, string[] description = null)
    {
        if (points == null)
        {
            throw new ArgumentNullException(nameof(points), "Points were not set.");
        }

        if (description != null && points.Length != description.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        Points = points;
        Description = description;
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="x">X Data.</param>
    /// <param name="y">Y Data.</param>
    /// <param name="description">Descriptions.</param>
    public WavesPointSeries(double[] x, double[] y, string[] description = null)
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

        if (description != null && x.Length != description.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        var length = x.Length;
        Points = new WavesPoint[length];
        for (var i = 0; i < length; i++)
        {
            Points[i] = new WavesPoint(x[i], y[i]);
        }

        Description = description;
    }

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0d;

    /// <inheritdoc />
    public double[] DashPattern { get; set; } = new double[] { 0, 0, 0, 0 };

    /// <inheritdoc />
    public double Thickness { get; set; } = 1.0d;

    /// <inheritdoc />
    public WavesColor Color { get; set; } = WavesColor.Red;

    /// <inheritdoc />
    public WavesPointSeriesType Type { get; set; } = WavesPointSeriesType.Line;

    /// <inheritdoc />
    public WavesPoint[] Points { get; private set; }

    /// <inheritdoc />
    public string[] Description { get; private set; }

    /// <inheritdoc />
    public void Update(WavesPoint[] points, string[] description = null)
    {
        if (points == null)
        {
            throw new ArgumentNullException(nameof(points), "Points were not set.");
        }

        if (points.Length != Points.Length)
        {
            Points = new WavesPoint[points.Length];
        }

        for (var i = 0; i < Points.Length; i++)
        {
            Points[i] = points[i];
        }

        if (description != null && points.Length != description.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        if (description != null)
        {
            if (description.Length != Description.Length)
            {
                Description = new string[description.Length];
            }

            for (var i = 0; i < Description.Length; i++)
            {
                Description[i] = description[i];
            }
        }
    }

    /// <inheritdoc />
    public void Update(double[] x, double[] y, string[] description = null)
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

        if (description != null && x.Length != description.Length)
        {
            throw new Exception("Array lengths do not match.");
        }

        if (description != null)
        {
            if (description.Length != Description.Length)
            {
                Description = new string[description.Length];
            }

            for (var i = 0; i < Description.Length; i++)
            {
                Description[i] = description[i];
            }
        }
    }
}
