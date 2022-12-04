using System.Threading.Tasks;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;

/// <summary>
/// Interface of series generator.
/// </summary>
/// <typeparam name="T">Type of series generator.</typeparam>
public interface ISeriesGenerator<T>
    where T : IWavesSeries
{
    /// <summary>
    /// Gets or sets length.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets XMin.
    /// </summary>
    public object XMin { get; set; }

    /// <summary>
    /// Gets or sets XMax.
    /// </summary>
    public object XMax { get; set; }

    /// <summary>
    /// Gets or sets YMin
    /// </summary>
    public double YMin { get; set; }

    /// <summary>
    /// Gets or sets XMax.
    /// </summary>
    public double YMax { get; set; }

    /// <summary>
    /// Generates data.
    /// </summary>
    /// <returns>Returns series.</returns>
    Task<T> Generate();
}
