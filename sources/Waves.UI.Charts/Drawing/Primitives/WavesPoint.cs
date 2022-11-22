namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
/// Waves point.
/// </summary>
public struct WavesPoint
{
    /// <summary>
    /// Creates new instance of <see cref="WavesPoint"/>.
    /// </summary>
    /// <param name="x">X.</param>
    /// <param name="y">Y.</param>
    public WavesPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Gets X value.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets y value.
    /// </summary>
    public double Y { get; }
}
