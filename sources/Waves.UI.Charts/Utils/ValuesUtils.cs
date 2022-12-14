using System;

namespace Waves.UI.Charts.Utils;

/// <summary>
/// Data utils.
/// </summary>
public static class ValuesUtils
{
    /// <summary>
    /// Gets value from data.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <returns>Returns double value.</returns>
    public static double GetValue(object data)
    {
        if (data is null)
        {
            return double.NaN;
        }

        if (data is int i)
        {
            return i;
        }

        if (data is double d)
        {
            return d;
        }

        if (data is DateTime dt)
        {
            return dt.ToOADate();
        }

        throw new Exception("Value is not correct");
    }
}
