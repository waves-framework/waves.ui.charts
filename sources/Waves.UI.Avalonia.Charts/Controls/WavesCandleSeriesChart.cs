using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves candle series chart.
/// </summary>
public class WavesCandleSeriesChart : WavesSeriesChart<IWavesCandleSeries, WavesCandle>, IStyleable
{
    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);
}
