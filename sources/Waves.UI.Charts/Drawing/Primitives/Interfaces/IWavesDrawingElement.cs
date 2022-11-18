using System.Collections.Generic;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces
{
    /// <summary>
    ///     Interface for drawing element.
    /// </summary>
    public interface IWavesDrawingElement
    {
        /// <summary>
        /// Gets or sets drawing elements.
        /// </summary>
        IEnumerable<IWavesDrawingObject> DrawingObjects { get; set; }

        /// <summary>
        ///  Updates view.
        /// </summary>
        /// <param name="element">Drawing element.</param>
        void Update(object element);
    }
}
