using System;
using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Interface of series.
/// </summary>
public interface IWavesSeries : IDisposable
{
    /// <summary>
    /// Series updated event.
    /// </summary>
    event EventHandler Updated;

    /// <summary>
    /// Gets or sets whether series is visible.
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets opacity.
    /// </summary>
    public double Opacity { get; set; }

    /// <summary>
    /// Updates series.
    /// </summary>
    void Update();

    /// <summary>
    /// Draws series.
    /// </summary>
    /// <param name="chart">Chart.</param>
    void Draw(IWavesChart chart);
}
