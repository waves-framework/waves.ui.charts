using System.Threading.Tasks;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;

/// <summary>
/// Interface of series generator.
/// </summary>
public interface IWaves2DRandomSeriesGenerator : IWaves2DSeriesGenerator
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
}
