using System.Threading.Tasks;
using RandomDataGenerator.FieldOptions;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;
using Waves.UI.Charts.Series;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators;

/// <summary>
/// Random data point series generator.
/// </summary>
public class RandomDataPointSeriesGenerator : ISeriesGenerator<WavesPointSeries>
{
    /// <inheritdoc/>
    public int Length { get; set; }

    /// <inheritdoc/>
    public object XMin { get; set; }

    /// <inheritdoc/>
    public object XMax { get; set; }

    /// <inheritdoc/>
    public double YMin { get; set; }

    /// <inheritdoc/>
    public double YMax { get; set; }

    /// <inheritdoc/>
    public async Task<WavesPointSeries> Generate()
    {
        var random = new RandomDataGenerator.Randomizers.RandomizerNumber<double>(new FieldOptionsDouble());
        return null;
    }
}
