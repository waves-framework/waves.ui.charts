using System;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Interfaces;

/// <summary>
/// Interface of Waves surface.
/// </summary>
public interface IWavesSurface : IDisposable
{
    /// <summary>
    /// Gets surfaces width.
    /// </summary>
    public double SurfaceWidth { get; }

    /// <summary>
    /// Gets surface height.
    /// </summary>
    public double SurfaceHeight { get; }

    /// <summary>
    /// Gets renderer.
    /// </summary>
    public IWavesDrawingRenderer Renderer { get; }

    /// <summary>
    /// Gets or sets text color.
    /// </summary>
    public WavesColor TextColor { get; set; }

    /// <summary>
    /// Gets or sets background color.
    /// </summary>
    public WavesColor BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets drawing object collection.
    /// </summary>
    public IWavesDrawingObjects DrawingObjects { get; set; }
}
