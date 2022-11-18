using System.Collections.Generic;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Drawing;

/// <summary>
///     Base abstract class for drawing objects.
/// </summary>
public class WavesDrawingLayer : WavesDrawingObject, IWavesDrawingLayer
{
    /// <inheritdoc />
    public IList<IWavesDrawingObject> DrawingObjects { get; } = new List<IWavesDrawingObject>();

    /// <inheritdoc />
    public void AddObject(IWavesDrawingObject obj)
    {
        DrawingObjects.Add(obj);
    }

    /// <inheritdoc />
    public void RemoveObject(IWavesDrawingObject obj)
    {
        DrawingObjects.Remove(obj);
    }

    /// <inheritdoc />
    public override void Draw(IWavesDrawingElement e)
    {
        if (DrawingObjects == null)
        {
            return;
        }

        foreach (var obj in DrawingObjects)
        {
            obj.Draw(e);
        }
    }
}
