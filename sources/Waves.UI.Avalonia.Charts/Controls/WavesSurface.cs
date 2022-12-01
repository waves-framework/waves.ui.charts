using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Metadata;
using Waves.UI.Avalonia.Charts.Extensions;
using Waves.UI.Avalonia.Charts.Primitives;
using Waves.UI.Avalonia.Charts.Renderer;
using Waves.UI.Avalonia.Charts.Renderer.Operations;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Renderer.Skia;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves drawing surface.
/// </summary>
public class WavesSurface :
    TemplatedControl,
    IWavesSurface
{
    /// <summary>
    ///     Defines the <see cref="DrawingObjects" /> property.
    /// </summary>
    public static readonly AttachedProperty<IWavesDrawingObjects> DrawingObjectsProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, IWavesDrawingObjects>(
            nameof(DrawingObjects),
            new WavesDrawingObjects(),
            true);

    private SkiaDrawingOperation _renderingLogic;

    /// <summary>
    ///     Creates new instance of <see cref="WavesSurface" />.
    /// </summary>
    public WavesSurface()
    {
        Renderer = new AvaloniaDrawingRenderer();

        AffectsRender<WavesSurface>(DesiredSizeProperty);
        AffectsRender<WavesSurface>(DrawingObjectsProperty);
        AffectsRender<WavesChart>(BackgroundProperty);
        AffectsRender<WavesChart>(ForegroundProperty);

        ForegroundProperty.Changed.Subscribe(OnForegroundChanged);
        BackgroundProperty.Changed.Subscribe(OnBackgroundChanged);

        TextColor = GetWavesColor(Foreground);
        BackgroundColor = GetWavesColor(Background);
    }

    /// <summary>
    ///     Gets or sets drawing objects.
    /// </summary>
    [Content]
    public IWavesDrawingObjects? DrawingObjects
    {
        get => GetValue(DrawingObjectsProperty);
        set => SetValue(DrawingObjectsProperty !, value);
    }

    /// <inheritdoc />
    public WavesColor TextColor { get; set; }

    /// <inheritdoc />
    public WavesColor BackgroundColor { get; set; }

    /// <summary>
    /// Gets renderer.
    /// </summary>
    public IWavesDrawingRenderer Renderer { get; }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        Refresh(context);
        base.Render(context);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _renderingLogic?.Dispose();
        Renderer?.Dispose();
    }

    /// <summary>
    /// Gets color.
    /// </summary>
    /// <param name="brush">Brush.</param>
    /// <returns>Waves color.</returns>
    protected static WavesColor GetWavesColor(IBrush brush)
    {
        if (brush is SolidColorBrush solidColorBrush)
        {
            var color = solidColorBrush.Color;
            return color.ToWavesColor();
        }

        if (brush is ImmutableSolidColorBrush immutableSolidColorBrush)
        {
            var color = immutableSolidColorBrush.Color;
            return color.ToWavesColor();
        }

        return WavesColor.Transparent;
    }

    /// <summary>
    /// Refresh image.
    /// </summary>
    /// <param name="context">Drawing context.</param>
    protected virtual void Refresh(DrawingContext context)
    {
        if (!DrawingObjects.Any())
        {
            return;
        }

        RenderUpdate(context);
    }

    /// <summary>
    /// Renders objects.
    /// </summary>
    /// <param name="context">Drawing context.</param>
    protected virtual void RenderUpdate(DrawingContext context)
    {
        if (Renderer is SkiaDrawingRenderer)
        {
            RenderSkia(context);
        }
        else
        {
            // avalonia renderer
            Renderer.Update(context, DrawingObjects, BackgroundColor);
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
        Renderer.Update(canvas, DrawingObjects, BackgroundColor);
        return true;
    }

    /// <summary>
    /// On foreground changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnForegroundChanged(AvaloniaPropertyChangedEventArgs<IBrush?> obj)
    {
        var newValue = obj.NewValue.Value;
        if (newValue != null)
        {
            TextColor = GetWavesColor(newValue);
        }
    }

    /// <summary>
    /// On background changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnBackgroundChanged(AvaloniaPropertyChangedEventArgs<IBrush?> obj)
    {
        var newValue = obj.NewValue.Value;
        if (newValue != null)
        {
            BackgroundColor = GetWavesColor(newValue);
        }
    }
}
