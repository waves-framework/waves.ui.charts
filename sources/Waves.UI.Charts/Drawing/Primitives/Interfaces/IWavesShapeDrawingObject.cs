using System.Drawing;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces
{
    /// <summary>
    ///     Interface of primitive drawing object.
    /// </summary>
    public interface IWavesShapeDrawingObject : IWavesDrawingObject
    {
        /// <summary>
        ///     Gets or sets height.
        /// </summary>
        float Height { get; set; }

        /// <summary>
        ///     Gets or sets width.
        /// </summary>
        float Width { get; set; }

        /// <summary>
        ///     Gets or sets location.
        /// </summary>
        Point Location { get; set; }
    }
}
