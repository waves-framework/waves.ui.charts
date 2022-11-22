using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Drawing;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Line.
/// </summary>
public class WavesLine : WavesDrawingObject
{
    /// <summary>
    ///     Gets or sets first point.
    /// </summary>
    public Point Point1 { get; set; }

    /// <summary>
    ///     Gets or sets second point.
    /// </summary>
    public Point Point2 { get; set; }

    /// <summary>
    ///     Gets or sets dash pattern.
    /// </summary>
    public float[] DashPattern { get; set; } = { 0, 0, 0, 0 };

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
