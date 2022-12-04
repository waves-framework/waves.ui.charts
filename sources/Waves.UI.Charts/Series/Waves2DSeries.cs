using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Charts.Series;

/// <summary>
/// Waves 2D series abstraction.
/// </summary>
public abstract class Waves2DSeries : WavesSeries, IWaves2DSeries
{
    /// <inheritdoc />
    public abstract object XMin { get; }

    /// <inheritdoc />
    public abstract object XMax { get; }

    /// <inheritdoc />
    public abstract double YMin { get; }

    /// <inheritdoc />
    public abstract double YMax { get; }
}
