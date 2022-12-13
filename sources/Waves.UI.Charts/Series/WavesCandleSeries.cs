using System;
using System.Collections.Concurrent;
using System.Linq;
using ReactiveUI.Fody.Helpers;
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
public class WavesCandleSeries : Waves2DSeries
{
    private readonly ConcurrentBag<IWavesDrawingObject> _cache = new ();

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    public WavesCandleSeries()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="data">Data.</param>
    public WavesCandleSeries(WavesCandle[] data)
    {
        Candles = data ?? throw new ArgumentNullException(nameof(data), "Data was not set.");
    }

    /// <summary>
    /// Gets or sets growing color.
    /// </summary>
    [Reactive]
    public WavesColor GrowingColor { get; set; } = WavesColor.Green;

    /// <summary>
    /// Gets or sets falling color.
    /// </summary>
    [Reactive]
    public WavesColor FallingColor { get; set; } = WavesColor.Red;

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public WavesCandle[] Candles { get; protected set; }

    /// <inheritdoc />
    public override object XMin => Candles.Min(x => x.OpenDateTime);

    /// <inheritdoc />
    public override object XMax => Candles.Max(x => x.CloseDateTime);

    /// <inheritdoc />
    public override double YMin => Convert.ToDouble(Candles.Min(x => x.Low));

    /// <inheritdoc />
    public override double YMax => Convert.ToDouble(Candles.Max(x => x.High));

    /// <inheritdoc />
    public override void Update()
    {
        OnSeriesUpdated();
    }

    /// <summary>
    /// Updates data.
    /// </summary>
    /// <param name="data">New candles.</param>
    public void Update(WavesCandle[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Data was not set.");
        }

        if (data.Length != Candles.Length)
        {
            Candles = new WavesCandle[data.Length];
        }

        for (var i = 0; i < Candles.Length; i++)
        {
            Candles[i] = data[i];
        }

        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public override void Draw(IWavesChart chart)
    {
        if (Candles == null)
        {
            return;
        }

        if (Candles.Length == 0)
        {
            return;
        }

        while (_cache.TryTake(out var obj))
        {
            chart.DrawingObjects.Remove(obj);
        }

        var currentXMin = ValuesUtils.GetValue(chart.CurrentXMin);
        var currentXMax = ValuesUtils.GetValue(chart.CurrentXMax);
        var currentYMin = ValuesUtils.GetValue(chart.CurrentYMin);
        var currentYMax = ValuesUtils.GetValue(chart.CurrentYMax);

        var candles = Candles;
        foreach (var candle in candles)
        {
            var color = candle.Close > candle.Open ? GrowingColor : FallingColor;

            var rectangleLocation = ValuationUtils.NormalizePoint(
                candle.OpenDateTime.ToOADate(),
                Convert.ToDouble(candle.Close > candle.Open ? candle.Close : candle.Open),
                chart.SurfaceWidth,
                chart.SurfaceHeight,
                currentXMin,
                currentYMin,
                currentXMax,
                currentYMax);

            var rectangleHeight = Convert.ToDouble(Math.Abs(candle.Close - candle.Open));
            var rectangleWidth = ValuationUtils.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax) - ValuationUtils.NormalizeValueX(
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

            // TODO
            if (rectangle.Width > 2)
            {
                rectangle.StrokeThickness = 2;
            }

            // TODO
            if (rectangle.Height < 3)
            {
                rectangle.StrokeThickness = 0;
                rectangle.Height = 3;
            }

            var linePositionX = (ValuationUtils.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax) + ValuationUtils.NormalizeValueX(
                candle.OpenDateTime.ToOADate(),
                chart.SurfaceWidth,
                currentXMin,
                currentXMax)) / 2;

            var line = new WavesLine()
            {
                Point1 = new WavesPoint(
                    linePositionX,
                    ValuationUtils.NormalizeValueY(
                     Convert.ToDouble(candle.High),
                     chart.SurfaceHeight,
                     currentYMin,
                     currentYMax)),
                Point2 = new WavesPoint(
                    linePositionX,
                    ValuationUtils.NormalizeValueY(
                        Convert.ToDouble(candle.Low),
                        chart.SurfaceHeight,
                        currentYMin,
                        currentYMax)),
                Color = color,
                Opacity = 0.75,
            };

            if (chart.DrawingObjects == null)
            {
                continue;
            }

            chart.DrawingObjects.Add(line);
            chart.DrawingObjects.Add(rectangle);

            _cache.Add(line);
            _cache.Add(rectangle);
        }
    }
}
