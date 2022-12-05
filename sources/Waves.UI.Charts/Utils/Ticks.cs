using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Utils;

/// <summary>
///     Chart ticks generation.
/// </summary>
public static class Ticks
{
    /// <summary>
    /// Generates ticks drawing objects.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="ticks">Ticks.</param>
    /// <param name="cache">Axis ticks drawing objects cache.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <returns>Returns cache.</returns>
    public static ICollection<IWavesDrawingObject> GenerateAxisTicksDrawingObjects(
        this IWavesChart chart,
        List<WavesAxisTick> ticks,
        List<IWavesDrawingObject> cache,
        double width,
        double height)
    {
        if (chart.DrawingObjects is null)
        {
            throw new Exception("Collection of drawing object has not been initialized.");
        }

        var currentXMin = Values.GetValue(chart.CurrentXMin);
        var currentXMax = Values.GetValue(chart.CurrentXMax);
        var currentYMin = Values.GetValue(chart.CurrentYMin);
        var currentYMax = Values.GetValue(chart.CurrentYMax);

        foreach (var obj in cache)
        {
            chart.DrawingObjects.Remove(obj);
        }

        cache.Clear();

        foreach (var tick in ticks)
        {
            if (tick.Orientation == WavesAxisTickOrientation.Horizontal)
            {
                if (tick.Type == WavesAxisTickType.Primary)
                {
                    if (!chart.IsXAxisPrimaryTicksVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetXAxisTickLine(
                        tick.Value,
                        chart.XAxisPrimaryTickThickness,
                        chart.XAxisPrimaryTicksColor,
                        chart.XAxisPrimaryTicksDashArray,
                        0.5f,
                        currentXMin,
                        currentXMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
                else if (tick.Type == WavesAxisTickType.Additional)
                {
                    if (!chart.IsXAxisAdditionalTicksVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetXAxisTickLine(
                        tick.Value,
                        chart.XAxisAdditionalTickThickness,
                        chart.XAxisAdditionalTicksColor,
                        chart.XAxisAdditionalTicksDashArray,
                        0.25f,
                        currentXMin,
                        currentXMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
                else if (tick.Type == WavesAxisTickType.Zero)
                {
                    if (!chart.IsXAxisZeroLineVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetXAxisTickLine(
                        tick.Value,
                        chart.XAxisZeroLineThickness,
                        chart.XAxisZeroLineColor,
                        chart.XAxisZeroLineDashArray,
                        1f,
                        currentXMin,
                        currentXMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
            }
            else if (tick.Orientation == WavesAxisTickOrientation.Vertical)
            {
                if (tick.Type == WavesAxisTickType.Primary)
                {
                    if (!chart.IsYAxisPrimaryTicksVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetYAxisTickLine(
                        tick.Value,
                        chart.YAxisPrimaryTickThickness,
                        chart.YAxisPrimaryTicksColor,
                        chart.YAxisPrimaryTicksDashArray,
                        0.5f,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
                else if (tick.Type == WavesAxisTickType.Additional)
                {
                    if (!chart.IsYAxisAdditionalTicksVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetYAxisTickLine(
                        tick.Value,
                        chart.YAxisAdditionalTickThickness,
                        chart.YAxisAdditionalTicksColor,
                        chart.YAxisAdditionalTicksDashArray,
                        0.25f,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
                else if (tick.Type == WavesAxisTickType.Zero)
                {
                    if (!chart.IsYAxisZeroLineVisible)
                    {
                        continue;
                    }

                    var obj = Ticks.GetYAxisTickLine(
                        tick.Value,
                        chart.YAxisZeroLineThickness,
                        chart.YAxisZeroLineColor,
                        chart.YAxisZeroLineDashArray,
                        1f,
                        chart.CurrentYMin,
                        chart.CurrentYMax,
                        width,
                        height);

                    chart.DrawingObjects.Add(obj);
                    cache.Add(obj);
                }
            }
        }

        return cache;
    }

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

        var highestRange = Math.Pow(10, rank);
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
                    Type = WavesAxisTickType.Additional,
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
                    Type = WavesAxisTickType.Primary,
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
    /// Generates axis ticks.
    /// </summary>
    /// <param name="ticks">Input ticks.</param>
    /// <param name="dateMin">Date time min.</param>
    /// <param name="dateMax">Date time max.</param>
    /// <param name="format">Format.</param>
    /// <param name="primaryTicksCount">Primary ticks count.</param>
    /// <param name="additionalTicksCount">Additional ticks count.</param>
    /// <param name="orientation">Orientation.</param>
    public static void GenerateAxisTicks(
        this List<WavesAxisTick> ticks,
        DateTime dateMin,
        DateTime dateMax,
        string format,
        int primaryTicksCount,
        int additionalTicksCount,
        WavesAxisTickOrientation orientation)
    {
        var min = dateMin.ToOADate();
        var max = dateMax.ToOADate();

        var rank = (int)Math.Round(Math.Log10(max - min));

        if (rank == int.MinValue)
        {
            return;
        }

        var highestRange = Math.Pow(10, rank);
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
                    Description = DateTime.FromOADate(j).ToString(format),
                    Value = Convert.ToSingle(j),
                    IsVisible = true,
                    Orientation = orientation,
                    Type = WavesAxisTickType.Additional,
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

                var description = DateTime.FromOADate(i).ToString(format);
                if (roundFactor < 15)
                {
                    description = DateTime.FromOADate(i).ToString(format);
                }

                ticks.Add(new WavesAxisTick
                {
                    Description = description,
                    Value = Convert.ToSingle(i),
                    IsVisible = true,
                    Orientation = orientation,
                    Type = WavesAxisTickType.Primary,
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
        WavesColor stroke,
        double[] dashArray,
        double opacity,
        double xMin,
        double xMax,
        double width,
        double height)
    {
        return new WavesLine
        {
            Color = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            Thickness = strokeThickness,
            Point1 = new WavesPoint(Valuation.NormalizeValueX(value, width, xMin, xMax), 0),
            Point2 = new WavesPoint(Valuation.NormalizeValueX(value, width, xMin, xMax), height),
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
        WavesColor stroke,
        double[] dashArray,
        double opacity,
        double yMin,
        double yMax,
        double width,
        double height)
    {
        return new WavesLine
        {
            Color = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            Thickness = strokeThickness,
            Point1 = new WavesPoint(0, Valuation.NormalizeValueY(value, height, yMin, yMax)),
            Point2 = new WavesPoint(width, Valuation.NormalizeValueY(value, height, yMin, yMax)),
        };
    }

    /// <summary>
    /// Generates default ticks.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="ticks">Ticks.</param>
    /// <param name="signaturesFormat">Signatures format.</param>
    public static void GenerateDefaultTicks(
        this IWavesChart chart,
        List<WavesAxisTick> ticks,
        string signaturesFormat)
    {
        ticks?.Clear();
        ticks ??= new List<WavesAxisTick>();

        if (chart.CurrentXMin is double || chart.CurrentXMax is double)
        {
            ticks.GenerateAxisTicks(
                (double)chart.CurrentXMin,
                (double)chart.CurrentXMax,
                chart.XAxisPrimaryTicksNumber,
                chart.XAxisAdditionalTicksNumber,
                WavesAxisTickOrientation.Horizontal);
        }
        else if (chart.CurrentXMin is DateTime dateTimeMin && chart.CurrentXMax is DateTime dateTimeMax)
        {
            ticks.GenerateAxisTicks(
                dateTimeMin,
                dateTimeMax,
                signaturesFormat,
                chart.XAxisPrimaryTicksNumber,
                chart.XAxisAdditionalTicksNumber,
                WavesAxisTickOrientation.Horizontal);
        }

        ticks.GenerateAxisTicks(
            chart.CurrentYMin,
            chart.CurrentYMax,
            chart.YAxisPrimaryTicksNumber,
            chart.YAxisAdditionalTicksNumber,
            WavesAxisTickOrientation.Vertical);
    }

    /// <summary>
    /// Generates point ticks.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="cache">Cache.</param>
    /// <param name="point">Pointer location.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="stroke">Stroke.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="yMax">Y max.</param>
    /// <param name="dashArray">Dash array.</param>
    /// <param name="strokeThickness">Stroke thickness.</param>
    /// <param name="opacity">Opacity.</param>
    public static void GeneratePointerTicks(
        this IWavesChart chart,
        List<IWavesDrawingObject> cache,
        WavesPoint point,
        double width,
        double height,
        WavesColor stroke,
        double xMin,
        double xMax,
        double yMin,
        double yMax,
        double[] dashArray,
        double strokeThickness = 1d,
        double opacity = 0.5d)
    {
        var line1 = new WavesLine
        {
            Color = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            Thickness = strokeThickness,
            Point1 = new WavesPoint(0, point.Y),
            Point2 = new WavesPoint(width, point.Y),
        };

        var line2 = new WavesLine
        {
            Color = stroke,
            DashPattern = dashArray,
            IsAntialiased = true,
            IsVisible = true,
            Opacity = opacity,
            Thickness = strokeThickness,
            Point1 = new WavesPoint(point.X, 0),
            Point2 = new WavesPoint(point.X, width),
        };

        chart.DrawingObjects?.Add(line1);
        chart.DrawingObjects?.Add(line2);
        cache.Add(line1);
        cache.Add(line2);
    }
}
