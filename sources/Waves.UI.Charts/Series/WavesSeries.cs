using System;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves series.
/// </summary>
/// <typeparam name="T">Type of series data.</typeparam>
public abstract class WavesSeries : IWavesSeries
{
    /// <inheritdoc />
    public event EventHandler Updated;

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0d;

    /// <inheritdoc />
    public virtual void Update()
    {
        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public abstract void Draw(IWavesChart chart);

    /// <summary>
    /// Series updated invocator.
    /// </summary>
    protected virtual void OnSeriesUpdated()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }
}
