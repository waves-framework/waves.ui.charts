using System;
using System.Drawing;
using System.Threading.Tasks;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Utils;

/// <summary>
/// Resampling utils.
/// </summary>
public class Resampling
{
    /// <summary>
    /// Largest Triangle Three Buckets Decimation.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <param name="threshold">Threshold.</param>
    /// <returns>Returns decimated point array.</returns>
    public static WavesPoint[] LargestTriangleThreeBucketsDecimation(WavesPoint[] input, int threshold)
    {
        if (threshold == 0)
        {
            return Array.Empty<WavesPoint>();
        }

        var output = new WavesPoint[threshold];

        var dataLength = input.Length;

        if (threshold >= dataLength || threshold <= 0)
        {
            return input;
        }

        var every = (double)(dataLength - 2) / (threshold - 2);

        var index = 0;
        var maxAreaPoint = new WavesPoint(0, 0);
        var nextIndex = 0;

        output[0] = input[index];

        for (var i = 0; i < threshold - 2; i++)
        {
            var avgX = 0.0d;
            var avgY = 0.0d;
            var avgRangeStart = (int)(Math.Floor((i + 1) * every) + 1);
            var avgRangeEnd = (int)(Math.Floor((i + 2) * every) + 1);
            avgRangeEnd = avgRangeEnd < dataLength ? avgRangeEnd : dataLength;

            var avgRangeLength = avgRangeEnd - avgRangeStart;

            for (; avgRangeStart < avgRangeEnd; avgRangeStart++)
            {
                avgX += input[avgRangeStart].X;
                avgY += input[avgRangeStart].Y;
            }

            avgX /= avgRangeLength;
            avgY /= avgRangeLength;

            var rangeOffs = (int)(Math.Floor((i + 0) * every) + 1);
            var rangeTo = (int)(Math.Floor((i + 1) * every) + 1);

            var pointAx = input[index].X;
            var pointAy = input[index].Y;
            var maxArea = -1.0d;

            var diffAxAvgX = pointAx - avgX;
            var diffAvgYAy = avgY - pointAy;

            for (; rangeOffs < rangeTo; rangeOffs++)
            {
                var area = Math.Abs(diffAxAvgX * (input[rangeOffs].Y - pointAy)
                                    - (pointAx - input[rangeOffs].X) * diffAvgYAy) * 0.5;

                if (!(area > maxArea))
                {
                    continue;
                }

                maxArea = area;
                maxAreaPoint = input[rangeOffs];
                nextIndex = rangeOffs;
            }

            output[i + 1] = maxAreaPoint;
            index = nextIndex;
        }

        output[threshold - 1] = input[dataLength - 1];

        return output;
    }

    /// <summary>
    ///     Spline interpolation.
    /// </summary>
    /// <param name="input">Input data.</param>
    /// <param name="threshold">Number of points.</param>
    /// <returns>Interpolated data.</returns>
    public static WavesPoint[] SplineInterpolation(WavesPoint[] input, int threshold)
    {
        // TODO:
        return input;
    }
}
