using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Line.
/// </summary>
public class WavesPolyline : WavesDrawingObject
{
    /// <summary>
    ///     Gets or sets points.
    /// </summary>
    public WavesPoint[] Points { get; set; }

    /// <summary>
    ///     Gets or sets dash pattern.
    /// </summary>
    public double[] DashPattern { get; set; } = { 0, 0, 0, 0 };

    /// <summary>
    /// Gets or sets stroke thickness.
    /// </summary>
    public double Thickness { get; set; } = 1;

    /// <summary>
    /// Gets or sets fill.
    /// </summary>
    public WavesColor Color { get; set; } = WavesColor.Black;

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
