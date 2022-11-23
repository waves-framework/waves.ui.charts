using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Utils;
using Color = System.Drawing.Color;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves chart.
/// </summary>
public class WavesChart : WavesSurface, IWavesChart
{
    /// <summary>
    /// Defines <see cref="IsXAxisPrimaryTicksVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsXAxisPrimaryTicksVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsXAxisPrimaryTicksVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsXAxisAdditionalTicksVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsXAxisAdditionalTicksVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsXAxisAdditionalTicksVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsXAxisSignaturesVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsXAxisSignaturesVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsXAxisSignaturesVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsXAxisZeroLineVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsXAxisZeroLineVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsXAxisZeroLineVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsYAxisPrimaryTicksVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsYAxisPrimaryTicksVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsYAxisPrimaryTicksVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsYAxisAdditionalTicksVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsYAxisAdditionalTicksVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsYAxisAdditionalTicksVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsYAxisSignaturesVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsYAxisSignaturesVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsYAxisSignaturesVisible),
            true);

    /// <summary>
    /// Defines <see cref="IsYAxisZeroLineVisible"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsYAxisZeroLineVisibleProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsYAxisZeroLineVisible),
            true);

    /// <summary>
    /// Defines <see cref="XMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> XMinProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(XMin),
            0);

    /// <summary>
    /// Defines <see cref="XMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> XMaxProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(XMax),
            1);

    /// <summary>
    /// Defines <see cref="YMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMinProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(YMin),
            -1);

    /// <summary>
    /// Defines <see cref="YMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMaxProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(YMax),
            1);

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> XAxisPrimaryTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(XAxisPrimaryTicksNumber),
            4);

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> XAxisAdditionalTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(XAxisAdditionalTicksNumber),
            4);

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> YAxisPrimaryTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(YAxisPrimaryTicksNumber),
            4);

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> YAxisAdditionalTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(YAxisAdditionalTicksNumber),
            4);

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTickThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> XAxisPrimaryTickThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(XAxisPrimaryTickThickness),
            1);

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTickThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> XAxisAdditionalTickThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(XAxisAdditionalTickThickness),
            1);

    /// <summary>
    /// Defines <see cref="XAxisZeroLineThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> XAxisZeroLineThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(XAxisZeroLineThickness),
            1);

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTickThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YAxisPrimaryTickThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(YAxisPrimaryTickThickness),
            1);

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTickThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YAxisAdditionalTickThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(YAxisAdditionalTickThickness),
            1);

    /// <summary>
    /// Defines <see cref="YAxisZeroLineThickness"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YAxisZeroLineThicknessProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(YAxisZeroLineThickness),
            1);

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> XAxisPrimaryTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(XAxisPrimaryTicksDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> XAxisAdditionalTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(XAxisAdditionalTicksDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="XAxisZeroLineDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> XAxisZeroLineDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(XAxisZeroLineDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisPrimaryTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisPrimaryTicksDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisAdditionalTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisAdditionalTicksDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="YAxisZeroLineDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisZeroLineDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisZeroLineDashArray),
            new double[] { 1, 1, 1, 1 });

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> XAxisPrimaryTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(XAxisPrimaryTicksColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> XAxisAdditionalTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(XAxisAdditionalTicksColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="XAxisZeroLineColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> XAxisZeroLineColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(XAxisZeroLineColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> YAxisPrimaryTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(YAxisPrimaryTicksColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> YAxisAdditionalTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(YAxisAdditionalTicksColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="YAxisZeroLineColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<Color> YAxisZeroLineColorProperty =
        AvaloniaProperty.Register<WavesChart, Color>(
            nameof(YAxisZeroLineColor),
            Color.Gray);

    /// <summary>
    /// Defines <see cref="AxisTicks"/> styled property.
    /// </summary>
    public static readonly StyledProperty<ICollection<WavesAxisTick>> AxisTicksProperty =
        AvaloniaProperty.Register<WavesChart, ICollection<WavesAxisTick>>(
            nameof(AxisTicks),
            new List<WavesAxisTick>());

    /// <summary>
    /// Defines <see cref="HasDefaultTicks"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> HasDefaultTicksProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(HasDefaultTicks),
            true);

    private readonly List<IWavesDrawingObject> _axisTicksDrawingObjects = new ();
    private List<WavesAxisTick> _axisTicks;

    /// <summary>
    /// Creates new instance of <see cref="WavesChart"/>.
    /// </summary>
    public WavesChart()
        : base()
    {
        AffectsRender<WavesChart>(IsXAxisPrimaryTicksVisibleProperty);
        AffectsRender<WavesChart>(IsXAxisAdditionalTicksVisibleProperty);
        AffectsRender<WavesChart>(IsXAxisSignaturesVisibleProperty);
        AffectsRender<WavesChart>(IsXAxisZeroLineVisibleProperty);
        AffectsRender<WavesChart>(IsYAxisPrimaryTicksVisibleProperty);
        AffectsRender<WavesChart>(IsYAxisPrimaryTicksVisibleProperty);
        AffectsRender<WavesChart>(IsYAxisPrimaryTicksVisibleProperty);
        AffectsRender<WavesChart>(IsYAxisPrimaryTicksVisibleProperty);
        AffectsRender<WavesChart>(XMinProperty);
        AffectsRender<WavesChart>(XMaxProperty);
        AffectsRender<WavesChart>(YMinProperty);
        AffectsRender<WavesChart>(YMaxProperty);
        AffectsRender<WavesChart>(XAxisPrimaryTicksNumberProperty);
        AffectsRender<WavesChart>(XAxisAdditionalTicksNumberProperty);
        AffectsRender<WavesChart>(YAxisPrimaryTicksNumberProperty);
        AffectsRender<WavesChart>(YAxisAdditionalTicksNumberProperty);
        AffectsRender<WavesChart>(XAxisPrimaryTickThicknessProperty);
        AffectsRender<WavesChart>(XAxisAdditionalTickThicknessProperty);
        AffectsRender<WavesChart>(XAxisZeroLineThicknessProperty);
        AffectsRender<WavesChart>(YAxisPrimaryTickThicknessProperty);
        AffectsRender<WavesChart>(YAxisAdditionalTickThicknessProperty);
        AffectsRender<WavesChart>(YAxisZeroLineThicknessProperty);
        AffectsRender<WavesChart>(XAxisPrimaryTicksDashArrayProperty);
        AffectsRender<WavesChart>(XAxisAdditionalTicksDashArrayProperty);
        AffectsRender<WavesChart>(XAxisZeroLineDashArrayProperty);
        AffectsRender<WavesChart>(YAxisPrimaryTicksDashArrayProperty);
        AffectsRender<WavesChart>(YAxisAdditionalTicksDashArrayProperty);
        AffectsRender<WavesChart>(YAxisZeroLineDashArrayProperty);
        AffectsRender<WavesChart>(XAxisPrimaryTicksColorProperty);
        AffectsRender<WavesChart>(XAxisAdditionalTicksColorProperty);
        AffectsRender<WavesChart>(XAxisZeroLineColorProperty);
        AffectsRender<WavesChart>(YAxisPrimaryTicksColorProperty);
        AffectsRender<WavesChart>(YAxisAdditionalTicksColorProperty);
        AffectsRender<WavesChart>(YAxisZeroLineColorProperty);
    }

    /// <inheritdoc />
    public bool IsXAxisPrimaryTicksVisible
    {
        get => GetValue(IsXAxisPrimaryTicksVisibleProperty);
        set => SetValue(IsXAxisPrimaryTicksVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsXAxisAdditionalTicksVisible
    {
        get => GetValue(IsXAxisAdditionalTicksVisibleProperty);
        set => SetValue(IsXAxisAdditionalTicksVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsXAxisSignaturesVisible
    {
        get => GetValue(IsXAxisSignaturesVisibleProperty);
        set => SetValue(IsXAxisSignaturesVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsXAxisZeroLineVisible
    {
        get => GetValue(IsXAxisZeroLineVisibleProperty);
        set => SetValue(IsXAxisZeroLineVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsYAxisPrimaryTicksVisible
    {
        get => GetValue(IsYAxisPrimaryTicksVisibleProperty);
        set => SetValue(IsYAxisPrimaryTicksVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsYAxisAdditionalTicksVisible
    {
        get => GetValue(IsYAxisAdditionalTicksVisibleProperty);
        set => SetValue(IsYAxisAdditionalTicksVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsYAxisSignaturesVisible
    {
        get => GetValue(IsYAxisSignaturesVisibleProperty);
        set => SetValue(IsYAxisSignaturesVisibleProperty, value);
    }

    /// <inheritdoc />
    public bool IsYAxisZeroLineVisible
    {
        get => GetValue(IsYAxisZeroLineVisibleProperty);
        set => SetValue(IsYAxisZeroLineVisibleProperty, value);
    }

    /// <inheritdoc />
    public double XMin
    {
        get => GetValue(XMinProperty);
        set => SetValue(XMinProperty, value);
    }

    /// <inheritdoc />
    public double XMax
    {
        get => GetValue(XMaxProperty);
        set => SetValue(XMaxProperty, value);
    }

    /// <inheritdoc />
    public double YMin
    {
        get => GetValue(YMinProperty);
        set => SetValue(YMinProperty, value);
    }

    /// <inheritdoc />
    public double YMax
    {
        get => GetValue(YMaxProperty);
        set => SetValue(YMaxProperty, value);
    }

    /// <inheritdoc />
    public int XAxisPrimaryTicksNumber
    {
        get => GetValue(XAxisPrimaryTicksNumberProperty);
        set => SetValue(XAxisPrimaryTicksNumberProperty, value);
    }

    /// <inheritdoc />
    public int XAxisAdditionalTicksNumber
    {
        get => GetValue(XAxisAdditionalTicksNumberProperty);
        set => SetValue(XAxisAdditionalTicksNumberProperty, value);
    }

    /// <inheritdoc />
    public int YAxisPrimaryTicksNumber
    {
        get => GetValue(YAxisPrimaryTicksNumberProperty);
        set => SetValue(YAxisPrimaryTicksNumberProperty, value);
    }

    /// <inheritdoc />
    public int YAxisAdditionalTicksNumber
    {
        get => GetValue(YAxisAdditionalTicksNumberProperty);
        set => SetValue(YAxisAdditionalTicksNumberProperty, value);
    }

    /// <inheritdoc />
    public double XAxisPrimaryTickThickness
    {
        get => GetValue(XAxisPrimaryTickThicknessProperty);
        set => SetValue(XAxisPrimaryTickThicknessProperty, value);
    }

    /// <inheritdoc />
    public double XAxisAdditionalTickThickness
    {
        get => GetValue(XAxisAdditionalTickThicknessProperty);
        set => SetValue(XAxisAdditionalTickThicknessProperty, value);
    }

    /// <inheritdoc />
    public double XAxisZeroLineThickness
    {
        get => GetValue(XAxisZeroLineThicknessProperty);
        set => SetValue(XAxisZeroLineThicknessProperty, value);
    }

    /// <inheritdoc />
    public double YAxisPrimaryTickThickness
    {
        get => GetValue(YAxisPrimaryTickThicknessProperty);
        set => SetValue(YAxisPrimaryTickThicknessProperty, value);
    }

    /// <inheritdoc />
    public double YAxisAdditionalTickThickness
    {
        get => GetValue(YAxisAdditionalTickThicknessProperty);
        set => SetValue(YAxisAdditionalTickThicknessProperty, value);
    }

    /// <inheritdoc />
    public double YAxisZeroLineThickness
    {
        get => GetValue(YAxisZeroLineThicknessProperty);
        set => SetValue(YAxisZeroLineThicknessProperty, value);
    }

    /// <inheritdoc />
    public double[] XAxisPrimaryTicksDashArray
    {
        get => GetValue(XAxisPrimaryTicksDashArrayProperty);
        set => SetValue(XAxisPrimaryTicksDashArrayProperty, value);
    }

    /// <inheritdoc />
    public double[] XAxisAdditionalTicksDashArray
    {
        get => GetValue(XAxisAdditionalTicksDashArrayProperty);
        set => SetValue(XAxisAdditionalTicksDashArrayProperty, value);
    }

    /// <inheritdoc />
    public double[] XAxisZeroLineDashArray
    {
        get => GetValue(XAxisZeroLineDashArrayProperty);
        set => SetValue(XAxisZeroLineDashArrayProperty, value);
    }

    /// <inheritdoc />
    public double[] YAxisPrimaryTicksDashArray
    {
        get => GetValue(YAxisPrimaryTicksDashArrayProperty);
        set => SetValue(YAxisPrimaryTicksDashArrayProperty, value);
    }

    /// <inheritdoc />
    public double[] YAxisAdditionalTicksDashArray
    {
        get => GetValue(YAxisAdditionalTicksDashArrayProperty);
        set => SetValue(YAxisAdditionalTicksDashArrayProperty, value);
    }

    /// <inheritdoc />
    public double[] YAxisZeroLineDashArray
    {
        get => GetValue(YAxisZeroLineDashArrayProperty);
        set => SetValue(YAxisZeroLineDashArrayProperty, value);
    }

    /// <inheritdoc />
    public Color XAxisPrimaryTicksColor
    {
        get => GetValue(XAxisPrimaryTicksColorProperty);
        set => SetValue(XAxisPrimaryTicksColorProperty, value);
    }

    /// <inheritdoc />
    public Color XAxisAdditionalTicksColor
    {
        get => GetValue(XAxisAdditionalTicksColorProperty);
        set => SetValue(XAxisAdditionalTicksColorProperty, value);
    }

    /// <inheritdoc />
    public Color XAxisZeroLineColor
    {
        get => GetValue(XAxisZeroLineColorProperty);
        set => SetValue(XAxisZeroLineColorProperty, value);
    }

    /// <inheritdoc />
    public Color YAxisPrimaryTicksColor
    {
        get => GetValue(YAxisPrimaryTicksColorProperty);
        set => SetValue(YAxisPrimaryTicksColorProperty, value);
    }

    /// <inheritdoc />
    public Color YAxisAdditionalTicksColor
    {
        get => GetValue(YAxisAdditionalTicksColorProperty);
        set => SetValue(YAxisAdditionalTicksColorProperty, value);
    }

    /// <inheritdoc />
    public Color YAxisZeroLineColor
    {
        get => GetValue(YAxisZeroLineColorProperty);
        set => SetValue(YAxisZeroLineColorProperty, value);
    }

    /// <inheritdoc />
    public ICollection<WavesAxisTick> AxisTicks
    {
        get => GetValue(AxisTicksProperty);
        set
        {
            _axisTicks = value.ToList();
            SetValue(AxisTicksProperty, value);
        }
    }

    /// <inheritdoc />
    public bool HasDefaultTicks
    {
        get => GetValue(HasDefaultTicksProperty);
        private set => SetValue(HasDefaultTicksProperty, value);
    }

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        if (HasDefaultTicks)
        {
            HasDefaultTicks = this.GenerateDefaultTicks();
        }

        this.GenerateAxisTicksDrawingObjects(_axisTicksDrawingObjects, Bounds.Width, Bounds.Height);
        //// GenerateAxisTicksSignaturesDrawingObjects();

        base.Refresh(context);
    }
}
