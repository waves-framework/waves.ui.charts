using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Rectangle object.
/// </summary>
public class WavesRectangle : WavesShapeDrawingObject
{
    /// <summary>
    ///     Gets or sets corner radius.
    /// </summary>
    public double CornerRadius { get; set; } = 1;

    /// <inheritdoc />
    public override void Draw(IWavesDrawingRenderer e)
    {
        if (!IsVisible)
        {
            return;
        }

        e.Draw(this);
    }
}
