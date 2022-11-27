using Waves.UI.Charts.Drawing.Primitives.Enums;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces;

/// <summary>
///     Text style interface.
/// </summary>
public interface IWavesTextStyle
{
    /// <summary>
    ///     Gets or sets font size.
    /// </summary>
    float FontSize { get; set; }

    /// <summary>
    ///     Gets or sets font family.
    /// </summary>
    string FontFamily { get; set; }

    /// <summary>
    ///     Gets or sets font weight.
    /// </summary>
    int Weight { get; set; }

    /// <summary>
    ///     Gets or sets whether the text may be in subpixels.
    /// </summary>
    bool IsSubpixelText { get; set; }

    /// <summary>
    ///     Gets or sets text alignment.
    /// </summary>
    WavesTextAlignment Alignment { get; set; }
}
