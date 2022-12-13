using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;

namespace Waves.UI.Charts.Utils;

/// <summary>
/// Resampling utils.
/// </summary>
public class ResamplingUtils
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
        var inputPointCount = input.Length;
        var inputDistances = new double[inputPointCount];
        for (var i = 1; i < inputPointCount; i++)
        {
            inputDistances[i] = inputDistances[i - 1] + input[i].X - input[i - 1].X;
        }

        var meanDistance = inputDistances.Last() / (threshold - 1);
        var evenDistances = Enumerable.Range(0, threshold).Select(x => x * meanDistance).ToArray();
        var xsOut = Interpolate(inputDistances, input.Select(x => x.X).ToArray(), evenDistances);
        var ysOut = Interpolate(inputDistances, input.Select(x => x.Y).ToArray(), evenDistances);
        var length = xsOut.Length;
        var result = new WavesPoint[length];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = new WavesPoint(xsOut[i], ysOut[i]);
        }

        return result;
    }

    /// <summary>
    ///     Interpolate.
    /// </summary>
    /// <param name="xOrig">X.</param>
    /// <param name="yOrig">Y.</param>
    /// <param name="xInterp">X interpolated.</param>
    /// <returns>Interpolated array.</returns>
    private static double[] Interpolate(double[] xOrig, double[] yOrig, double[] xInterp)
    {
        if (xOrig.Length < 2 && yOrig.Length < 2)
        {
            return Array.Empty<double>();
        }

        var (a, b) = FitMatrix(xOrig, yOrig);

        var yInterp = new double[xInterp.Length];
        for (var i = 0; i < yInterp.Length; i++)
        {
            int j;
            for (j = 0; j < xOrig.Length - 2; j++)
            {
                if (xInterp[i] <= xOrig[j + 1])
                {
                    break;
                }
            }

            var dx = xOrig[j + 1] - xOrig[j];
            var t = (xInterp[i] - xOrig[j]) / dx;
            var y = (1 - t) * yOrig[j] + t * yOrig[j + 1] +
                    t * (1 - t) * (a[j] * (1 - t) + b[j] * t);
            yInterp[i] = y;
        }

        return yInterp;
    }

    private static (double[] a, double[] b) FitMatrix(double[] x, double[] y)
    {
        var n = x.Length;
        var a = new double[n - 1];
        var b = new double[n - 1];
        var r = new double[n];
        var a1 = new double[n];
        var b1 = new double[n];
        var c1 = new double[n];

        double dx1, dx2, dy1, dy2;

        dx1 = x[1] - x[0];
        c1[0] = 1.0f / dx1;
        b1[0] = 2.0f * c1[0];
        r[0] = 3 * (y[1] - y[0]) / (dx1 * dx1);

        for (var i = 1; i < n - 1; i++)
        {
            dx1 = x[i] - x[i - 1];
            dx2 = x[i + 1] - x[i];
            a1[i] = 1.0f / dx1;
            c1[i] = 1.0f / dx2;
            b1[i] = 2.0f * (a1[i] + c1[i]);
            dy1 = y[i] - y[i - 1];
            dy2 = y[i + 1] - y[i];
            r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
        }

        dx1 = x[n - 1] - x[n - 2];
        dy1 = y[n - 1] - y[n - 2];
        a1[n - 1] = 1.0f / dx1;
        b1[n - 1] = 2.0f * a1[n - 1];
        r[n - 1] = 3 * (dy1 / (dx1 * dx1));

        var cPrime = new double[n];
        cPrime[0] = c1[0] / b1[0];
        for (var i = 1; i < n; i++)
        {
            cPrime[i] = c1[i] / (b1[i] - cPrime[i - 1] * a1[i]);
        }

        var dPrime = new double[n];
        dPrime[0] = r[0] / b1[0];
        for (var i = 1; i < n; i++)
        {
            dPrime[i] = (r[i] - dPrime[i - 1] * a1[i]) / (b1[i] - cPrime[i - 1] * a1[i]);
        }

        var k = new double[n];
        k[n - 1] = dPrime[n - 1];
        for (var i = n - 2; i >= 0; i--)
        {
            k[i] = dPrime[i] - cPrime[i] * k[i + 1];
        }

        for (var i = 1; i < n; i++)
        {
            dx1 = x[i] - x[i - 1];
            dy1 = y[i] - y[i - 1];
            a[i - 1] = k[i - 1] * dx1 - dy1;
            b[i - 1] = -k[i] * dx1 + dy1;
        }

        return (a, b);
    }
}
