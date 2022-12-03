using System.Collections.Concurrent;
using Avalonia;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Data;
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
    public static readonly StyledProperty<object> XMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, object>(
            nameof(XMin),
            0d,
            true);

    /// <summary>
    /// Defines <see cref="XMax"/> styled property.
    /// </summary>
    public static readonly AttachedProperty<object> XMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, object>(
            nameof(XMax),
            1d,
            true);

    /// <summary>
    /// Defines <see cref="YMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(YMin),
            -1d,
            true);

    /// <summary>
    /// Defines <see cref="YMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> YMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(YMax),
            1d,
            true);

    /// <summary>
    /// Defines <see cref="CurrentXMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<object> CurrentXMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, object>(
            nameof(CurrentXMin),
            0d,
            true);

    /// <summary>
    /// Defines <see cref="CurrentXMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<object> CurrentXMaxProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, object>(
            nameof(CurrentXMax),
            1d,
            true);

    /// <summary>
    /// Defines <see cref="CurrentYMin"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentYMinProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, double>(
            nameof(CurrentYMin),
            -1d,
            true);

    /// <summary>
    /// Defines <see cref="CurrentYMax"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> CurrentYMaxProperty =
        AvaloniaProperty.Register<WavesChart, double>(
            nameof(CurrentYMax),
            1d);

    /// <summary>
    /// Defines <see cref="SignaturesXFormat"/> styled property.
    /// </summary>
    public static readonly StyledProperty<string> SignaturesXFormatProperty =
        AvaloniaProperty.Register<WavesChart, string>(
            nameof(SignaturesXFormat),
            null);

    /// <summary>
    /// Defines <see cref="XAxisPrimaryTicksNumber"/> styled property.
    /// </summary>
    public static readonly StyledProperty<int> XAxisPrimaryTicksNumberProperty =
        AvaloniaProperty.Register<WavesChart, int>(
            nameof(XAxisPrimaryTicksNumber),
            2);

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

    private readonly List<IWavesDrawingObject> _cache = new ();
    private readonly List<IWavesDrawingObject> _ticksCache = new ();
    private readonly List<IWavesDrawingObject> _signaturesCache = new ();

    private IWavesDrawingObject _background;
    private object _xMin = 0d;
    private object _xMax = 1d;
    private double _yMin = -1d;
    private double _yMax = 1d;

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
    public object XMin
    {
        get => GetValue(XMinProperty);
        set => SetValue(XMinProperty, value);
    }

    /// <inheritdoc />
    public object XMax
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
    public object CurrentXMin
    {
        get => GetValue(CurrentXMinProperty);
        set => SetValue(CurrentXMinProperty, value);
    }

    /// <inheritdoc />
    public object CurrentXMax
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
    public string SignaturesXFormat
    {
        get => GetValue(SignaturesXFormatProperty);
        set => SetValue(SignaturesXFormatProperty, value);
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
        protected set => SetValue(HasDefaultTicksProperty, value);
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

    /// <summary>
    /// Gets or sets ticks.
    /// </summary>
    protected List<WavesAxisTick> Ticks { get; set; } = new List<WavesAxisTick>();

    /// <inheritdoc />
    public void SetCache(IWavesDrawingObject obj)
    {
        _cache.Add(obj);
    }

    /// <inheritdoc />
    public void SetCache(IEnumerable<IWavesDrawingObject> obj)
    {
        _cache.AddRange(obj);
    }

    /// <inheritdoc />
    public void ReleaseCache(IWavesDrawingObject obj)
    {
        _cache.Remove(obj);
    }

    /// <inheritdoc />
    public void ReleaseCache(IEnumerable<IWavesDrawingObject> objs)
    {
        foreach (var obj in objs)
        {
            _cache.Remove(obj);
        }
    }

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
            this.GenerateDefaultTicks(Ticks, SignaturesXFormat);
        }

        // generate axis ticks
        this.GenerateAxisTicksDrawingObjects(
            Ticks,
            _ticksCache,
            Bounds.Width,
            Bounds.Height);

        // generate signatures
        this.GenerateAxisSignaturesDrawingObjects(
            Renderer,
            Ticks,
            _signaturesCache,
            Bounds.Width,
            Bounds.Height);
    }

    /// <summary>
    /// On value changed.
    /// </summary>
    /// <param name="newValue">New value.</param>
    /// <param name="oldValue">Old value cache.</param>
    /// <returns>Returns changed or not.</returns>
    private bool OnValueChanged(object newValue, ref object oldValue)
    {
        if (newValue is double d && oldValue is double xMinD)
        {
            if (Math.Abs(d - xMinD) < double.Epsilon)
            {
                return false;
            }
        }

        if (newValue is DateTime dt && oldValue is DateTime xMinDt)
        {
            if (dt == xMinDt)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// On value changed.
    /// </summary>
    /// <param name="newValue">New value.</param>
    /// <param name="oldValue">Old value cache.</param>
    /// <returns>Returns changed or not.</returns>
    private bool OnValueChanged(double newValue, ref double oldValue)
    {
        if (Math.Abs(newValue - oldValue) < double.Epsilon)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// On XMin changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnXMinChanged(AvaloniaPropertyChangedEventArgs<object> obj)
    {
        var newValue = obj.NewValue.Value;
        if (!OnValueChanged(newValue, ref _xMin))
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
    private void OnXMaxChanged(AvaloniaPropertyChangedEventArgs<object> obj)
    {
        var newValue = obj.NewValue.Value;
        if (!OnValueChanged(newValue, ref _xMax))
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
        if (!OnValueChanged(newValue, ref _yMin))
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
        if (!OnValueChanged(newValue, ref _yMax))
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

        var xMin = Values.GetValue(XMin);
        var xMax = Values.GetValue(XMax);
        var yMin = Values.GetValue(YMin);
        var yMax = Values.GetValue(YMax);
        var currentXMin = Values.GetValue(CurrentXMin);
        var currentXMax = Values.GetValue(CurrentXMax);
        var currentYMin = Values.GetValue(CurrentYMin);
        var currentYMax = Values.GetValue(CurrentYMax);

        var deltaFY = -delta.Y;
        var deltaFX = -delta.X;

        var x = Valuation.DenormalizeValueX(position.X, Bounds.Width, currentXMin, currentXMax);
        var y = Valuation.DenormalizeValueY(position.Y, Bounds.Height, CurrentYMin, CurrentYMax);

        if (double.IsInfinity(x))
        {
            return;
        }

        if (double.IsInfinity(y))
        {
            return;
        }

        if (IsShiftPressed)
        {
            Console.WriteLine("X: " + deltaFX);
            ScrollChart(deltaFY, x, y);
            return;
        }

        Console.WriteLine("Y: " + deltaFY);
        var xMinDelta = (x - currentXMin) * deltaFY;
        var xMaxDelta = (currentXMax - x) * deltaFY;

        if (currentXMax - xMaxDelta - (currentXMin + xMinDelta) > (xMax - xMin) / 1000000)
        {
            if (CurrentXMin is double && CurrentXMax is double)
            {
                CurrentXMin = currentXMin + xMinDelta;
                CurrentXMax = currentXMax - xMaxDelta;
            }

            if (CurrentXMin is DateTime && CurrentXMax is DateTime)
            {
                CurrentXMin = DateTime.FromOADate(currentXMin + xMinDelta);
                CurrentXMax = DateTime.FromOADate(currentXMax - xMaxDelta);
            }
        }

        if (currentXMin < xMin)
        {
            CurrentXMin = XMin;
        }

        if (currentXMax > xMax)
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
        if (delta == 0)
        {
            return;
        }

        var min = Values.GetValue(XMin);
        var max = Values.GetValue(XMax);
        var currentMin = Values.GetValue(CurrentXMin);
        var currentMax = Values.GetValue(CurrentXMax);

        var minDelta = (x - currentMin) * delta;
        var maxDelta = (currentMax - x) * delta;

        if (currentMax + maxDelta > max)
        {
            return;
        }

        if (currentMin + minDelta < min)
        {
            return;
        }

        if (CurrentXMin is double && CurrentXMax is double)
        {
            CurrentXMin = currentMin - minDelta;
            CurrentXMax = currentMax - maxDelta;
        }

        if (CurrentXMin is DateTime && CurrentXMax is DateTime)
        {
            CurrentXMin = DateTime.FromOADate(currentMin - minDelta);
            CurrentXMax = DateTime.FromOADate(currentMax - maxDelta);
        }

        if (currentMin < min)
        {
            CurrentXMin = XMin;
        }

        if (currentMax > max)
        {
            CurrentXMax = XMax;
        }

        InvalidateVisual();
    }
}
