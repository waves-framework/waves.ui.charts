using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Data;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives
{
    /// <summary>
    ///     Primitive drawing object.
    /// </summary>
    public abstract class WavesShapeDrawingObject : WavesDrawingObject, IWavesShapeDrawingObject
    {
        /// <inheritdoc />
        public double Height { get; set; } = 0;

        /// <inheritdoc />
        public double Width { get; set; } = 0;

        /// <inheritdoc />
        public double StrokeThickness { get; set; } = 1;

        /// <inheritdoc />
        public WavesColor Fill { get; set; } = WavesColor.Black;

        /// <inheritdoc />
        public WavesColor Stroke { get; set; } = WavesColor.Gray;

        /// <inheritdoc />
        public WavesPoint Location { get; set; } = new WavesPoint(0, 0);

        /// <inheritdoc />
        public abstract override void Draw(IWavesDrawingRenderer e);
    }
}
