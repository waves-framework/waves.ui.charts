﻿using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Base abstract class for drawing objects.
/// </summary>
public abstract class WavesDrawingObject : IWavesDrawingObject
{
    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public bool IsAntialiased { get; set; } = true;

    /// <inheritdoc />
    public bool IsVisible { get; set; } = true;

    /// <inheritdoc />
    public double Opacity { get; set; } = 1.0f;

    /// <inheritdoc />
    public abstract void Draw(IWavesDrawingRenderer e);
}
