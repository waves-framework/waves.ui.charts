using System.Collections.Generic;
using System.Drawing;
using Waves.UI.Charts.Drawing.Primitives;

namespace Waves.UI.Charts.Drawing.Interfaces;

/// <summary>
///     Interface for chart.
/// </summary>
public interface IWavesChart : IWavesSurface
{
    /// <summary>
    /// Gets or sets whether X axis primary ticks are visible.
    /// </summary>
    public bool IsXAxisPrimaryTicksVisible { get; set; }

    /// <summary>
    /// Gets or sets whether X axis additional ticks are visible.
    /// </summary>
    public bool IsXAxisAdditionalTicksVisible { get; set; }

    /// <summary>
    /// Gets or sets whether X axis signatures are visible.
    /// </summary>
    public bool IsXAxisSignaturesVisible { get; set; }

    /// <summary>
    /// Gets or sets whether X axis zero line is visible.
    /// </summary>
    public bool IsXAxisZeroLineVisible { get; set; }

    /// <summary>
    /// Gets or sets whether Y axis primary ticks are visible.
    /// </summary>
    public bool IsYAxisPrimaryTicksVisible { get; set; }

    /// <summary>
    /// Gets or sets whether Y axis additional ticks are visible.
    /// </summary>
    public bool IsYAxisAdditionalTicksVisible { get; set; }

    /// <summary>
    /// Gets or sets whether Y axis signatures are visible.
    /// </summary>
    public bool IsYAxisSignaturesVisible { get; set; }

    /// <summary>
    /// Gets or sets whether Y axis zero line is visible.
    /// </summary>
    public bool IsYAxisZeroLineVisible { get; set; }

    /// <summary>
    /// Gets or sets X min.
    /// </summary>
    public double XMin { get; set; }

    /// <summary>
    /// Gets or sets X max.
    /// </summary>
    public double XMax { get; set; }

    /// <summary>
    /// Gets or sets Y min.
    /// </summary>
    public double YMin { get; set; }

    /// <summary>
    /// Gets or sets Y max.
    /// </summary>
    public double YMax { get; set; }

    /// <summary>
    /// Gets or sets X axis primary ticks number.
    /// </summary>
    public int XAxisPrimaryTicksNumber { get; set; }

    /// <summary>
    /// Gets or sets X axis additional ticks number.
    /// </summary>
    public int XAxisAdditionalTicksNumber { get; set; }

    /// <summary>
    /// Gets or sets Y axis primary ticks number.
    /// </summary>
    public int YAxisPrimaryTicksNumber { get; set; }

    /// <summary>
    /// Gets or sets Y axis additional ticks number.
    /// </summary>
    public int YAxisAdditionalTicksNumber { get; set; }

    /// <summary>
    /// Gets or sets X axis primary ticks thickness.
    /// </summary>
    public double XAxisPrimaryTickThickness { get; set; }

    /// <summary>
    /// Gets or sets X axis additional ticks thickness.
    /// </summary>
    public double XAxisAdditionalTickThickness { get; set; }

    /// <summary>
    /// Gets or sets X axis zero line thickness.
    /// </summary>
    public double XAxisZeroLineThickness { get; set; }

    /// <summary>
    /// Gets or sets Y axis primary ticks thickness.
    /// </summary>
    public double YAxisPrimaryTickThickness { get; set; }

    /// <summary>
    /// Gets or sets Y axis additional ticks thickness.
    /// </summary>
    public double YAxisAdditionalTickThickness { get; set; }

    /// <summary>
    /// Gets or sets Y axis zero line thickness.
    /// </summary>
    public double YAxisZeroLineThickness { get; set; }

    /// <summary>
    /// Gets or sets X axis primary ticks dash array.
    /// </summary>
    public double[] XAxisPrimaryTicksDashArray { get; set; }

    /// <summary>
    /// Gets or sets X axis additional ticks dash array.
    /// </summary>
    public double[] XAxisAdditionalTicksDashArray { get; set; }

    /// <summary>
    /// Gets or sets X axis zero line dash array.
    /// </summary>
    public double[] XAxisZeroLineDashArray { get; set; }

    /// <summary>
    /// Gets or sets Y axis primary ticks dash array.
    /// </summary>
    public double[] YAxisPrimaryTicksDashArray { get; set; }

    /// <summary>
    /// Gets or sets Y axis additional ticks dash array.
    /// </summary>
    public double[] YAxisAdditionalTicksDashArray { get; set; }

    /// <summary>
    /// Gets or sets Y axis zero line dash array.
    /// </summary>
    public double[] YAxisZeroLineDashArray { get; set; }

    /// <summary>
    /// Gets or sets X axis primary ticks color.
    /// </summary>
    public Color XAxisPrimaryTicksColor { get; set; }

    /// <summary>
    /// Gets or sets X axis additional ticks color.
    /// </summary>
    public Color XAxisAdditionalTicksColor { get; set; }

    /// <summary>
    /// Gets or sets X axis zero line color.
    /// </summary>
    public Color XAxisZeroLineColor { get; set; }

    /// <summary>
    /// Gets or sets Y axis primary ticks color.
    /// </summary>
    public Color YAxisPrimaryTicksColor { get; set; }

    /// <summary>
    /// Gets or sets Y axis additional ticks color.
    /// </summary>
    public Color YAxisAdditionalTicksColor { get; set; }

    /// <summary>
    /// Gets or sets Y axis zero line color.
    /// </summary>
    public Color YAxisZeroLineColor { get; set; }

    /// <summary>
    /// Gets or sets font size.
    /// </summary>
    public double FontSize { get; set; }

    /// <summary>
    /// Gets or sets Axis ticks collection.
    /// </summary>
    ICollection<WavesAxisTick> AxisTicks { get; set; }

    /// <summary>
    /// Gets or sets whether chart has default ticks or not.
    /// </summary>
    bool HasDefaultTicks { get; }
}
