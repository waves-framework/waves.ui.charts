namespace Waves.UI.Charts.Drawing.Primitives.Data;

/// <summary>
///     Size base structure.
/// </summary>
public readonly struct WavesSize
{
    /// <summary>
    ///     Creates new instance of size (square).
    /// </summary>
    /// <param name="length">Length.</param>
    public WavesSize(double length)
    {
        Width = length;
        Height = length;
    }

    /// <summary>
    ///     Creates new instance of size (rectangle)
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public WavesSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    ///     Gets width.
    /// </summary>
    public double Width { get; }

    /// <summary>
    ///     Gets height.
    /// </summary>
    public double Height { get; }

    /// <summary>
    ///     Gets space.
    /// </summary>
    public double Space => Width * Height;

    /// <summary>
    ///     Gets aspect ration.
    /// </summary>
    public double Aspect => Width / Height;

    /// <summary>
    ///     Gets whether two size structures are equals.
    /// </summary>
    /// <param name="other">The second structure.</param>
    /// <returns>Return true if equals, false if not.</returns>
    public bool Equals(WavesSize other)
    {
        return Width.Equals(other.Width) && Height.Equals(other.Height);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is WavesSize size && Equals(size);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
        }
    }
}
