namespace Waves.UI.Charts.Drawing.Primitives.Interfaces;

/// <summary>
///     Interface of primitive drawing object.
/// </summary>
public interface IWavesShapeDrawingObject : IWavesDrawingObject
{
    /// <summary>
    ///     Gets or sets height.
    /// </summary>
    double Height { get; set; }

    /// <summary>
    ///     Gets or sets width.
    /// </summary>
    double Width { get; set; }

    /// <summary>
    ///     Gets or sets stroke thickness.
    /// </summary>
    double StrokeThickness { get; set; }

    /// <summary>
    ///     Gets or sets fill.
    /// </summary>
    WavesColor Fill { get; set; }

    /// <summary>
    ///     Gets or sets stroke.
    /// </summary>
    WavesColor Stroke { get; set; }

    /// <summary>
    ///     Gets or sets location.
    /// </summary>
    WavesPoint Location { get; set; }
}
