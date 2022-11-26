using System;

namespace Waves.UI.Charts.Drawing.Primitives;

/// <summary>
///     Base color structure.
/// </summary>
public struct WavesColor
{
    /// <summary>
    ///     Red color.
    /// </summary>
    public static readonly WavesColor Red = FromArgb(255, 255, 0, 0);

    /// <summary>
    ///     Green color.
    /// </summary>
    public static readonly WavesColor Green = FromArgb(255, 0, 255, 0);

    /// <summary>
    ///     Blue color.
    /// </summary>
    public static readonly WavesColor Blue = FromArgb(255, 0, 0, 255);

    /// <summary>
    ///     White color.
    /// </summary>
    public static readonly WavesColor White = FromArgb(255, 255, 255, 255);

    /// <summary>
    ///     Gray color.
    /// </summary>
    public static readonly WavesColor Gray = FromArgb(255, 100, 100, 100);

    /// <summary>
    ///     Light color.
    /// </summary>
    public static readonly WavesColor LightGray = FromArgb(255, 150, 150, 150);

    /// <summary>
    ///     Dark gray color.
    /// </summary>
    public static readonly WavesColor DarkGray = FromArgb(255, 50, 50, 50);

    /// <summary>
    ///     Black color.
    /// </summary>
    public static readonly WavesColor Black = FromArgb(255, 0, 0, 0);

    /// <summary>
    ///     Transparent color.
    /// </summary>
    public static readonly WavesColor Transparent = FromArgb(0, 0, 0, 0);

