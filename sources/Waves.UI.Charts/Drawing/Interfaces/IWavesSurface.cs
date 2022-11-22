using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Interfaces;

/// <summary>
/// Interface of Waves surface.
/// </summary>
public interface IWavesSurface
{
    /// <summary>
    /// Gets or sets drawing object collection.
    /// </summary>
    public IWavesDrawingObjects? DrawingObjects { get; set; }
}
