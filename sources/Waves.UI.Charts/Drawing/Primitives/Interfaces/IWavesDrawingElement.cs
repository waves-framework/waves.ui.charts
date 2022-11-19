using System;
using System.Collections.Generic;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces;

/// <summary>
///     Interface for drawing element.
/// </summary>
public interface IWavesDrawingElement : IDisposable
{
    /// <summary>
    ///     Updates view.
    /// </summary>
    /// <param name="element">Drawing element.</param>
    void Update(object element);

    // /// <summary>
    // /// Draws pixel.
    // /// </summary>
    // /// <param name="pixel">Pixel.</param>
    // void Draw(WavesPixel pixel);
    //
    // /// <summary>
    // ///     Draws ellipse.
    // /// </summary>
    // /// <param name="ellipse">Ellipse.</param>
    // void Draw(WavesEllipse ellipse);

    /// <summary>
    /// Draws line.
    /// </summary>
    /// <param name="line">Line.</param>
    void Draw(WavesLine line);

    // /// <summary>
    // /// Draws lines.
    // /// </summary>
    // /// <param name="lines">Lines.</param>
    // void Draw(IEnumerable<WavesLine> lines);
    //
    // /// <summary>
    // /// Draws rectangle.
    // /// </summary>
    // /// <param name="rectangle">Rectangle.</param>
    // void Draw(WavesRectangle rectangle);
    //
    // /// <summary>
    // /// Draws text.
    // /// </summary>
    // /// <param name="text">Text.</param>
    // void Draw(WavesText text);
    //
    // /// <summary>
    // /// Draws image.
    // /// </summary>
    // /// <param name="image">Image.</param>
    // void Draw(WavesImage image);
    //
    // /// <summary>
    // ///     Measures text size.
    // /// </summary>
    // /// <param name="text">Text.</param>
    // /// <param name="paint">Paint.</param>
    // /// <returns>Text's size.</returns>
    // WavesSize MeasureText(string text, IWavesTextPaint paint);
}
