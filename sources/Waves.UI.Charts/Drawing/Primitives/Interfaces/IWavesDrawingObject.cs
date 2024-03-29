﻿using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives.Interfaces;

/// <summary>
///     Interface of drawing object.
/// </summary>
public interface IWavesDrawingObject
{
    /// <summary>
    ///     Gets or sets id.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    ///     Gets whether drawing object is antialiased.
    /// </summary>
    bool IsAntialiased { get; set; }

    /// <summary>
    ///     Gets whether drawing object is visible.
    /// </summary>
    bool IsVisible { get; set; }

    /// <summary>
    ///     Gets or sets object's opacity.
    /// </summary>
    double Opacity { get; set; }

    /// <summary>
    ///     Draw object in current canvas.
    /// </summary>
    /// <param name="e">Renderer.</param>
    void Draw(IWavesDrawingRenderer e);
}
