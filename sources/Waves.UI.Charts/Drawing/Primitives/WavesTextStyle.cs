using System.Collections.Generic;
using Waves.UI.Charts.Drawing.Primitives.Enums;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
/// Text style.
/// </summary>
public class WavesTextStyle
{
    /// <summary>
    /// Gets or sets font size.
    /// </summary>
    public double FontSize { get; set; } = 12;

    /// <summary>
    /// Gets or sets font family.
    /// </summary>
    public string FontFamily { get; set; } = "Segoe UI";

    /// <summary>
    /// Gets or sets font weight.
    /// </summary>
    public WavesFontWeight FontWeight { get; set; } = WavesFontWeight.Regular;

    /// <summary>
    /// Gets or sets whether text places in subpixel or not.
    /// </summary>
    public bool IsSubpixelText { get; set; } = true;

    /// <summary>
    /// Gets or sets text alignment.
    /// </summary>
    public WavesTextAlignment TextAlignment { get; set; } = WavesTextAlignment.Left;
}
