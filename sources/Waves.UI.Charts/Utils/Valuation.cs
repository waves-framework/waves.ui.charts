using System.Collections.Generic;
using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Utils;

/// <summary>
///     Valuation utils.
/// </summary>
public static class Valuation
{
    /// <summary>
    ///     Normalizes X value.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <param name="width">Surface width.</param>
    /// <param name="min">Min.</param>
    /// <param name="max">Max.</param>
    /// <returns>Returns normalized value.</returns>
    public static double NormalizePointX2D(
        double input,
        double width,
        double min,
        double max)
    {
        return (input - min) * width / (max - min);
    }

    /// <summary>
    ///     Denormalizes X value.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <param name="width">Surface width.</param>
    /// <param name="min">Min.</param>
    /// <param name="max">Max.</param>
    /// <returns>Returns denormalized value.</returns>
    public static double DenormalizePointX2D(
        double input,
        double width,
        double min,
        double max)
    {
        return input * (max - min) / width + min;
    }

    /// <summary>
    ///     Normalizes Y value.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <param name="height">Surface height.</param>
    /// <param name="min">Min.</param>
    /// <param name="max">Max.</param>
    /// <returns>Returns normalized value.</returns>
    public static double NormalizePointY2D(
        double input,
        double height,
        double min,
        double max)
    {
        return height - (input - min) * height / (max - min);
    }

    /// <summary>
    ///     Denormalizes Y value.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <param name="height">Surface height.</param>
    /// <param name="min">Min.</param>
    /// <param name="max">Max.</param>
    /// <returns>Returns denormalized value.</returns>
    public static double DenormalizePointY2D(
        double input,
        double height,
        double min,
        double max)
    {
        return (height - input) * (max - min) / height + min;
    }

    /// <summary>
    /// Normalizes point.
    /// </summary>
    /// <param name="input">Input point.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMax">Y max.</param>
    /// <returns>Returns normalized point.</returns>
    public static WavesPoint NormalizePoint(
        WavesPoint input,
        double width,
        double height,
        double xMin,
        double yMin,
        double xMax,
        double yMax)
    {
        return new WavesPoint(
            NormalizePointX2D(input.X, width, xMin, xMax),
            NormalizePointY2D(input.Y, height, yMin, yMax));
    }

    /// <summary>
    /// Normalizes points.
    /// </summary>
    /// <param name="input">Input points.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMax">Y max.</param>
    /// <returns>Returns normalized points.</returns>
    public static WavesPoint[] NormalizePoints(
        WavesPoint[] input,
        double width,
        double height,
        double xMin,
        double yMin,
        double xMax,
        double yMax)
    {
        var result = new WavesPoint[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            result[i] = NormalizePoint(input[i], width, height, xMin, xMax, yMin, yMax);
        }

        return result;
    }

    /// <summary>
    /// Normalizes point.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMax">Y max.</param>
    /// <returns>Returns normalized point.</returns>
    public static WavesPoint NormalizePoint(
        double x,
        double y,
        double width,
        double height,
        double xMin,
        double yMin,
        double xMax,
        double yMax)
    {
        return new WavesPoint(
            NormalizePointX2D(x, width, xMin, xMax),
            NormalizePointY2D(y, height, yMin, yMax));
    }

    /// <summary>
    /// Denormalizes point.
    /// </summary>
    /// <param name="input">Input point.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMax">Y max.</param>
    /// <returns>Returns denormalized point.</returns>
    public static WavesPoint DenormalizePoint(
        Point input,
        double width,
        double height,
        double xMin,
        double yMin,
        double xMax,
        double yMax)
    {
        return new WavesPoint(
            DenormalizePointX2D(input.X, width, xMin, xMax),
            DenormalizePointY2D(input.Y, height, yMin, yMax));
    }

    /// <summary>
    /// Denormalizes point.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="xMin">X min.</param>
    /// <param name="yMin">Y min.</param>
    /// <param name="xMax">X max.</param>
    /// <param name="yMax">Y max.</param>
    /// <returns>Returns denormalized point.</returns>
    public static WavesPoint DenormalizePoint(
        double x,
        double y,
        double width,
        double height,
        double xMin,
        double yMin,
        double xMax,
        double yMax)
    {
        return new WavesPoint(
            DenormalizePointX2D(x, width, xMin, xMax),
            DenormalizePointY2D(y, height, yMin, yMax));
    }
}
