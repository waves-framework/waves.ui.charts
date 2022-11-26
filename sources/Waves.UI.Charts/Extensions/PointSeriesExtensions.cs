using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Charts.Extensions;

/// <summary>
/// Point series utils.
/// </summary>
public static class PointSeriesExtensions
{
    /// <summary>
    ///     Generates line series.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="series">Series.</param>
    /// <param name="cache">Cache.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="background">Background.</param>
    /// <param name="foreground">Foreground.</param>
    public static void GenerateLineSeries(
        this IWavesChart chart,
        IWavesPointSeries series,
        ICollection<IWavesDrawingObject> cache,
        double width,
        double height,
        WavesColor background,
        WavesColor foreground)
    {
        if (series.Points == null)
        {
            return;
        }

        if (series.Points.Length == 0)
        {
            return;
        }

        var visiblePoints = new List<WavesPoint> { new () };
        foreach (var point in series.Points)
        {
            if (point.X < chart.CurrentXMin)
            {
                visiblePoints[0] = point;
                continue;
            }

            if (point.X >= chart.CurrentXMin && point.X <= chart.CurrentXMax)
            {
                visiblePoints.Add(point);
            }
            else if (point.X > chart.CurrentXMax)
            {
                visiblePoints.Add(point);
                break;
            }
        }

        var length = (int)width;
        var points = visiblePoints.Count > length
            ? Resampling.LargestTriangleThreeBucketsDecimation(visiblePoints.ToArray(), length)
            : Resampling.SplineInterpolation(visiblePoints.ToArray(), length);

        for (var i = 0; i < points.Length; i++)
        {
            points[i] = Valuation.NormalizePoint(
                points[i],
                width,
                height,
                chart.CurrentXMin,
                chart.CurrentYMin,
                chart.CurrentXMax,
                chart.CurrentYMax);
        }

        for (var i = 1; i < points.Length; i++)
        {
            var line = new WavesLine
            {
                Color = series.Color,
                IsAntialiased = true,
                IsVisible = true,
                Thickness = 2,
                Point1 = points[i - 1],
                Point2 = points[i],
                Opacity = series.Opacity,
            };

            if (visiblePoints.Count > length)
            {
                line.IsAntialiased = false;
            }

            chart.DrawingObjects?.Add(line);
            cache.Add(line);
        }

        if (series.DotType != WavesDotType.None)
        {
            List<IWavesDrawingObject> dotsObjects = null;

            switch (series.DotType)
            {
                case WavesDotType.Circle:
                    dotsObjects = GenerateCircleDots(points, background, series.Color, series.DotSize);
                    break;
                case WavesDotType.FilledCircle:
                    dotsObjects = GenerateCircleDots(points, series.Color, background, series.DotSize);
                    break;
            }

            if (dotsObjects != null)
            {
                foreach (var obj in dotsObjects)
                {
                    chart.DrawingObjects?.Add(obj);
                    cache.Add(obj);
                }
            }
        }
    }

