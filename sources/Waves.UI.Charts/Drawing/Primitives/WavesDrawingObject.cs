using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Drawing;

/// <summary>
///     Base abstract class for drawing objects.
/// </summary>
public abstract class WavesDrawingObject : IWavesDrawingObject
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public bool IsAntialiased { get; set; } = true;

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public float Opacity { get; set; } = 1.0f;

    /// <inheritdoc />
    public float StrokeThickness { get; set; } = 1;

    /// <inheritdoc />
    public Color Fill { get; set; } = Color.Black;

    /// <inheritdoc />
    public Color Stroke { get; set; } = Color.Gray;

    /// <inheritdoc />
    public abstract void Draw(IWavesDrawingRenderer e);
}
