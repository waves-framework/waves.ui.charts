using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Presentation;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.UserControls;

/// <summary>
/// Point series chart view model.
/// </summary>
[WavesViewModel(typeof(CandleSeriesChartViewModel))]
public class CandleSeriesChartViewModel : WavesViewModelBase
{
    private IWavesSeries<IWavesSeriesData> _series;

    /// <summary>
    /// Creates new instance of <see cref="CandleSeriesChartViewModel"/>.
    /// </summary>
    public CandleSeriesChartViewModel()
    {
        XMin = 0d;
        YMin = 0d;
        XMax = 1d;
        YMax = 1d;

        Initialize();
    }

    /// <summary>
    /// Gets or sets X Min.
    /// </summary>
    [Reactive]
    public object XMin { get; set; }

    /// <summary>
    /// Gets or sets X Max.
    /// </summary>
    [Reactive]
    public object XMax { get; set; }

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
    public ObservableCollection<IWavesSeries<IWavesSeriesData>> Series { get; set; }

    /// <summary>
    /// Initializes chart.
    /// </summary>
    private async void Initialize()
    {
        Series = new ObservableCollection<IWavesSeries<IWavesSeriesData>>();

        InitializeChartData();
    }

    private async void InitializeChartData()
    {
        var bCandles = await GetCandles();
        var length = bCandles.Count;
        var startX = 1000d;
        var endX = 4000d;
        var step = (endX - startX) / length;
        var candles = new WavesCandle[length];
        var random = new Random();
        var volatility = 0.01m;
        var oldPrice = 500m;
        var minuteCount = 0;
        var now = DateTime.Now;

        //// https://stackoverflow.com/questions/8597731/are-there-known-techniques-to-generate-realistic-looking-fake-stock-data
        //// rnd = Random_Float(); // generate number, 0 <= x < 1.0
        //// change_percent = 2 * volatility * rnd;
        //// if (change_percent > volatility)
        ////     change_percent -= (2 * volatility);
        //// change_amount = old_price * change_percent;
        //// new_price = old_price + change_amount;

        for (var i = 0; i < length; i++)
        {
            //// var changePercent = 2 * volatility * Convert.ToDecimal(random.NextDouble());
            //// if (changePercent > volatility)
            //// {
            ////     changePercent -= 2 * volatility;
            //// }
            ////
            //// var changeAmount = oldPrice * changePercent;
            //// var newPrice = oldPrice + changeAmount;

            //// dt[i] = now.AddMinutes(minuteCount++);
            //// c[i] = newPrice;
            //// o[i] = oldPrice;
            //// h[i] = newPrice + Convert.ToDecimal(random.NextDouble());
            //// l[i] = newPrice - Convert.ToDecimal(random.NextDouble());

            candles[i] = new WavesCandle()
            {
                Open = bCandles[i].OpenPrice,
                Close = bCandles[i].ClosePrice,
                High = bCandles[i].HighPrice,
                Low = bCandles[i].LowPrice,
                OpenDateTime = bCandles[i].OpenTime,
                CloseDateTime = bCandles[i].CloseTime,
            };
        }

        _series = new WavesCandleSeries(candles);

        var xmin = candles.Min(x => x.OpenDateTime);
        var xmax = candles.Max(x => x.CloseDateTime);
        var ymin = candles.Min(x => x.Low);
        var ymax = candles.Max(x => x.High);

        ymin -= 10 * (ymax - ymin) / 100;
        ymax += 10 * (ymax - ymin) / 100;

        XMin = xmin;
        XMax = xmax;
        YMin = Convert.ToDouble(ymin);
        YMax = Convert.ToDouble(ymax);
        Series.Add(_series);
    }

    private async Task<List<IBinanceKline>> GetCandles()
    {
        using var client = new BinanceClient();
        var result = await client.SpotApi.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute, DateTime.Now.AddHours(-12), DateTime.Now);
        if (!result.Success)
        {
            throw new Exception($"Error requesting data: {result.Error.Message}");
        }

        return result.Data.ToList();
    }
}
