using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Skia;
using Waves.UI.Avalonia.Charts.Primitives;
using Waves.UI.Charts.Drawing.Skia;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves drawing surface.
/// </summary>
public class WavesSurface :
    Control
{
    /// <summary>
    /// Defines the <see cref="DrawingObjects"/> property.
    /// </summary>
    public static readonly AttachedProperty<WavesDrawingObjects> DrawingObjectsProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, WavesDrawingObjects>(
            nameof(DrawingObjects),
            new WavesDrawingObjects(),
            inherits: true);

    private readonly SkiaDrawingRenderer _renderer;
    private bool _changed = true;

    /// <summary>
    ///     Creates new instance of <see cref="WavesSurface" />.
    /// </summary>
    public WavesSurface()
    {
        _renderer = new SkiaDrawingRenderer();
        AffectsRender<WavesSurface>(DrawingObjectsProperty);
    }

    /// <summary>
    ///     Gets or sets drawing objects.
    /// </summary>
    [Content]
    public WavesDrawingObjects? DrawingObjects
    {
        get => GetValue(DrawingObjectsProperty);
        set
        {
            _changed = true;
            SetValue(DrawingObjectsProperty, value);
        }
    }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        Refresh(context);
        base.Render(context);
    }

    /// <summary>
    ///     Refresh image.
    /// </summary>
    private void Refresh(DrawingContext context)
    {
        //// if (!_changed)
        //// {
        ////     return;
        //// }

        _changed = false;

        if (!DrawingObjects.Any())
        {
            return;
        }

        var feature = context.PlatformImpl.GetFeature<ISkiaSharpApiLeaseFeature>();
        if (feature == null)
        {
            return;
        }

        var surface = feature.Lease().SkSurface;
        _renderer.Update(surface, DrawingObjects);
    }
}
