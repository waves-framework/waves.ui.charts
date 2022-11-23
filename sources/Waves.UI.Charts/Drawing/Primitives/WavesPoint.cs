using System;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Waves point.
/// </summary>
public struct WavesPoint
{
    /// <summary>
    ///     Creates new instance of <see cref="WavesPoint"/>.
    /// </summary>
    /// <param name="xValue">The x value of the vector. </param>
    /// <param name="yValue">The y value of the vector. </param>
    public WavesPoint(double xValue, double yValue)
        : this()
    {
        X = xValue;
        Y = yValue;
    }

    /// <summary>
    ///     X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    ///     Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    ///     The length of the vector.
    /// </summary>
    public double Length => Math.Sqrt(SquaredLength);

    /// <summary>
    ///     The squared length of the vector. Useful for optimi.
    /// </summary>
    public double SquaredLength => X * X + Y * Y;

    /// <summary>
    ///     The absolute angle of the vector.
    /// </summary>
    public double Angle => Math.Atan2(Y, X);

    /// <summary>
    ///     Overrides the Equals method to provice better equality for vectors.
    /// </summary>
    /// <param name="obj">The object to test equality against.</param>
    /// <returns>Whether the objects are equal. </returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        var other = (WavesPoint)obj;
        return Math.Abs(X - other.X) < double.Epsilon && Math.Abs(Y - other.Y) < double.Epsilon;
    }

    /// <summary>
    ///     Overrides the hashcode.
    /// </summary>
    /// <returns>The hashcode for the vector.</returns>
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    /// <summary>
    ///     ToString method overriden for easy printing/debugging.
    /// </summary>
    /// <returns>The string representation of the vector.</returns>
    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }
}
