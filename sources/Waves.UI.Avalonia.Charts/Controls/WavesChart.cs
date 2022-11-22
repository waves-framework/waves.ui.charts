using System.Drawing;
using Waves.UI.Charts.Drawing.Interfaces;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves chart.
/// </summary>
public class WavesChart : WavesSurface, IWavesChart
{
    /// <inheritdoc />
    public bool IsXAxisPrimaryTicksVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsXAxisAdditionalTicksVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsXAxisSignaturesVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsXAxisZeroLineVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsYAxisPrimaryTicksVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsYAxisAdditionalTicksVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsYAxisSignaturesVisible { get; set; }
    
    /// <inheritdoc />
    public bool IsYAxisZeroLineVisible { get; set; }
    
    /// <inheritdoc />
    public double XMin { get; set; }
    
    /// <inheritdoc />
    public double XMax { get; set; }
    
    /// <inheritdoc />
    public double YMin { get; set; }
    
    /// <inheritdoc />
    public double YMax { get; set; }
    
    /// <inheritdoc />
    public int XAxisPrimaryTicksNumber { get; set; }
    
    /// <inheritdoc />
    public int XAxisAdditionalTicksNumber { get; set; }
    
    /// <inheritdoc />
    public int YAxisPrimaryTicksNumber { get; set; }
    
    /// <inheritdoc />
    public int YAxisAdditionalTicksNumber { get; set; }
    
    /// <inheritdoc />
    public double XAxisPrimaryTickThickness { get; set; }
    
    /// <inheritdoc />
    public double XAxisAdditionalTickThickness { get; set; }
    
    /// <inheritdoc />
    public double XAxisZeroLineThickness { get; set; }
    
    /// <inheritdoc />
    public double YAxisPrimaryTickThickness { get; set; }
    
    /// <inheritdoc />
    public double YAxisAdditionalTickThickness { get; set; }
    
    /// <inheritdoc />
    public double YAxisZeroLineThickness { get; set; }
    
    /// <inheritdoc />
    public double[] XAxisPrimaryTicksDashArray { get; set; }
    
    /// <inheritdoc />
    public double[] XAxisAdditionalTicksDashArray { get; set; }
    
    /// <inheritdoc />
    public double[] XAxisZeroLineDashArray { get; set; }
    
    /// <inheritdoc />
    public double[] YAxisPrimaryTicksDashArray { get; set; }
    
    /// <inheritdoc />
    public double[] YAxisAdditionalTicksDashArray { get; set; }
    
    /// <inheritdoc />
    public double[] YAxisZeroLineDashArray { get; set; }
    
    /// <inheritdoc />
    public Color XAxisPrimaryTicksColor { get; set; }
    
    /// <inheritdoc />
    public Color XAxisAdditionalTicksColor { get; set; }
    
    /// <inheritdoc />
    public Color XAxisZeroLineColor { get; set; }
    
    /// <inheritdoc />
    public Color YAxisPrimaryTicksColor { get; set; }
    
    /// <inheritdoc />
    public Color YAxisAdditionalTicksColor { get; set; }
    
    /// <inheritdoc />
    public Color YAxisZeroLineColor { get; set; }
    
    /// <inheritdoc />
    public double FontSize { get; set; }
}
