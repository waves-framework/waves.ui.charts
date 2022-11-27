using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
/// Waves text.
/// </summary>
public class WavesText : WavesDrawingObject
{
    /// <summary>
    /// Gets or sets text style.
    /// </summary>
    public WavesTextStyle Style { get; set; }

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets location.
    /// </summary>
    public WavesPoint Location { get; set; } = new (0, 0);

    /// <summary>
    /// Gets or sets fill.
    /// </summary>
    public WavesColor Color { get; set; } = WavesColor.Black;

    /// <inheritdoc/>
    public override void Draw(IWavesDrawingRenderer e)
    {
        if (Style == null || !IsVisible)
        {
            return;
        }

        e.Draw(this);
    }
}
