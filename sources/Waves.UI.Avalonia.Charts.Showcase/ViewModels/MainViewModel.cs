using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels;

/// <summary>
/// Main view model.
/// </summary>
public class MainViewModel : ViewModelBase
{
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

        var candles = (await GetCandles()).ToList();
        var length = candles.Count;

        var x = new double[length];
        var y = new double[length];

        for (var i = 0; i < length; i++)
        {
            x[i] = i / (double)length;
            y[i] = Convert.ToDouble(candles[i].ClosePrice);
        }

        XMin = x.Min();
        XMax = x.Max();
        YMin = y.Min();
        YMax = y.Max();

        var series = new WavesPointSeries(x, y)
        {
            Color = WavesColor.Red
        };

        Series.Add(series);

        var phase = 0d;
        //// var task = new Task(async () =>
        //// {
        ////     await Task.Delay(1000);
        ////
        ////     do
        ////     {
        ////         var random = new Random();
        ////         var x = new double[length];
        ////         var y = new double[length];
        ////         phase += 0.01;
        ////         for (var i = 0; i < length; i++)
        ////         {
        ////             var randomValue = random.NextDouble() / 10;
        ////             x[i] = i / (double)length;
        ////             y[i] = randomValue + 0.8 * Math.Sin(i / 1000.0d + phase);
        ////         }
        ////
        ////         if (phase > 2 * Math.PI)
        ////         {
        ////             phase = 0;
        ////         }
        ////
        ////         await Dispatcher.UIThread.InvokeAsync(() => { series?.Update(x, y); });
        ////         await Task.Delay(33);
        ////     }
        ////     while (true);
        //// });
        //// task.Start();
    }

    private async Task<IEnumerable<IBinanceKline>> GetCandles()
    {
        using (var client = new BinanceClient())
        {
            var result = await client.SpotApi.ExchangeData.GetKlinesAsync(
                SelectedSymbol,
                KlineInterval.FiveMinutes,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

            if (result.Success)
            {
                return result.Data;
            }
            else
            {
                throw new Exception($"Error requesting data: {result.Error.Message}");
            }
        }
    }
}
