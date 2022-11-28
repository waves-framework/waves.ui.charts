using System;
using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Series.Interfaces;

/// <summary>
/// Interface of series.
/// </summary>
/// <typeparam name="T">Type of series data.</typeparam>
public interface IWavesSeries<T>
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
    ///     Gets or sets point.
    /// </summary>
    public T[] Data { get; }

    /// <summary>
    /// Updates series.
    /// </summary>
    void Update();

    /// <summary>
    /// Updates series.
    /// </summary>
    /// <param name="data">Series data.</param>
    void Update(T[] data);
}
