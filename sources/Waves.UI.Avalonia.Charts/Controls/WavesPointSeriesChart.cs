using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Extensions;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves point series chart.
/// </summary>
public class WavesPointSeriesChart : WavesSeriesChart<IWavesPointSeries, WavesPoint>, IStyleable
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
            switch (series.Type)
            {
                case WavesPointSeriesType.Line:
                    this.GenerateLineSeries(
                        series,
                        DrawingObjectsCache,
                        Bounds.Width,
                        Bounds.Height,
                        GetWavesColor(Background),
                        GetWavesColor(Foreground));
                    break;
                case WavesPointSeriesType.Bar:
                    this.GenerateBarSeries(
                        series,
                        DrawingObjectsCache,
                        Bounds.Width,
                        Bounds.Height,
                        GetWavesColor(Background),
                        GetWavesColor(Foreground));
                    break;
            }
        }

        PrepareGrid();

        if (!DrawingObjects.Any())
        {
            return;
        }

        RenderUpdate(context);
    }

    /// <summary>
    ///     Clears cache objects.
    /// </summary>
    private void ClearCacheObjects()
    {
        foreach (var obj in DrawingObjectsCache)
        {
            DrawingObjects?.Remove(obj);
        }

        DrawingObjectsCache.Clear();
    }
}
