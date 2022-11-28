using System;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves series.
/// </summary>
/// <typeparam name="T">Type of series data.</typeparam>
public class WavesSeries<T> : IWavesSeries<T>
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    public WavesSeries()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="WavesPointSeries" />.
    /// </summary>
    /// <param name="data">Data.</param>
    public WavesSeries(T[] data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data), "Data was not set.");
    }

    /// <inheritdoc />
    public event EventHandler Updated;

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0d;

    /// <summary>
    ///     Gets or sets point.
    /// </summary>
    public T[] Data { get; protected set; }

    /// <inheritdoc />
    public virtual void Update()
    {
        OnSeriesUpdated();
    }

    /// <inheritdoc />
    public void Update(T[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Data was not set.");
        }

        if (data.Length != Data.Length)
        {
            Data = new T[data.Length];
        }

        for (var i = 0; i < Data.Length; i++)
        {
            Data[i] = data[i];
        }

        OnSeriesUpdated();
    }

    /// <summary>
    /// Series updated invocator.
    /// </summary>
    protected virtual void OnSeriesUpdated()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }
}
