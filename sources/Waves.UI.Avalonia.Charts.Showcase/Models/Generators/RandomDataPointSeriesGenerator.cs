using System;
using System.Threading.Tasks;
using RandomDataGenerator.FieldOptions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators;

/// <summary>
/// Random data point series generator.
/// </summary>
public class RandomDataPointSeriesGenerator :
    ReactiveObject,
    IWaves2DRandomSeriesGenerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomDataPointSeriesGenerator"/> class.
    /// </summary>
    public RandomDataPointSeriesGenerator()
    {
        Length = 50000;
        XMin = 0;
        XMax = 1;
        YMin = 0;
        YMax = 1;
        Color = WavesColor.Random();
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

    /// <summary>
    /// Gets or sets series color.
    /// </summary>
    [Reactive]
    public WavesColor Color { get; set; }

    /// <inheritdoc/>
    public async Task<IWaves2DSeries> Generate()
    {
        var random = new RandomDataGenerator.Randomizers.RandomizerNumber<double>(new FieldOptionsDouble()
        {
            Min = YMin,
            Max = YMax,
        });

        var xMin = ValuesUtils.GetValue(XMin);
        var xMax = ValuesUtils.GetValue(XMax);
        var step = Math.Abs(xMax - xMin) / Length;
        var points = new WavesPoint[Length];
        for (var i = 0; i < Length; i++)
        {
            points[i].X = xMin + i * step;
            points[i].Y = random.Generate() !.Value;
        }

        return new WavesPointSeries(points)
        {
            Color = Color,
            DotColor = Color,
        };
    }
}