    /// <summary>
    /// Generates bar series.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="series">Series.</param>
    /// <param name="cache">Cache.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="background">Background.</param>
    /// <param name="foreground">Foreground.</param>
    public static void GenerateBarSeries(
        this IWavesChart chart,
        IWavesPointSeries series,
        ICollection<IWavesDrawingObject> cache,
        double width,
        double height,
        WavesColor background,
        WavesColor foreground)
    {
        if (series.Points == null)
        {
            return;
        }

        if (series.Points.Length == 0)
        {
            return;
        }

        var visiblePoints = new List<WavesPoint>();
        {
            foreach (var point in series.Points)
            {
                if (point.X < chart.CurrentXMin)
                {
                    if (visiblePoints.Count == 0)
                    {
                        visiblePoints.Add(new WavesPoint());
                    }

                    visiblePoints[0] = point;
                    continue;
                }

                if (point.X >= chart.CurrentXMin && point.X <= chart.CurrentXMax)
                {
                    visiblePoints.Add(point);
                }
                else if (point.X > chart.CurrentXMax)
                {
                    visiblePoints.Add(point);
                    break;
                }
            }
        }

        var length = (int)width;
        var points = visiblePoints.Count > length
                ? Resampling.LargestTriangleThreeBucketsDecimation(visiblePoints.ToArray(), length)
                : Resampling.SplineInterpolation(visiblePoints.ToArray(), length);

        for (var i = 0; i < points.Length; i++)
        {
            points[i] = Valuation.NormalizePoint(
                points[i],
                width,
                height,
                chart.CurrentXMin,
                chart.CurrentYMin,
                chart.CurrentXMax,
                chart.CurrentYMax);
        }

        for (var i = 0; i < points.Length - 1; i++)
        {
            var rectWidth = points[i + 1].X - points[i].X;
            var rectHeight = height - points[i].Y;

            var rectangle = new WavesRectangle
            {
                Fill = series.Color,
                IsAntialiased = false,
                IsVisible = true,
                StrokeThickness = 2,
                Stroke = background,
                Location = points[i],
                Width = rectWidth,
                Height = rectHeight,
                Opacity = 0.8f,
            };

            if (visiblePoints.Count > length / 4)
            {
                rectangle.StrokeThickness = 0;
                rectangle.IsAntialiased = false;
            }

            if (visiblePoints.Count <= length / 32)
            {
                // add signatures
                var ep = new WavesPoint(points[i].X + (points[i + 1].X - points[i].X) / 2, points[i].Y);
                var value = Valuation.DenormalizePointY2D(points[i].Y, height, chart.CurrentYMin, chart.CurrentYMax);

                var v = Math.Round(value, 2).ToString(CultureInfo.CurrentCulture);

                var text = new WavesText()
                {
                    Color = foreground,
                    Location = new WavesPoint(ep.X, ep.Y),
                    Style = new WavesTextStyle(),
                    Value = v,
                    IsVisible = true,
                    IsAntialiased = true,
                };

                var size = chart.Renderer.MeasureText(text);

                text.Location = new WavesPoint(text.Location.X - size.Width / 2, text.Location.Y - size.Height - 6);

                chart.DrawingObjects?.Add(text);
                cache.Add(text);
            }

            chart.DrawingObjects?.Add(rectangle);
            cache.Add(rectangle);
        }

        if (points.Length == 0)
        {
            return;
        }

        var lastIndex = points.Length - 1;
        var lastWidth = width - points[lastIndex].X;
        var lastHeight = height - points[lastIndex].Y;

        var lastRectangle = new WavesRectangle
        {
            Fill = series.Color,
            IsAntialiased = false,
            IsVisible = true,
            StrokeThickness = 2,
            Stroke = background,
            Location = points[lastIndex],
            Width = lastWidth,
            Height = lastHeight,
            Opacity = 0.8f,
        };

        if (visiblePoints.Count > length / 4)
        {
            lastRectangle.StrokeThickness = 0;
            lastRectangle.IsAntialiased = false;
        }

        if (visiblePoints.Count <= length / 32)
        {
            var ep = new WavesPoint(
                points[lastIndex].X + (points[lastIndex].X - points[lastIndex].X) / 2,
                points[lastIndex].Y);
            var value = Valuation.DenormalizePointY2D(
                points[lastIndex].Y,
                height,
                chart.CurrentYMin,
                chart.CurrentYMax);

            var v = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);

            var text = new WavesText
            {
                Color = foreground,
                Location = new WavesPoint(ep.X, ep.Y),
                Style = new WavesTextStyle(),
                Value = v,
                IsVisible = true,
                IsAntialiased = true,
            };

            var size = chart.Renderer.MeasureText(text);

            text.Location = new WavesPoint(text.Location.X - size.Width / 2 + lastWidth / 2, text.Location.Y - size.Height - 6);

            chart.DrawingObjects?.Add(text);
            cache.Add(text);
        }

        chart.DrawingObjects?.Add(lastRectangle);
        cache.Add(lastRectangle);
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
}
