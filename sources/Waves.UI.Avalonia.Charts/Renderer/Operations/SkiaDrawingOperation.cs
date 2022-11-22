using Avalonia;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;

namespace Waves.UI.Avalonia.Charts.Renderer.Operations;

/// <summary>
/// Skia drawing operation.
/// </summary>
public class SkiaDrawingOperation : ICustomDrawOperation
{
    /// <inheritdoc />
    public Rect Bounds { get; set; }

    /// <summary>
    /// Render action.
    /// </summary>
    public Func<object, object> RenderAction { get; set; }

    /// <inheritdoc />
    public bool HitTest(Point p) => false;

    /// <inheritdoc/>
    public bool Equals(ICustomDrawOperation other) => false;

    /// <inheritdoc />
    public void Render(IDrawingContextImpl context)
    {
        var feature = context.GetFeature<ISkiaSharpApiLeaseFeature>();
        if (feature == null)
        {
            return;
        }

        var lease = feature.Lease();
        RenderAction.Invoke(lease.SkCanvas);
        lease.Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}
