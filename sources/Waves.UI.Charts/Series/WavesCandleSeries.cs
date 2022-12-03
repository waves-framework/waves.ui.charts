using System;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Candle series.
/// </summary>
/// <typeparam name="WavesCandle">Type of waves candle.</typeparam>
public class WavesCandleSeries<WavesCandle> : WavesSeries<WavesCandle>, IWavesSeries<WavesCandle>
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesCandleSeries" />.
    /// </summary>
    public WavesCandleSeries()
        : base()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesCandleSeries" />.
    /// </summary>
    /// <param name="data">Data.</param>
    public WavesCandleSeries(WavesCandle[] data)
        : base(data)
    {
    }

    /// <summary>
    /// Gets or sets growing color.
    /// </summary>
    public WavesColor GrowingColor { get; set; }

    /// <summary>
    /// Gets or sets falling color.
    /// </summary>
    public WavesColor FallingColor { get; set; }

    /// <inheritdoc />
    public override void Draw(IWavesChart chart)
    {
        if (Data == null)
        {
            return;
        }

        if (Data.Length == 0)
        {
            return;
        }

        var currentXMin = Values.GetValue(chart.CurrentXMin);
        var currentXMax = Values.GetValue(chart.CurrentXMax);
        var currentYMin = Values.GetValue(chart.CurrentYMin);
        var currentYMax = Values.GetValue(chart.CurrentYMax);

        var candles = Data;
        foreach (var candle in candles)
        {
            var color = candle.Close > candle.Open ? GrowingColor : FallingColor;

            var rectangleLocation = Valuation.NormalizePoint(
                candle.OpenDateTime.ToOADate(),
                Convert.ToDouble(candle.High),
                chart.SurfaceWidth,
                chart.SurfaceHeight,
                currentXMin,
                currentYMin,
                currentXMax,
                currentYMax);

            var rectangleHeight = Convert.ToDouble(Math.Abs(candle.Close - candle.Open));
            var rectangleWidth = Valuation.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax) - Valuation.NormalizeValueX(
                candle.OpenDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax);

            rectangleLocation = new WavesPoint(rectangleLocation.X, rectangleLocation.Y);
            var rectangle = new WavesRectangle
            {
                Fill = color,
                Location = rectangleLocation,
                Width = rectangleWidth,
                Height = rectangleHeight,
                StrokeThickness = 0,
                Stroke = chart.BackgroundColor,
            };

            if (rectangle.Width > 2)
            {
                rectangle.StrokeThickness = 2;
            }

            var linePositionX = (Valuation.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax) + Valuation.NormalizeValueX(
                candle.OpenDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax)) / 2;

            var line = new WavesLine()
            {
                Point1 = new WavesPoint(
                    linePositionX,
                    Valuation.NormalizeValueY(
                     Convert.ToDouble(candle.High),
                     chart.SurfaceHeight,
                     currentYMin,
                     currentYMax)),
                Point2 = new WavesPoint(
                    linePositionX,
                    Valuation.NormalizeValueY(
                        Convert.ToDouble(candle.Low),
                        chart.SurfaceHeight,
                        currentYMin,
                        currentYMax)),
                Color = color,
                Opacity = 0.75,
            };

            if (chart.DrawingObjects != null)
            {
                chart.DrawingObjects.Add(line);
                chart.DrawingObjects.Add(rectangle);

                chart.SetCache(line);
                chart.SetCache(rectangle);
            }
        }
    }
}
