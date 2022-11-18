using System.Collections.Generic;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces
{
    /// <summary>
    ///     Interface of drawing object.
    /// </summary>
    public interface IWavesDrawingLayer : IWavesDrawingObject
    {
        /// <summary>
        /// Gets objects of layer.
        /// </summary>
        IList<IWavesDrawingObject> DrawingObjects { get; }

        /// <summary>
        /// Adds object to layer.
        /// </summary>
        /// <param name="obj">Drawing object.</param>
        void AddObject(IWavesDrawingObject obj);

        /// <summary>
        /// Removes object from layer.
        /// </summary>
        /// <param name="obj">Drawing object.</param>
        void RemoveObject(IWavesDrawingObject obj);
    }
}
