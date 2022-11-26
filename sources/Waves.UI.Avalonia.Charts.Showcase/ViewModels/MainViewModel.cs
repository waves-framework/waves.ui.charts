using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels;

/// <summary>
/// Main view model.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private List<IBinanceKline> _candles;
    private WavesPointSeries _series;

    /// <summary>
    /// Creates new instance of <see cref="MainViewModel"/>.
    /// </summary>
    public MainViewModel()
    {
        SelectedSymbol = "BTCUSDT";
        InitializeChart();
    }

    /// <summary>
    /// Get or sets symbol.
    /// </summary>
    [Reactive]
    public string SelectedSymbol { get; set; }

    /// <summary>
    /// Gets or sets selected series type.
    /// </summary>
    [Reactive]
    public WavesPointSeriesType SelectedSeriesType { get; set; }

    /// <summary>
    /// Gets or sets series type.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesPointSeriesType> SeriesTypes { get; set; }

    /// <summary>
    /// Gets or sets X Min.
    /// </summary>
    [Reactive]
    public double XMin { get; set; }

    /// <summary>
    /// Gets or sets X Max.
    /// </summary>
    [Reactive]
    public double XMax { get; set; }

    /// <summary>
    /// Gets or sets Y Min.
    /// </summary>
    [Reactive]
    public double YMin { get; set; }

    /// <summary>
    /// Gets or sets Y Max.
    /// </summary>
    [Reactive]
    public double YMax { get; set; }

    /// <summary>
    /// Gets or sets series.
    /// </summary>
    [Reactive]
    public ObservableCollection<IWavesPointSeries> Series { get; set; }

    /// <summary>
    /// Initializes chart.
    /// </summary>
    private async void InitializeChart()
    {
        Series = new ObservableCollection<IWavesPointSeries>();
        SeriesTypes = new ObservableCollection<WavesPointSeriesType>()
        {
            WavesPointSeriesType.Line,
            WavesPointSeriesType.Bar,
        };

        _candles = (await GetCandles()).ToList();

        var length = _candles.Count;

        var x = new double[length];
        var y = new double[length];

        for (var i = 0; i < length; i++)
        {
            x[i] = i / (double)length;
            y[i] = Convert.ToDouble(_candles[i].ClosePrice);
        }

        _series = new WavesPointSeries(x, y)
        {
            Color = WavesColor.Green,
            Type = SelectedSeriesType,
            DotType = WavesDotType.FilledCircle,
        };

        Series.Add(_series);

        XMin = x.Min();
        XMax = x.Max();
        YMin = y.Min();
        YMax = y.Max();

        this.WhenPropertyChanged(x => x.SelectedSeriesType).Subscribe(_ => Update());
    }

    private void Update()
    {
        var length = _candles.Count;

        var x = new double[length];
        var y = new double[length];

        for (var i = 0; i < length; i++)
        {
            x[i] = i / (double)length;
            y[i] = Convert.ToDouble(_candles[i].ClosePrice);
        }

        _series.Color = WavesColor.Green;
        _series.Type = SelectedSeriesType;
        _series.DotType = WavesDotType.FilledCircle;
        _series.Update(x, y);
    }

    private async Task<IEnumerable<IBinanceKline>> GetCandles()
    {
        using var client = new BinanceClient();
        var result = await client.SpotApi.ExchangeData.GetKlinesAsync(
            SelectedSymbol,
            KlineInterval.OneMinute,
            DateTime.Now.AddDays(-1),
            DateTime.Now);

        if (result.Success)
        {
            return result.Data;
        }

        throw new Exception($"Error requesting data: {result.Error.Message}");
    }
}