    /// <summary>
    ///     Creates new instance of color structure.
    /// </summary>
    /// <param name="a">Alpha value.</param>
    /// <param name="r">Red value.</param>
    /// <param name="g">Green value.</param>
    /// <param name="b">Blue value.</param>
    public WavesColor(byte a, byte r, byte g, byte b)
    {
        A = a;
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    ///     Creates new instance of color structure.
    /// </summary>
    /// <param name="r">Red value.</param>
    /// <param name="g">Green value.</param>
    /// <param name="b">Blue value.</param>
    public WavesColor(byte r, byte g, byte b)
    {
        A = 255;
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    ///     Gets red color component value.
    /// </summary>
    public byte R { get; }

    /// <summary>
    ///     Gets green color component value.
    /// </summary>
    public byte G { get; }

    /// <summary>
    ///     Gets blue color component value.
    /// </summary>
    public byte B { get; }

    /// <summary>
    ///     Gets alpha color component value.
    /// </summary>
    public byte A { get; }

    /// <summary>
    ///     Gets normalized red color value.
    /// </summary>
    public float ScR => R / 255f;

    /// <summary>
    ///     Gets normalized green color value.
    /// </summary>
    public float ScG => G / 255f;

    /// <summary>
    ///     Gets normalized blue color value.
    /// </summary>
    public float ScB => B / 255f;

    /// <summary>
    ///     Gets normalized alpha color value.
    /// </summary>
    public float ScA => A / 255f;

    /// <summary>
    ///     Creates random color.
    /// </summary>
    /// <returns>Random color.</returns>
    public static WavesColor Random()
    {
        var random = new Random();

        var a = 255;
        var r = random.NextDouble() * 255;
        var g = random.NextDouble() * 255;
        var b = random.NextDouble() * 255;

        if (r > 255)
        {
            r = 255;
        }

        if (g > 255)
        {
            g = 255;
        }

        if (b > 255)
        {
            b = 255;
        }

        return new WavesColor(Convert.ToByte(a), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
    }

    /// <summary>
    ///     Gets color from ARGB values.
    /// </summary>
    /// <param name="a">Alpha value.</param>
    /// <param name="r">Red value.</param>
    /// <param name="g">Green value.</param>
    /// <param name="b">Blue value.</param>
    /// <returns>Color.</returns>
    public static WavesColor FromArgb(byte a, byte r, byte g, byte b)
    {
        return new WavesColor(a, r, g, b);
    }

    /// <summary>
    /// Gets color from ARGB values.
    /// </summary>
    /// <param name="a">Alpha.</param>
    /// <param name="color">Color.</param>
    /// <returns>Returns color.</returns>
    public static WavesColor WithOpacity(double a, WavesColor color)
    {
        return new WavesColor((byte)(a * color.A), color.R, color.G, color.B);
    }

    /// <summary>
    ///     Converts HEX string to Color.
    /// </summary>
    /// <param name="hexColor">HEX string.</param>
    /// <returns>Color.</returns>
    public static WavesColor FromHex(string hexColor)
    {
        try
        {
            if (string.IsNullOrEmpty(hexColor))
            {
                return default;
            }

            hexColor = hexColor.Trim("#".ToCharArray());

            if (hexColor.Length == 8)
            {
                return FromArgb(
                    Convert.ToByte(hexColor.Substring(0, 2), 16),
                    Convert.ToByte(hexColor.Substring(2, 2), 16),
                    Convert.ToByte(hexColor.Substring(4, 2), 16),
                    Convert.ToByte(hexColor.Substring(6, 2), 16));
            }

            return FromArgb(
                255,
                Convert.ToByte(hexColor.Substring(0, 2), 16),
                Convert.ToByte(hexColor.Substring(2, 2), 16),
                Convert.ToByte(hexColor.Substring(4, 2), 16));
        }
        catch (Exception)
        {
            throw new Exception("An error occured while getting HEX string.");
        }
    }

    /// <summary>
    ///     Tries parsing HEX string to color.
    /// </summary>
    /// <param name="hexColor">HEX string.</param>
    /// <param name="color">Output color.</param>
    /// <param name="isHasAlphaInSource">Whether color has alpha channel in HEX.</param>
    /// <returns>True if parsed, false if not.</returns>
    public static bool TryParseFromHex(string hexColor, out WavesColor color, out bool isHasAlphaInSource)
    {
        color = Black;
        isHasAlphaInSource = false;

        if (string.IsNullOrWhiteSpace(hexColor))
        {
            return false;
        }

        hexColor = hexColor.Trim('#');
        if (hexColor.Length > 8)
        {
            hexColor = hexColor.Substring(0, 8);
        }

        try
        {
            switch (hexColor.Length)
            {
                case 8:
                    color = FromArgb(
                        Convert.ToByte(hexColor.Substring(0, 2), 16),
                        Convert.ToByte(hexColor.Substring(2, 2), 16),
                        Convert.ToByte(hexColor.Substring(4, 2), 16),
                        Convert.ToByte(hexColor.Substring(6, 2), 16));
                    isHasAlphaInSource = true;
                    return true;

                case 6:
                    color = FromArgb(
                        255,
                        Convert.ToByte(hexColor.Substring(0, 2), 16),
                        Convert.ToByte(hexColor.Substring(2, 2), 16),
                        Convert.ToByte(hexColor.Substring(4, 2), 16));
                    return true;
            }
        }
        catch (Exception)
        {
            return false;
        }

        return false;
    }

    /// <summary>
    ///     Converts color to string.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return $"A:{A},R:{R},G:{G},B:{B}";
    }

    /// <summary>
    ///     Compares two colors.
    /// </summary>
    /// <param name="other">Other color.</param>
    /// <returns>True if equals, false if not.</returns>
    public bool Equals(WavesColor other)
    {
        return R == other.R && G == other.G && B == other.B && A == other.A;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is WavesColor color && Equals(color);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = R.GetHashCode();
            hashCode = (hashCode * 397) ^ G.GetHashCode();
            hashCode = (hashCode * 397) ^ B.GetHashCode();
            hashCode = (hashCode * 397) ^ A.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Converts to HEX string.
    /// </summary>
    /// <param name="isUseAlphaIsSet">Use alpha.</param>
    /// <param name="isHexPrefix">Use HEX prefix.</param>
    /// <returns>HEX string.</returns>
    public string ToHexString(bool isUseAlphaIsSet = true, bool isHexPrefix = true)
    {
        if (A == 255)
        {
            return isHexPrefix ? $"#{R:X2}{G:X2}{B:X2}" : $"{R:X2}{G:X2}{B:X2}";
        }

        if (isUseAlphaIsSet)
        {
            return isHexPrefix ? $"#{A:X2}{R:X2}{G:X2}{B:X2}" : $"{A:X2}{R:X2}{G:X2}{B:X2}";
        }

        return isHexPrefix ? $"#{R:X2}{G:X2}{B:X2}" : $"{R:X2}{G:X2}{B:X2}";
    }

    /// <summary>
    /// Converts to Uint.
    /// </summary>
    /// <returns>Uint.</returns>
    public uint ToUint()
    {
        return (uint)(((A << 24) | (R << 16) | (G << 8) | B) & 0xffffffffL);
    }
}
