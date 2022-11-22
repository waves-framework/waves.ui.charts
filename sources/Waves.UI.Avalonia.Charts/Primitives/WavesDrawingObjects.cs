using Avalonia.Collections;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Avalonia.Charts.Primitives;

/// <summary>
/// Waves drawing objects.
/// </summary>
public class WavesDrawingObjects : AvaloniaList<IWavesDrawingObject>, IWavesDrawingObjects
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WavesDrawingObjects"/> class.
    /// </summary>
    public WavesDrawingObjects()
    {
        ResetBehavior = ResetBehavior.Remove;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WavesDrawingObjects"/> class.
    /// </summary>
    /// <param name="items">The initial items in the collection.</param>
    public WavesDrawingObjects(IEnumerable<IWavesDrawingObject> items)
        : base(items)
    {
        ResetBehavior = ResetBehavior.Remove;
    }
}
