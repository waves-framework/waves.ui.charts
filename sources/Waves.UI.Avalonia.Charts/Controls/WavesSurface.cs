using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using Waves.UI.Avalonia.Charts.Primitives;
using Waves.UI.Avalonia.Charts.Renderer;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Drawing.Skia;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves drawing surface.
/// </summary>
public class WavesSurface :
    Control
{
    /// <summary>
    ///     Defines the <see cref="DrawingObjects" /> property.
    /// </summary>
    public static readonly AttachedProperty<WavesDrawingObjects> DrawingObjectsProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, WavesDrawingObjects>(
            nameof(DrawingObjects),
            new WavesDrawingObjects(),
            true);

    private readonly IWavesDrawingRenderer _renderer;
    private SkiaDrawingOperation _renderingLogic;

    /// <summary>
    ///     Creates new instance of <see cref="WavesSurface" />.
    /// </summary>
    public WavesSurface()
    {
        _renderer = new AvaloniaDrawingRenderer();

        AffectsRender<WavesSurface>(DesiredSizeProperty);
        AffectsRender<WavesSurface>(DrawingObjectsProperty);
    }

    /// <summary>
    ///     Gets or sets drawing objects.
    /// </summary>
    [Content]
    public WavesDrawingObjects? DrawingObjects
    {
        get => GetValue(DrawingObjectsProperty);
        set => SetValue(DrawingObjectsProperty, value);
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
        if (!DrawingObjects.Any())
        {
            return;
        }

        if (_renderer is SkiaDrawingRenderer)
        {
            RenderSkia(context);
        }
        else
        {
            // avalonia renderer
            _renderer.Update(context, DrawingObjects);
        }
    }

    /// <summary>
    /// Skia render operations.
    /// </summary>
    /// <param name="context">Drawing context.</param>
    private void RenderSkia(DrawingContext context)
    {
        if (_renderingLogic == null || _renderingLogic.Bounds != Bounds)
        {
            _renderingLogic?.Dispose();
            _renderingLogic = new SkiaDrawingOperation();
            _renderingLogic.RenderAction += OnSkiaRendering;
            _renderingLogic.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
        }

        _renderingLogic.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
        context.Custom(_renderingLogic);
    }

    /// <summary>
    /// Skia rendering actions.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <returns>Not used.</returns>
    private object OnSkiaRendering(object canvas)
    {
        _renderer.Update(canvas, DrawingObjects);
        return true;
    }
}
