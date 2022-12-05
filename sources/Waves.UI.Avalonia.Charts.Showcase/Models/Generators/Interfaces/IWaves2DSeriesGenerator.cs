using System.Threading.Tasks;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;

/// <summary>
/// Interface of series generator.
/// </summary>
public interface IWaves2DSeriesGenerator
{
    /// <summary>
    /// Generates data.
    /// </summary>
    /// <returns>Returns series.</returns>
    Task<IWaves2DSeries> Generate();
}
