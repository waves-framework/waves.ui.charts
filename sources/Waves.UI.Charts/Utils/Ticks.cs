using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;

namespace Waves.UI.Charts.Utils;

/// <summary>
///     Chart ticks generation.
/// </summary>
public static class Ticks
{
    /// <summary>
    /// Generates axis ticks.
    /// </summary>
    /// <param name="ticks">Input ticks.</param>
    /// <param name="min">Min.</param>
    /// <param name="max">Max.</param>
    /// <param name="primaryTicksCount">Primary ticks count.</param>
    /// <param name="additionalTicksCount">Additional ticks count.</param>
    /// <param name="orientation">Orientation.</param>
    public static void GenerateAxisTicks(
        this List<WavesAxisTick> ticks,
        double min,
        double max,
        int primaryTicksCount,
        int additionalTicksCount,
        WavesAxisTickOrientation orientation)
    {
        var rank = (int)Math.Round(Math.Log10(max - min));

        if (rank == int.MinValue)
        {
            return;
        }

        var highestRange = (double)Math.Pow(10, rank);
        var roundFactor = Math.Abs(rank) + 1;
        var tickStep = highestRange / primaryTicksCount;
        var additionalTickStep = tickStep / additionalTicksCount;

        if (!(Math.Abs(tickStep) > 0))
        {
            return;
        }

        var start = Math.Round(min / tickStep) * tickStep;

        for (var i = start; i <= max; i += tickStep)
        {
            for (var j = i + additionalTickStep; j <= i + tickStep - additionalTickStep; j += additionalTickStep)
            {
                if (j > max)
                {
                    continue;
                }

                if (Math.Abs(j - min) < double.Epsilon || Math.Abs(j - max) < double.Epsilon)
                {
                    continue;
                }

                ticks.Add(new WavesAxisTick
                {
                    Description = j.ToString(CultureInfo.InvariantCulture),
                    Value = Convert.ToSingle(j),
                    IsVisible = true,
                    Orientation = orientation,
                    Type = WavesAxisTickType.Additional
                });
            }

            if (i > max)
            {
                continue;
            }

            if (Math.Abs(i) > 0)
            {
                if (Math.Abs(i - min) < double.Epsilon || Math.Abs(i - max) < double.Epsilon)
                {
                    continue;
                }

                var description = i.ToString(CultureInfo.InvariantCulture);
                if (roundFactor < 15)
                {
                    description = Math.Round(i, roundFactor).ToString(CultureInfo.InvariantCulture);
                }

                ticks.Add(new WavesAxisTick
                {
                    Description = description,
                    Value = Convert.ToSingle(i),
                    IsVisible = true,
                    Orientation = orientation,
                    Type = WavesAxisTickType.Primary
                });
            }
        }

        ticks.Reverse();

        if (min < 0 && max > 0)
        {
            ticks.Add(new WavesAxisTick
            {
                Description = 0.ToString(CultureInfo.InvariantCulture),
                Value = 0,
                IsVisible = true,
                Orientation = orientation,
                Type = WavesAxisTickType.Zero,
            });
        }
    }

    /// <summary>
    /// Gets X axis tick line.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="strokeThickness">Thickness.</param>
    /// <param name="stroke">Stroke.</param>
    /// <param name="dashArray">Dash array.</param>
    /// <param name="opacity">Opacity.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <returns>Returns tick line.</returns>
    public static WavesLine GetXAxisTickLine(
        double value,
        double strokeThickness,
        Color stroke,
        double[] dashArray,
        double opacity,
        double xMin,
        double xMax,
        double width,
        double height)
    {
        return new WavesLine
        {
            Stroke = stroke,
            Fill = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            StrokeThickness = strokeThickness,
            Point1 = new WavesPoint(Valuation.NormalizePointX2D(value, width, xMin, xMax), 0),
            Point2 = new WavesPoint(Valuation.NormalizePointX2D(value, width, xMin, xMax), height),
        };
    }

    /// <summary>
    /// Gets Y axis tick line.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="strokeThickness">Thickness.</param>
    /// <param name="stroke">Stroke.</param>
    /// <param name="dashArray">Dash array.</param>
    /// <param name="opacity">Opacity.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="yMax">Y max.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <returns>Returns tick line.</returns>
    public static WavesLine GetYAxisTickLine(
        double value,
        double strokeThickness,
        Color stroke,
        double[] dashArray,
        double opacity,
        double yMin,
        double yMax,
        double width,
        double height)
    {
        return new WavesLine
        {
            Stroke = stroke,
            Fill = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            StrokeThickness = strokeThickness,
            Point1 = new WavesPoint(0, Valuation.NormalizePointY2D(value, height, yMin, yMax)),
            Point2 = new WavesPoint(width, Valuation.NormalizePointY2D(value, height, yMin, yMax)),
        };
    }
}
