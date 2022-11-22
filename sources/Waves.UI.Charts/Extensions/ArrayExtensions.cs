using System;
using System.Collections.Generic;

namespace Waves.UI.Charts.Extensions;

/// <summary>
/// Array extensions.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Converts float array to double.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <returns>Double.</returns>
    public static double[] ToDouble(this float[] array)
    {
        return Array.ConvertAll(array, x => (double)x);
    }

    /// <summary>
    /// Converts double array to float.
    /// </summary>
    /// <param name="array">Array.</param>
    /// <returns>Float.</returns>
    public static float[] ToFloat(this double[] array)
    {
        return Array.ConvertAll(array, x => (float)x);
    }
}
