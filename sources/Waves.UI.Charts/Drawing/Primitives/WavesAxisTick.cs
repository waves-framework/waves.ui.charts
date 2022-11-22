using Waves.UI.Charts.Drawing.Primitives.Enums;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
/// Waves axis tick.
/// </summary>
public class WavesAxisTick
{
    /// <summary>
    /// Gets or sets whether tick is visible.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets type.
    /// </summary>
    public WavesAxisTickType Type { get; set; } = WavesAxisTickType.Primary;

    /// <summary>
    /// Gets or sets orientation.
    /// </summary>
    public WavesAxisTickOrientation Orientation { get; set; } = WavesAxisTickOrientation.Horizontal;
}
