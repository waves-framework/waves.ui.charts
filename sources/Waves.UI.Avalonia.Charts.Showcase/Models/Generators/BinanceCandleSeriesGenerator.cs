using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using ExCSS;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators;

/// <summary>
/// Binance data generator.
/// </summary>
public class BinanceCandleSeriesGenerator :
    ReactiveObject,
    IWaves2DSeriesGenerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinanceCandleSeriesGenerator"/> class.
    /// </summary>
    public BinanceCandleSeriesGenerator()
    {
        var now = DateTime.Now;
        Symbol = "BTCUSDT";
        StartDateTime = now.AddDays(-3);
        StopDateTime = now;
    }

    /// <summary>
    /// Gets or sets symbol.
    /// </summary>
    [Reactive]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets start date time.
    /// </summary>
    [Reactive]
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Gets or sets stop date time.
    /// </summary>
    [Reactive]
    public DateTime StopDateTime { get; set; }

    /// <inheritdoc />
    public async Task<IWaves2DSeries> Generate()
    {
        var bCandles = await GetCandles();
        var length = bCandles.Count;
        var candles = new WavesCandle[length];

        for (var i = 0; i < length; i++)
        {
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

        return new WavesCandleSeries(candles);
    }

    private async Task<List<IBinanceKline>> GetCandles()
    {
        using var client = new BinanceClient();
        var result = await client.SpotApi.ExchangeData.GetKlinesAsync(Symbol, KlineInterval.OneMinute, StartDateTime, StopDateTime);
        if (!result.Success)
        {
            throw new Exception($"Error requesting data: {result.Error.Message}");
        }

        return result.Data.ToList();
    }
}
