using System;
using System.Threading.Tasks;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators;

/// <summary>
/// Random data point series generator.
/// </summary>
public class RandomDataCandleSeriesGenerator :
    ReactiveObject,
    IWaves2DRandomSeriesGenerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomDataCandleSeriesGenerator"/> class.
    /// </summary>
    public RandomDataCandleSeriesGenerator()
    {
        Length = 1000;
        XMin = DateTime.Now.AddDays(-1);
        XMax = DateTime.Now;
        YMin = 100;
        YMax = 1000;
    }

    /// <inheritdoc/>
    [Reactive]
    public int Length { get; set; }

    /// <inheritdoc/>
    [Reactive]
    public object XMin { get; set; }

    /// <inheritdoc/>
    [Reactive]
    public object XMax { get; set; }

    /// <inheritdoc/>
    [Reactive]
    public double YMin { get; set; }

    /// <inheritdoc/>
    [Reactive]
    public double YMax { get; set; }

    /// <inheritdoc/>
    public async Task<IWaves2DSeries> Generate()
    {
        var random = new RandomDataGenerator.Randomizers.RandomizerNumber<double>(new FieldOptionsDouble()
        {
            Min = -1,
            Max = 1,
        });

        var random1 = new RandomDataGenerator.Randomizers.RandomizerNumber<double>(new FieldOptionsDouble()
        {
            Min = -(YMax - YMin),
            Max = YMax - YMin,
        });

        var xMin = ValuesUtils.GetValue(XMin);
        var xMax = ValuesUtils.GetValue(XMax);
        var step = Math.Abs(xMax - xMin) / Length;
        var candles = new WavesCandle[Length];
        var volatility = 0.01m;
        var oldPrice = 500m;
        for (var i = 0; i < Length - 1; i++)
        {
            var newPrice = NewPrice(volatility, random, oldPrice, random1, out var low, out var high);
            candles[i].OpenDateTime = DateTime.FromOADate(xMin + i * step);
            candles[i].CloseDateTime = DateTime.FromOADate(xMin + (i + 1) * step);
            candles[i].Open = oldPrice;
            candles[i].Close = newPrice;
            candles[i].Low = high > low ? low : high;
            candles[i].High = high > low ? high : low;
            oldPrice = newPrice;
        }

        // last point
        var lastNewPrice = NewPrice(volatility, random, oldPrice, random1, out var lastLow, out var lastHigh);
        candles[Length - 1].OpenDateTime = DateTime.FromOADate(xMin + (Length - 1) * step);
        candles[Length - 1].CloseDateTime = DateTime.FromOADate(xMin + Length * step);
        candles[Length - 1].Open = oldPrice;
        candles[Length - 1].Close = lastNewPrice;
        candles[Length - 1].Low = lastHigh > lastLow ? lastLow : lastHigh;
        candles[Length - 1].High = lastHigh > lastLow ? lastHigh : lastLow;

        return new WavesCandleSeries(candles);
    }

    private static decimal NewPrice(
        decimal volatility,
        RandomizerNumber<double> random,
        decimal oldPrice,
        RandomizerNumber<double> random1,
        out decimal low,
        out decimal high)
    {
        var changePercent = 2 * volatility * Convert.ToDecimal(random.Generate());
        if (changePercent > volatility)
        {
            changePercent -= 2 * volatility;
        }

        var changeAmount = oldPrice * changePercent;
        var newPrice = oldPrice + changeAmount;
        low = newPrice - Convert.ToDecimal(random1.Generate());
        high = newPrice - Convert.ToDecimal(random1.Generate());
        return newPrice;
    }
}
