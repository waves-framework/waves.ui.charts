using System;
using System.Collections.Generic;
using System.Globalization;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Utils;

/// <summary>
/// Grid generation utils.
/// </summary>
public static class GridUtils
{
    /// <summary>
    /// Generates grid objects.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="ticks">Ticks objects.</param>
    /// <param name="cache">Cache.</param>
    public static void GenerateGridObjects(
        this IWavesChart chart,
        List<WavesAxisTick> ticks,
        List<IWavesDrawingObject> cache)
    {
        if (chart.DrawingObjects is null)
        {
            throw new Exception("Collection of drawing object has not been initialized.");
        }

        if (ticks is null)
        {
            throw new Exception("Collection of drawing object has not been initialized.");
        }

        var currentXMin = ValuesUtils.GetValue(chart.CurrentXMin);
        var currentXMax = ValuesUtils.GetValue(chart.CurrentXMax);
        var currentYMin = ValuesUtils.GetValue(chart.CurrentYMin);
        var currentYMax = ValuesUtils.GetValue(chart.CurrentYMax);

        chart.ClearCache(cache);

        var textColor = chart.TextColor;
        var width = chart.SurfaceWidth;
        var height = chart.SurfaceHeight;
        var textStyle = chart.TextStyle;
        var renderer = chart.Renderer;
        var horizontalSignaturesAlignment = chart.HorizontalSignatureAlignment;
        var verticalSignaturesAlignment = chart.VerticalSignatureAlignment;

        double lineStrokeThickness = default;
        double lineOpacity = 1.0d;  // TODO;
        double[] lineDashArray = default;
        WavesColor lineColor = default;
        WavesPoint linePoint1;
        WavesPoint linePoint2;

        var signatureOpacity = 1.0d; // TODO;
        var signatureRectangleStrokeThickness = 1.0d;    // TODO;
        var signatureRectangleCornerRadius = 3.0d;       // TODO;
        var signatureRectangleBackground = chart.BackgroundColor;
        var signatureRectangleStroke = textColor;

        const int signatureMarginX = 12; // TODO
        const int signatureMarginY = 6;  // TODO

        foreach (var tick in ticks)
        {
            WavesSize signatureSize = default;
            WavesText signature = null;
            WavesRectangle signatureRectangle = null;

            if (tick.Type is WavesAxisTickType.Primary or WavesAxisTickType.Zero)
            {
                signature = new WavesText
                {
                    Style = textStyle,
                    Value = tick.Description,
                    IsVisible = true,
                    IsAntialiased = true,
                    Opacity = signatureOpacity,
                    Color = textColor,
                };

                signatureSize = renderer.MeasureText(signature);

                signatureRectangle = new WavesRectangle
                {
                    CornerRadius = signatureRectangleCornerRadius,
                    Opacity = signatureOpacity,
                    Fill = signatureRectangleBackground,
                    Stroke = signatureRectangleStroke,
                    StrokeThickness = signatureRectangleStrokeThickness,
                    Width = signatureSize.Width + signatureMarginX,
                    Height = signatureSize.Height + signatureMarginY,
                    IsVisible = true,
                    IsAntialiased = true,
                };
            }

            switch (tick.Orientation)
            {
                case WavesAxisTickOrientation.Horizontal:

                    // line points
                    (linePoint1, linePoint2) = GetLinePointsX(tick, width, height, currentXMin, currentXMax);

                    if (signature != null)
                    {
                        // signature location
                        SetSignatureLocationX(horizontalSignaturesAlignment, tick, width, height, currentXMin, currentXMax, signatureSize, signatureMarginX, signatureMarginY, signature, signatureRectangle);
                    }

                    break;
                case WavesAxisTickOrientation.Vertical:

                    // line points
                    (linePoint1, linePoint2) = GetLinePointsY(tick, width, height, currentYMin, currentYMax);

                    if (signature != null)
                    {
                        // signature location
                        SetSignatureLocationY(verticalSignaturesAlignment, tick, width, height, currentYMin, currentYMax, signatureSize, signatureMarginX, signatureMarginY, signature, signatureRectangle);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (tick.Type)
            {
                case WavesAxisTickType.Primary:
                    lineStrokeThickness = chart.XAxisPrimaryTickThickness;
                    lineDashArray = chart.XAxisPrimaryTicksDashArray;
                    lineColor = chart.XAxisPrimaryTicksColor;
                    break;
                case WavesAxisTickType.Additional:
                    lineStrokeThickness = chart.XAxisAdditionalTickThickness;
                    lineDashArray = chart.XAxisAdditionalTicksDashArray;
                    lineColor = chart.XAxisAdditionalTicksColor;
                    break;
                case WavesAxisTickType.Zero:
                    lineStrokeThickness = chart.XAxisZeroLineThickness;
                    lineDashArray = chart.XAxisZeroLineDashArray;
                    lineColor = chart.XAxisZeroLineColor;
                    break;
                case WavesAxisTickType.Marker:
                    // TODO:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var line = new WavesLine
            {
                Color = lineColor,
                DashPattern = lineDashArray,
                IsAntialiased = true,
                IsVisible = true,
                Opacity = lineOpacity,
                Thickness = lineStrokeThickness,
                Point1 = linePoint1,
                Point2 = linePoint2,
            };

            chart.AddDrawingObject(cache, line);

            if (signature != null)
            {
                chart.AddDrawingObject(cache, signatureRectangle);
                chart.AddDrawingObject(cache, signature);
            }
        }
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

    private static void GenerateAxisTicks(
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

    private static void GenerateAxisTicks(
        this List<WavesAxisTick> ticks,
        DateTime dateMin,
        DateTime dateMax,
        string format,
        int primaryTicksCount,
        int additionalTicksCount,
        WavesAxisTickOrientation orientation)
    {
        var span = dateMax - dateMin;
        var interval = GetInterval(span);

        var interval1 = span.Round(interval);
        var start = dateMin.Round(interval);
        var end = dateMax.Round(interval);
        var primaryTickStep = TimeSpan.FromTicks((end - start).Ticks / primaryTicksCount).Round(interval);
        var additionalTickStep = TimeSpan.FromTicks((end - start).Ticks / additionalTicksCount).Round(interval);

        if (primaryTickStep == TimeSpan.Zero)
        {
            return;
        }

        if (additionalTickStep == TimeSpan.Zero)
        {
            return;
        }

        for (var i = start; i <= end; i += primaryTickStep)
        {
            for (var j = i + additionalTickStep; j <= i + primaryTickStep - additionalTickStep; j += additionalTickStep)
            {
                ticks.Add(new WavesAxisTick
                {
                    Description = j.ToString(format),
                    Value = j.ToOADate(),
                    IsVisible = true,
                    Orientation = orientation,
                    Type = WavesAxisTickType.Additional,
                });
            }

            var description = i.ToString(format);
            ticks.Add(new WavesAxisTick
            {
                Description = description,
                Value = i.ToOADate(),
                IsVisible = true,
                Orientation = orientation,
                Type = WavesAxisTickType.Primary,
            });
        }
    }

    private static TimeSpan GetInterval(TimeSpan span)
    {
        var interval = TimeSpan.FromSeconds(1);

        if (span.TotalDays >= 1)
        {
            return TimeSpan.FromDays(1);
        }

        if (span.TotalHours >= 1)
        {
            return TimeSpan.FromHours(1);
        }

        if (span.TotalMinutes >= 1)
        {
            return TimeSpan.FromMinutes(1);
        }

        if (span.TotalSeconds >= 1)
        {
            return TimeSpan.FromSeconds(1);
        }

        if (span.TotalMilliseconds >= 1)
        {
            return TimeSpan.FromSeconds(1);
        }

        return interval;
    }

    private static DateTime Round(this DateTime dateTime, TimeSpan interval)
    {
        var halfIntervalTicks = (interval.Ticks + 1) >> 1;
        return dateTime.AddTicks(halfIntervalTicks - ((dateTime.Ticks + halfIntervalTicks) % interval.Ticks));
    }

    private static TimeSpan Round(this TimeSpan timeSpan, TimeSpan interval)
    {
        long ticks = (timeSpan.Ticks + interval.Ticks / 2 + 1) / interval.Ticks;
        return new TimeSpan(ticks * interval.Ticks);
    }

    private static void SetSignatureLocationX(
        WavesAxisHorizontalSignatureAlignment horizontalSignaturesAlignment,
        WavesAxisTick tick,
        double width,
        double height,
        double currentXMin,
        double currentXMax,
        WavesSize signatureSize,
        int signatureMarginX,
        int signatureMarginY,
        WavesText signature,
        WavesRectangle signatureRectangle)
    {
        var (signatureLocationX, signatureLocationY) = GetSignatureTextLocationX(
            horizontalSignaturesAlignment,
            tick.Value,
            width,
            height,
            currentXMin,
            currentXMax,
            signatureSize);

        var (signatureRectangleLocationX, signatureRectangleLocationY) = GetSignatureRectangleLocation(
            signatureLocationX,
            signatureLocationY,
            signatureMarginX,
            signatureMarginY);

        signature.Location = new WavesPoint(signatureLocationX, signatureLocationY);
        signatureRectangle.Location = new WavesPoint(signatureRectangleLocationX, signatureRectangleLocationY);
    }

    private static void SetSignatureLocationY(
        WavesAxisVerticalSignatureAlignment verticalSignaturesAlignment,
        WavesAxisTick tick,
        double width,
        double height,
        double currentYMin,
        double currentYMax,
        WavesSize signatureSize,
        int signatureMarginX,
        int signatureMarginY,
        WavesText signature,
        WavesRectangle signatureRectangle)
    {
        var (signatureLocationX, signatureLocationY) = GetSignatureTextLocationY(
            verticalSignaturesAlignment,
            tick.Value,
            width,
            height,
            currentYMin,
            currentYMax,
            signatureSize);

        var (signatureRectangleLocationX, signatureRectangleLocationY) = GetSignatureRectangleLocation(
            signatureLocationX,
            signatureLocationY,
            signatureMarginX,
            signatureMarginY);

        signature.Location = new WavesPoint(signatureLocationX - signatureMarginX, signatureLocationY);
        signatureRectangle.Location =
            new WavesPoint(signatureRectangleLocationX - signatureMarginX, signatureRectangleLocationY);
    }

    private static (WavesPoint, WavesPoint) GetLinePointsX(
        WavesAxisTick tick,
        double width,
        double height,
        double currentXMin,
        double currentXMax)
    {
        var point1 = new WavesPoint(ValuationUtils.NormalizeValueX(tick.Value, width, currentXMin, currentXMax), 0);
        var point2 = new WavesPoint(ValuationUtils.NormalizeValueX(tick.Value, width, currentXMin, currentXMax), height);
        return (point1, point2);
    }

    private static (WavesPoint, WavesPoint) GetLinePointsY(
        WavesAxisTick tick,
        double width,
        double height,
        double currentYMin,
        double currentYMax)
    {
        var point1 = new WavesPoint(0, ValuationUtils.NormalizeValueY(tick.Value, height, currentYMin, currentYMax));
        var point2 = new WavesPoint(width, ValuationUtils.NormalizeValueY(tick.Value, height, currentYMin, currentYMax));
        return (point1, point2);
    }

    private static (double, double) GetSignatureTextLocationX(
        WavesAxisHorizontalSignatureAlignment signatureAlignment,
        double value,
        double width,
        double height,
        double currentXMin,
        double currentXMax,
        WavesSize signatureSize)
    {
        double x, y;
        switch (signatureAlignment)
        {
            case WavesAxisHorizontalSignatureAlignment.Top:
                x = ValuationUtils.NormalizeValueX(
                    value,
                    width,
                    currentXMin,
                    currentXMax) - signatureSize.Width / 2;
                y = signatureSize.Height * 2;
                break;
            case WavesAxisHorizontalSignatureAlignment.Bottom:
                x = ValuationUtils.NormalizeValueX(
                    value,
                    width,
                    currentXMin,
                    currentXMax) - signatureSize.Width / 2;
                y = height - signatureSize.Height * 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(signatureAlignment), signatureAlignment, null);
        }

        return (x, y);
    }

    private static (double, double) GetSignatureTextLocationY(
        WavesAxisVerticalSignatureAlignment signatureAlignment,
        double value,
        double width,
        double height,
        double currentYMin,
        double currentYMax,
        WavesSize signatureSize)
    {
        double x, y;
        switch (signatureAlignment)
        {
            case WavesAxisVerticalSignatureAlignment.Left:
                x = signatureSize.Width;
                y = ValuationUtils.NormalizeValueY(value, height, currentYMin, currentYMax) - signatureSize.Height / 2;
                break;
            case WavesAxisVerticalSignatureAlignment.Right:
                x = width - signatureSize.Width;
                y = ValuationUtils.NormalizeValueY(value, height, currentYMin, currentYMax) - signatureSize.Height / 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(signatureAlignment), signatureAlignment, null);
        }

        return (x, y);
    }

    private static (double, double) GetSignatureRectangleLocation(double signatureLocationX, double signatureLocationY, double marginX, double marginY)
    {
        var x = signatureLocationX - marginX / 2.0d;
        var y = signatureLocationY - marginY / 2.0d;
        return (x, y);
    }
}
