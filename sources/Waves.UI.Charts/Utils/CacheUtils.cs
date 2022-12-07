using System;
using System.Collections.Generic;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;

namespace Waves.UI.Charts.Utils;

/// <summary>
/// Cache utils.
/// </summary>
public static class CacheUtils
{
    /// <summary>
    /// Adds drawing object to chart collection and to cache.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="cache">Cache.</param>
    /// <param name="obj">Drawing object.</param>
    public static void AddDrawingObject(this IWavesChart chart, List<IWavesDrawingObject> cache, IWavesDrawingObject obj)
    {
        if (obj == null)
        {
            return;
        }

        chart.DrawingObjects?.Add(obj);
        cache.Add(obj);
    }

    /// <summary>
    /// Clears cache.
    /// </summary>
    /// <param name="chart">Chart.</param>
    /// <param name="cache">Cache.</param>
    public static void ClearCache(this IWavesChart chart, List<IWavesDrawingObject> cache)
    {
        foreach (var obj in cache)
        {
            chart.DrawingObjects?.Remove(obj);
        }

        cache.Clear();
    }
}
