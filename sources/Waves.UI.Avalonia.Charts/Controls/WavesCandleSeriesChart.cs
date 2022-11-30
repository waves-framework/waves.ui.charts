using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves candle series chart.
/// </summary>
public class WavesCandleSeriesChart : WavesSeriesChart<IWavesCandleSeries, WavesCandle>, IStyleable
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
        IWavesCandleSeries series,
        List<IWavesDrawingObject> drawingObjectsCache,
        double boundsWidth,
        double boundsHeight,
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

        var candles = series.Data;
        foreach (var candle in candles)
        {
            var color = candle.Close > candle.Open ? growingColor : fallingColor;
            //// var rectangleLocation = Valuation.NormalizePoint(
            ////     candle.DateTime.ToOADate(),
            ////     candle.)
            var rectangle = new WavesRectangle()
            {
                Fill = color,
                Location = new WavesPoint(),
            };
        }
    }
}
