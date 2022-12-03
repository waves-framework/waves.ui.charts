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
public class WavesCandleSeries :
    IWavesSeries<WavesCandle>
{
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
        Data = data ?? throw new ArgumentNullException(nameof(data), "Data was not set.");
    }

    /// <inheritdoc />
    public event EventHandler Updated;

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0d;

    /// <summary>
    /// Gets or sets growing color.
    /// </summary>
    public WavesColor GrowingColor { get; set; }

    /// <summary>
    /// Gets or sets falling color.
    /// </summary>
    public WavesColor FallingColor { get; set; }

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public WavesCandle[] Data { get; protected set; }

    /// <inheritdoc />
    public virtual void Update()
    {
        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public void Update(WavesCandle[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Data was not set.");
        }

        if (data.Length != Data.Length)
        {
            Data = new WavesCandle[data.Length];
        }

        for (var i = 0; i < Data.Length; i++)
        {
            Data[i] = data[i];
        }

        OnSeriesUpdated();
    }

    /// <summary>
    /// Updates data.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Update(IWavesSeriesData[] data)
    {
        Update(data);
    }

    /// <inheritdoc />
    public virtual void Draw(IWavesChart chart)
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

    /// <summary>
    /// Series updated invocator.
    /// </summary>
    protected virtual void OnSeriesUpdated()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }
}
