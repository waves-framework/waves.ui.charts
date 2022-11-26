using Avalonia;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves chart.
/// </summary>
public class WavesChart : WavesSurface, IWavesChart, IStyleable
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
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(XMin),
            0,
            true);

    /// <summary>
    /// Defines <see cref="XMax"/> styled property.
    /// </summary>
    public static readonly AttachedProperty<double> XMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(XMax),
            1,
            true);

    /// <summary>
    /// Defines <see cref="YMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(YMin),
            -1,
            true);

    /// <summary>
    /// Defines <see cref="YMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(YMax),
            1,
            true);

    /// <summary>
    /// Defines <see cref="CurrentXMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentXMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(CurrentXMin),
            0,
            true);

    /// <summary>
    /// Defines <see cref="CurrentXMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentXMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(CurrentXMax),
            1,
            true);

    /// <summary>
    /// Defines <see cref="CurrentYMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentYMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(CurrentYMin),
            -1,
            true);

    /// <summary>
    /// Defines <see cref="CurrentYMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentYMaxProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(CurrentYMax),
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
            8);

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> YAxisPrimaryTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(YAxisPrimaryTicksNumber),
            2);

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
            new double[] { 4, 4, 4, 4 });

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> XAxisAdditionalTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(XAxisAdditionalTicksDashArray),
            new double[] { 4, 4, 4, 4 });

    /// <summary>
    /// Defines <see cref="XAxisZeroLineDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> XAxisZeroLineDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(XAxisZeroLineDashArray),
            new double[] { 0, 0, 0, 0 });

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisPrimaryTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisPrimaryTicksDashArray),
            new double[] { 4, 4, 4, 4 });

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTicksDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisAdditionalTicksDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisAdditionalTicksDashArray),
            new double[] { 4, 4, 4, 4 });

    /// <summary>
    /// Defines <see cref="YAxisZeroLineDashArray"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double[]> YAxisZeroLineDashArrayProperty =
        AvaloniaProperty.Register<WavesChart, double[]>(
            nameof(YAxisZeroLineDashArray),
            new double[] { 0, 0, 0, 0 });

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> XAxisPrimaryTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(XAxisPrimaryTicksColor),
            WavesColor.Gray);

    /// <summary>
    /// Defines <see cref="XAxisAdditionalTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> XAxisAdditionalTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(XAxisAdditionalTicksColor),
            WavesColor.DarkGray);

    /// <summary>
    /// Defines <see cref="XAxisZeroLineColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> XAxisZeroLineColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(XAxisZeroLineColor),
            WavesColor.LightGray);

    /// <summary>
    /// Defines <see cref="YAxisPrimaryTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> YAxisPrimaryTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(YAxisPrimaryTicksColor),
            WavesColor.Gray);

    /// <summary>
    /// Defines <see cref="YAxisAdditionalTicksColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> YAxisAdditionalTicksColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(YAxisAdditionalTicksColor),
            WavesColor.DarkGray);

    /// <summary>
    /// Defines <see cref="YAxisZeroLineColor"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesColor> YAxisZeroLineColorProperty =
        AvaloniaProperty.Register<WavesChart, WavesColor>(
            nameof(YAxisZeroLineColor),
            WavesColor.LightGray);

    /// <summary>
    /// Defines <see cref="HasDefaultTicks"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> HasDefaultTicksProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(HasDefaultTicks),
            true);

    /// <summary>
    /// Defines <see cref="HorizontalSignatureAlignment"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesAxisHorizontalSignatureAlignment> HorizontalSignatureAlignmentProperty =
        AvaloniaProperty.Register<WavesChart, WavesAxisHorizontalSignatureAlignment>(
            nameof(HorizontalSignatureAlignment),
            WavesAxisHorizontalSignatureAlignment.Bottom);

    /// <summary>
    /// Defines <see cref="VerticalSignatureAlignment"/> styled property.
    /// </summary>
    public static readonly StyledProperty<WavesAxisVerticalSignatureAlignment> VerticalSignatureAlignmentProperty =
        AvaloniaProperty.Register<WavesChart, WavesAxisVerticalSignatureAlignment>(
            nameof(VerticalSignatureAlignment),
            WavesAxisVerticalSignatureAlignment.Left);

    /// <summary>
    /// Defines <see cref="IsCtrlPressed"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsCtrlPressedProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsCtrlPressed),
            false);

    /// <summary>
    /// Defines <see cref="IsShiftPressed"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsShiftPressedProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsShiftPressed),
            false);

    /// <summary>
    /// Defines <see cref="IsMouseOver"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsMouseOverProperty =
        AvaloniaProperty.Register<WavesChart, bool>(
            nameof(IsMouseOver),
            false);

    private readonly List<WavesAxisTick> _ticks = new ();
    private readonly List<IWavesDrawingObject> _ticksCache = new ();
    private readonly List<IWavesDrawingObject> _signaturesCache = new ();

    private IWavesDrawingObject _background;
    private double _xMin = 0;
    private double _xMax = 1;
    private double _yMin = -1;
    private double _yMax = 1;

    /// <summary>
    /// Creates new instance of <see cref="WavesChart"/>.
    /// </summary>
    public WavesChart()
        : base()
    {
        var styles = (Styles)AvaloniaXamlLoader.Load(new Uri($"avares://Waves.UI.Avalonia.Charts/Styles/WavesChart.axaml"));
        Styles.AddRange(styles);

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
        AffectsRender<WavesChart>(HorizontalSignatureAlignmentProperty);
        AffectsRender<WavesChart>(VerticalSignatureAlignmentProperty);

        XMinProperty.Changed.Subscribe(OnXMinChanged);
        XMaxProperty.Changed.Subscribe(OnXMaxChanged);
        YMinProperty.Changed.Subscribe(OnYMinChanged);
        YMaxProperty.Changed.Subscribe(OnYMaxChanged);
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
    public double CurrentXMin
    {
        get => GetValue(CurrentXMinProperty);
        set => SetValue(CurrentXMinProperty, value);
    }

    /// <inheritdoc />
    public double CurrentXMax
    {
        get => GetValue(CurrentXMaxProperty);
        set => SetValue(CurrentXMaxProperty, value);
    }

    /// <inheritdoc />
    public double CurrentYMin
    {
        get => GetValue(CurrentYMinProperty);
        set => SetValue(CurrentYMinProperty, value);
    }

    /// <inheritdoc />
    public double CurrentYMax
    {
        get => GetValue(CurrentYMaxProperty);
        set => SetValue(CurrentYMaxProperty, value);
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
    public WavesColor XAxisPrimaryTicksColor
    {
        get => GetValue(XAxisPrimaryTicksColorProperty);
        set => SetValue(XAxisPrimaryTicksColorProperty, value);
    }

    /// <inheritdoc />
    public WavesColor XAxisAdditionalTicksColor
    {
        get => GetValue(XAxisAdditionalTicksColorProperty);
        set => SetValue(XAxisAdditionalTicksColorProperty, value);
    }

    /// <inheritdoc />
    public WavesColor XAxisZeroLineColor
    {
        get => GetValue(XAxisZeroLineColorProperty);
        set => SetValue(XAxisZeroLineColorProperty, value);
    }

    /// <inheritdoc />
    public WavesColor YAxisPrimaryTicksColor
    {
        get => GetValue(YAxisPrimaryTicksColorProperty);
        set => SetValue(YAxisPrimaryTicksColorProperty, value);
    }

    /// <inheritdoc />
    public WavesColor YAxisAdditionalTicksColor
    {
        get => GetValue(YAxisAdditionalTicksColorProperty);
        set => SetValue(YAxisAdditionalTicksColorProperty, value);
    }

    /// <inheritdoc />
    public WavesColor YAxisZeroLineColor
    {
        get => GetValue(YAxisZeroLineColorProperty);
        set => SetValue(YAxisZeroLineColorProperty, value);
    }

    /// <inheritdoc />
    public WavesAxisHorizontalSignatureAlignment HorizontalSignatureAlignment
    {
        get => GetValue(HorizontalSignatureAlignmentProperty);
        set => SetValue(HorizontalSignatureAlignmentProperty, value);
    }

    /// <inheritdoc />
    public WavesAxisVerticalSignatureAlignment VerticalSignatureAlignment
    {
        get => GetValue(VerticalSignatureAlignmentProperty);
        set => SetValue(VerticalSignatureAlignmentProperty, value);
    }

    /// <inheritdoc />
    public bool HasDefaultTicks
    {
        get => GetValue(HasDefaultTicksProperty);
        private set => SetValue(HasDefaultTicksProperty, value);
    }

    /// <summary>
    /// Gets or sets whether if Shift key pressed or not.
    /// </summary>
    public bool IsShiftPressed
    {
        get => GetValue(IsShiftPressedProperty);
        private set => SetValue(IsShiftPressedProperty, value);
    }

    /// <summary>
    /// Gets or sets whether if Ctrl key pressed or not.
    /// </summary>
    public bool IsCtrlPressed
    {
        get => GetValue(IsCtrlPressedProperty);
        private set => SetValue(IsCtrlPressedProperty, value);
    }

    /// <summary>
    /// Gets or sets whether if mouse over control.
    /// </summary>
    public bool IsMouseOver
    {
        get => GetValue(IsMouseOverProperty);
        private set => SetValue(IsMouseOverProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);

    /// <inheritdoc />
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        Focus();
        IsMouseOver = true;
    }

    /// <inheritdoc />
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        IsMouseOver = false;
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
        {
            IsShiftPressed = true;
        }

        if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        {
            IsCtrlPressed = true;
        }
    }

    /// <inheritdoc />
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
        {
            IsShiftPressed = false;
        }

        if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        {
            IsCtrlPressed = false;
        }
    }

    /// <inheritdoc />
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        var position = e.GetPosition(this);
        var delta = e.Delta;

        ZoomChart(delta, position);
    }

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        PrepareBackground();
        PrepareGrid();

        base.Refresh(context);
    }

    /// <summary>
    /// Refreshes background.
    /// </summary>
    protected void PrepareBackground()
    {
        if (_background != null)
        {
            DrawingObjects?.Remove(_background);
        }

        _background = new WavesRectangle()
        {
            CornerRadius = CornerRadius.TopLeft,
            Fill = BackgroundColor,
            Location = new WavesPoint(0, 0),
            Width = Bounds.Width,
            Height = Bounds.Height,
        };

        DrawingObjects?.Add(_background);
    }

    /// <summary>
    /// Prepares chart (background, ticks).
    /// </summary>
    protected void PrepareGrid()
    {
        if (HasDefaultTicks)
        {
            HasDefaultTicks = this.GenerateDefaultTicks(_ticks);
        }

        // generate axis ticks
        this.GenerateAxisTicksDrawingObjects(
            _ticks,
            _ticksCache,
            Bounds.Width,
            Bounds.Height);

        // generate signatures
        this.GenerateAxisSignaturesDrawingObjects(
            Renderer,
            _ticks,
            _signaturesCache,
            Bounds.Width,
            Bounds.Height);
    }

    /// <summary>
    /// On XMin changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnXMinChanged(AvaloniaPropertyChangedEventArgs<double> obj)
    {
        var newValue = obj.NewValue.Value;
        if (Math.Abs(newValue - _xMin) < double.Epsilon)
        {
            return;
        }

        _xMin = newValue;
        CurrentXMin = newValue;
    }

    /// <summary>
    /// On XMax changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnXMaxChanged(AvaloniaPropertyChangedEventArgs<double> obj)
    {
        var newValue = obj.NewValue.Value;
        if (Math.Abs(newValue - _xMax) < double.Epsilon)
        {
            return;
        }

        _xMax = newValue;
        CurrentXMax = newValue;
    }

    /// <summary>
    /// On YMin changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnYMinChanged(AvaloniaPropertyChangedEventArgs<double> obj)
    {
        var newValue = obj.NewValue.Value;
        if (Math.Abs(newValue - _yMin) < double.Epsilon)
        {
            return;
        }

        _yMin = newValue;
        CurrentYMin = newValue;
    }

    /// <summary>
    /// On YMax changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnYMaxChanged(AvaloniaPropertyChangedEventArgs<double> obj)
    {
        var newValue = obj.NewValue.Value;
        if (Math.Abs(newValue - _yMax) < double.Epsilon)
        {
            return;
        }

        _yMax = newValue;
        CurrentYMax = newValue;
    }

    /// <summary>
    ///     Zooms chart.
    /// </summary>
    /// <param name="delta">Zoom delta.</param>
    /// <param name="position">Zoom position.</param>
    private void ZoomChart(Vector delta, Point position)
    {
        if (!IsMouseOver)
        {
            return;
        }

        //// if (!IsZoomEnabled)
        //// {
        ////     return;
        //// }

        var deltaF = -delta.Y;

        var x = Valuation.DenormalizePointX2D(position.X, Bounds.Width, CurrentXMin, CurrentXMax);
        var y = Valuation.DenormalizePointY2D(position.Y, Bounds.Height, CurrentYMin, CurrentYMax);

        if (double.IsInfinity(x))
        {
            return;
        }

        if (double.IsInfinity(y))
        {
            return;
        }

        if (IsCtrlPressed)
        {
            var yMin = 0.0d;
            var yMax = 0.0d;

            if (false) // TODO:
            {
                yMin = -CurrentYMin * deltaF;
                yMax = CurrentYMax * deltaF;
            }
            else
            {
                yMin = (y - CurrentYMin) * deltaF;
                yMax = (CurrentYMax - y) * deltaF;
            }

            if (CurrentYMax - yMax - (CurrentYMin + yMin) > (YMax - YMin) / 1000000)
            {
                CurrentYMax -= yMax;
                CurrentYMin += yMin;
            }

            if (CurrentYMin < YMin)
            {
                CurrentYMin = YMin;
            }

            if (CurrentYMax > YMax)
            {
                CurrentYMax = YMax;
            }

            InvalidateVisual();
            return;
        }

        if (IsShiftPressed)
        {
            ScrollChart(deltaF, x, y);

            InvalidateVisual();
            return;
        }

        var xMin = (x - CurrentXMin) * deltaF;
        var xMax = (CurrentXMax - x) * deltaF;

        if (CurrentXMax - xMax - (CurrentXMin + xMin) > (XMax - XMin) / 1000000)
        {
            CurrentXMax -= xMax;
            CurrentXMin += xMin;
        }

        if (CurrentXMin < XMin)
        {
            CurrentXMin = XMin;
        }

        if (CurrentXMax > XMax)
        {
            CurrentXMax = XMax;
        }

        InvalidateVisual();
    }

    /// <summary>
    ///     Scrolls chart along the X axis.
    /// </summary>
    /// <param name="delta">Scrolling delta.</param>
    /// <param name="x">Scroll value along the X axis.</param>
    /// <param name="y">Scroll value along the Y axis.</param>
    private void ScrollChart(double delta, double x, double y)
    {
        var xMin = delta / 100d;
        var xMax = delta / 100d;

        if (CurrentXMax + xMax > XMax)
        {
            return;
        }

        if (CurrentXMin + xMin < XMin)
        {
            return;
        }

        CurrentXMax += xMax;
        CurrentXMin += xMin;

        if (CurrentXMin < XMin)
        {
            CurrentXMin = XMin;
        }

        if (CurrentXMax > XMax)
        {
            CurrentXMax = XMax;
        }
    }
}
