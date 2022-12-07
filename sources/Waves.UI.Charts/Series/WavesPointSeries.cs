using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Charts.Series;

/// <summary>
///     Waves point series.
/// </summary>
public class WavesPointSeries : Waves2DSeries
{
    private readonly ConcurrentBag<IWavesDrawingObject> _cache = new ();

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="points">Data.</param>
    public WavesPointSeries(WavesPoint[] points)
    {
        CheckAndFillArray(points);
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="x">X Data.</param>
    /// <param name="y">Y Data.</param>
    public WavesPointSeries(double[] x, double[] y)
    {
        CheckAndFillArray(x, y);
    }

    /// <summary>
    ///     Gets or sets color.
    /// </summary>
    public WavesColor Color { get; set; } = WavesColor.Red;

    /// <summary>
    ///     Gets or sets dash pattern.
    /// </summary>
    public double[] DashPattern { get; set; }

    /// <summary>
    ///     Gets or sets stroke thickness.
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    ///     Gets or sets dot type.
    /// </summary>
    public WavesDotType DotType { get; set; } = WavesDotType.None;

    /// <summary>
    ///     Gets or sets dot type.
    /// </summary>
    public double DotSize { get; set; } = 5;

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public WavesPoint[] Points { get; protected set; }

    /// <inheritdoc />
    public override object XMin => Points.Min(x => x.X);

    /// <inheritdoc />
    public override object XMax => Points.Max(x => x.X);

    /// <inheritdoc />
    public override double YMin => Points.Min(x => x.Y);

    /// <inheritdoc />
    public override double YMax => Points.Max(x => x.Y);

    /// <summary>
    ///     Updates data.
    /// </summary>
    /// <param name="points">Data.</param>
    public void Update(WavesPoint[] points)
    {
        CheckAndFillArray(points);
        OnSeriesUpdated();
    }

    /// <summary>
    ///     Updates data.
    /// </summary>
    /// <param name="x">X array.</param>
    /// <param name="y">Y array.</param>
    public void Update(double[] x, double[] y)
    {
        CheckAndFillArray(x, y);
        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public override void Draw(IWavesChart chart)
    {
        if (Points == null)
        {
            return;
        }

        if (Points.Length == 0)
        {
            return;
        }

        while (_cache.TryTake(out var obj))
        {
            chart.DrawingObjects?.Remove(obj);
        }

        var currentXMin = ValuesUtils.GetValue(chart.CurrentXMin);
        var currentXMax = ValuesUtils.GetValue(chart.CurrentXMax);
        var currentYMin = ValuesUtils.GetValue(chart.CurrentYMin);
        var currentYMax = ValuesUtils.GetValue(chart.CurrentYMax);
        var visiblePoints = new List<WavesPoint> { new () };
        foreach (var point in Points)
        {
            if (point.X <= currentXMin)
            {
                visiblePoints[0] = point;
                continue;
            }

            if (point.X > currentXMin && point.X <= currentXMax)
            {
                visiblePoints.Add(point);
            }
            else if (point.X > currentXMax)
            {
                visiblePoints.Add(point);
                break;
            }
        }

        var length = (int)chart.SurfaceWidth;
        var points = visiblePoints.Count > length
            ? ResamplingUtils.LargestTriangleThreeBucketsDecimation(visiblePoints.ToArray(), length)
            : ResamplingUtils.SplineInterpolation(visiblePoints.ToArray(), length);
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = ValuationUtils.NormalizePoint(
                points[i],
                chart.SurfaceWidth,
                chart.SurfaceHeight,
                currentXMin,
                currentYMin,
                currentXMax,
                currentYMax);
        }

        for (var i = 1; i < points.Length; i++)
        {
            var line = new WavesLine
            {
                Color = Color,
                IsAntialiased = true,
                IsVisible = true,
                Thickness = 2,
                Point1 = points[i - 1],
                Point2 = points[i],
                Opacity = Opacity,
            };
            if (visiblePoints.Count > length)
            {
                line.IsAntialiased = false;
            }

            chart.DrawingObjects?.Add(line);
            _cache.Add(line);
        }

        if (DotType != WavesDotType.None)
        {
            List<IWavesDrawingObject> dotsObjects = null;
            var dotPoints = new WavesPoint[visiblePoints.Count];
            if (visiblePoints.Count <= length)
            {
                for (var i = 0; i < visiblePoints.Count; i++)
                {
                    dotPoints[i] = ValuationUtils.NormalizePoint(
                        visiblePoints[i],
                        chart.SurfaceWidth,
                        chart.SurfaceHeight,
                        currentXMin,
                        currentYMin,
                        currentXMax,
                        currentYMax);
                }
            }
            else
            {
                return;
            }

            switch (DotType)
            {
                case WavesDotType.Circle:
                    dotsObjects = GenerateCircleDots(dotPoints, chart.BackgroundColor, Color, DotSize);
                    break;
                case WavesDotType.FilledCircle:
                    dotsObjects = GenerateCircleDots(dotPoints, Color, chart.BackgroundColor, DotSize);
                    break;
            }

            if (dotsObjects != null)
            {
                foreach (var obj in dotsObjects)
                {
                    chart.DrawingObjects?.Add(obj);
                    _cache.Add(obj);
                }
            }
        }
    }

    /// <summary>
    /// Generates circle dots.
    /// </summary>
    /// <param name="points">Points.</param>
    /// <param name="background">Background.</param>
    /// <param name="foreground">Foreground.</param>
    /// <param name="dotSize">DotSize.</param>
    private static List<IWavesDrawingObject> GenerateCircleDots(WavesPoint[] points, WavesColor background, WavesColor foreground, double dotSize)
    {
        return points.Select(point => new WavesEllipse()
            {
                Location = point,
                Fill = background,
                Stroke = foreground,
                StrokeThickness = 1.5,
                Width = dotSize,
                Height = dotSize,
            })
            .Cast<IWavesDrawingObject>()
            .ToList();
    }

    /// <summary>
    /// Checks input array and fills inner.
    /// </summary>
    /// <param name="points">Points.</param>
    private void CheckAndFillArray(WavesPoint[] points)
    {
        if (points == null)
        {
            throw new ArgumentNullException(nameof(points), "Points data were not set.");
        }

        var length = points.Length;
        if (Points == null || Points.Length != points.Length)
        {
            Points = new WavesPoint[length];
        }

        for (var i = 0; i < length; i++)
        {
            Points[i] = points[i];
        }
    }

    /// <summary>
    /// Checks input array and fills inner.
    /// </summary>
    /// <param name="x">X.</param>
    /// <param name="y">Y.</param>
    private void CheckAndFillArray(double[] x, double[] y)
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
        if (Points == null || Points.Length != x.Length)
        {
            Points = new WavesPoint[length];
        }

        for (var i = 0; i < length; i++)
        {
            Points[i] = new WavesPoint(x[i], y[i]);
        }
    }
}
