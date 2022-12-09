using System;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves series.
/// </summary>
/// <typeparam name="T">Type of series data.</typeparam>
public abstract class WavesSeries : ReactiveObject, IWavesSeries
{
    private readonly IDisposable _propertyChangedDisposable;

    /// <summary>
    /// Initializes a new instance of the <see cref="WavesSeries"/> class.
    /// </summary>
    public WavesSeries()
    {
        // default properties
        IsVisible = true;
        Opacity = 1.0d;

        // disposables
        _propertyChangedDisposable = this.WhenAnyPropertyChanged().Subscribe(_ => OnSeriesUpdated());
    }

    /// <inheritdoc />
    public event EventHandler Updated;

    /// <inheritdoc />
    [Reactive]
    public bool IsVisible { get; set; }

    /// <inheritdoc />
    [Reactive]
    public double Opacity { get; set; }

    /// <inheritdoc />
    public virtual void Update()
    {
        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public abstract void Draw(IWavesChart chart);

    /// <inheritdoc />
    public void Dispose()
    {
        _propertyChangedDisposable?.Dispose();
    }

    /// <summary>
    /// Series updated invocator.
    /// </summary>
    protected virtual void OnSeriesUpdated()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }
}
