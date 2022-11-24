using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Series.
/// </summary>
public interface IWavesSeries
{
    /// <summary>
    /// Gets or sets whether series is visible.
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets opacity.
    /// </summary>
    public double Opacity { get; set; }

    /// <summary>
    ///     Gets or sets dash pattern.
    /// </summary>
    public double[] DashPattern { get; set; }

    /// <summary>
    /// Gets or sets stroke thickness.
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    /// Gets or sets fill.
    /// </summary>
    public WavesColor Color { get; set; }
}
