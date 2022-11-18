using System;
using System.Collections.Generic;
using SkiaSharp;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Skia;

/// <summary>
///     Skia drawing element.
/// </summary>
public class SkiaDrawingElement : IWavesDrawingElement
{
    private SKSurface _surface;
    private ICollection<IWavesDrawingObject> _drawingObjects;

    /// <inheritdoc />
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Update(object element)
    {
        if (element is not SKSurface surface)
        {
            return;
        }

        if (_surface == null || _surface.Handle == IntPtr.Zero)
        {
            _surface = surface;
        }

        _surface.Canvas.Clear(SKColor.Empty);

        if (_drawingObjects == null)
        {
            return;
        }

        foreach (var obj in _drawingObjects)
        {
            obj.Draw(this);
        }
    }

    /// <inheritdoc />
    public void Draw(IWavesDrawingObject obj)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Draw(WavesLine line)
    {
        throw new NotImplementedException();
    }
}
