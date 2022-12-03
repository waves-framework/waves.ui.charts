using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves candle series chart.
/// </summary>
public class WavesCandleSeriesChart : WavesSeriesChart<WavesCandleSeries, WavesCandle>, IStyleable
{
    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        PrepareBackground();
        ClearCacheObjects();

        foreach (var series in Series)
        {
            GenerateCandleSeries(
                series,
                DrawingObjectsCache,
                Bounds.Width,
                Bounds.Height,
                WavesColor.Green,
                WavesColor.Red);
        }

        PrepareGrid();

        if (!DrawingObjects.Any())
        {
            return;
        }

        RenderUpdate(context);
    }

    private void GenerateCandleSeries(
        WavesCandleSeries series,
        List<IWavesDrawingObject> drawingObjectsCache,
        double width,
        double height,
        WavesColor growingColor,
        WavesColor fallingColor)
    {
        if (series.Data == null)
        {
            return;
        }

        if (series.Data.Length == 0)
        {
            return;
        }

        var currentXMin = Values.GetValue(CurrentXMin);
        var currentXMax = Values.GetValue(CurrentXMax);
        var currentYMin = Values.GetValue(CurrentYMin);
        var currentYMax = Values.GetValue(CurrentYMax);

        var candles = series.Data;
        foreach (var candle in candles)
        {
            var color = candle.Close > candle.Open ? growingColor : fallingColor;

            var rectangleLocation = Valuation.NormalizePoint(
                candle.OpenDateTime.ToOADate(),
                Convert.ToDouble(candle.High),
                width,
                height,
                currentXMin,
                currentYMin,
                currentXMax,
                currentYMax);

            var rectangleHeight = Convert.ToDouble(Math.Abs(candle.Close - candle.Open));
            var rectangleWidth = Valuation.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                width,
                currentXMin,
                currentXMax) - Valuation.NormalizeValueX(
                candle.OpenDateTime.ToOADate(),
                width,
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
                Stroke = BackgroundColor,
            };

            if (rectangle.Width > 2)
            {
                rectangle.StrokeThickness = 2;
            }

            var linePositionX = (Valuation.NormalizeValueX(
                candle.CloseDateTime.ToOADate(),
                width,
                currentXMin,
                currentXMax) + Valuation.NormalizeValueX(
                candle.OpenDateTime.ToOADate(),
                width,
                currentXMin,
                currentXMax)) / 2;

            var line = new WavesLine()
            {
                Point1 = new WavesPoint(
                    linePositionX,
                    Valuation.NormalizeValueY(
                     Convert.ToDouble(candle.High),
                     height,
                     currentYMin,
                     currentYMax)),
                Point2 = new WavesPoint(
                    linePositionX,
                    Valuation.NormalizeValueY(
                        Convert.ToDouble(candle.Low),
                        height,
                        currentYMin,
                        currentYMax)),
                Color = color,
                Opacity = 0.75,
            };

            DrawingObjects.Add(line);
            DrawingObjectsCache.Add(line);
            DrawingObjects.Add(rectangle);
            DrawingObjectsCache.Add(rectangle);
        }
    }
}
