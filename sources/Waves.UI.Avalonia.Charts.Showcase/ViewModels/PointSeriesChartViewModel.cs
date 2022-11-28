using System;
using System.Collections.ObjectModel;
using System.Linq;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Presentation;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels;

/// <summary>
/// Point series chart view model.
/// </summary>
public class PointSeriesChartViewModel : WavesViewModelBase
{
    private WavesPointSeries _series;

    /// <summary>
    /// Creates new instance of <see cref="PointSeriesChartViewModel"/>.
    /// </summary>
    public PointSeriesChartViewModel()
    {
        SelectedSymbol = "BTCUSDT";
        Initialize();
    }

    /// <summary>
    /// Get or sets symbol.
    /// </summary>
    [Reactive]
    public string SelectedSymbol { get; set; }

    /// <summary>
    /// Gets or sets selected series type.
    /// </summary>
    [Reactive]
    public WavesPointSeriesType SelectedSeriesType { get; set; }

    /// <summary>
    /// Gets or sets series type.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesPointSeriesType> SeriesTypes { get; set; }

    /// <summary>
    /// Gets or sets selected dot type.
    /// </summary>
    [Reactive]
    public WavesDotType SelectedDotType { get; set; }

    /// <summary>
    /// Gets or sets dots type.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesDotType> DotsTypes { get; set; }

    /// <summary>
    /// Selected series color.
    /// </summary>
    [Reactive]
    public WavesColor SelectedSeriesColor { get; set; }

    /// <summary>
    /// Gets or sets available series colors.
    /// </summary>
    public ObservableCollection<WavesColor> AvailableSeriesColors { get; set; }

    /// <summary>
    /// Gets or sets X Min.
    /// </summary>
    [Reactive]
    public double XMin { get; set; }

    /// <summary>
    /// Gets or sets X Max.
    /// </summary>
    [Reactive]
    public double XMax { get; set; }

    /// <summary>
    /// Gets or sets Y Min.
    /// </summary>
    [Reactive]
    public double YMin { get; set; }

    /// <summary>
    /// Gets or sets Y Max.
    /// </summary>
    [Reactive]
    public double YMax { get; set; }

    /// <summary>
    /// Gets or sets Signature X min.
    /// </summary>
    [Reactive]
    public object SignatureXMin { get; set; }

    /// <summary>
    /// Gets or sets Signature X max.
    /// </summary>
    [Reactive]
    public object SignatureXMax { get; set; }

    /// <summary>
    /// Gets or sets series.
    /// </summary>
    [Reactive]
    public ObservableCollection<IWavesPointSeries> Series { get; set; }

    /// <summary>
    /// Initializes chart.
    /// </summary>
    private async void Initialize()
    {
        Series = new ObservableCollection<IWavesPointSeries>();
        SeriesTypes = new ObservableCollection<WavesPointSeriesType>()
        {
            WavesPointSeriesType.Line,
            WavesPointSeriesType.Bar,
        };
        DotsTypes = new ObservableCollection<WavesDotType>()
        {
            WavesDotType.None,
            WavesDotType.Circle,
            WavesDotType.FilledCircle,
        };
        AvailableSeriesColors = new ObservableCollection<WavesColor>()
        {
            WavesColor.Blue,
            WavesColor.Green,
            WavesColor.Red,
            WavesColor.LightGray,
        };

        SelectedSeriesType = WavesPointSeriesType.Line;
        SelectedDotType = WavesDotType.FilledCircle;
        SelectedSeriesColor = WavesColor.Red;

        InitializeChartData();

        this.WhenPropertyChanged(x => x.SelectedSeriesType).Subscribe(_ => Update());
        this.WhenPropertyChanged(x => x.SelectedDotType).Subscribe(_ => Update());
        this.WhenPropertyChanged(x => x.SelectedSeriesColor).Subscribe(_ => Update());
    }

    private void InitializeChartData()
    {
        var length = 50;
        var startX = 1000d;
        var endX = 4000d;
        var step = (endX - startX) / length;

        var x = new double[length];
        var y = new double[length];
        var random = new Random();

        x[0] = startX;
        y[0] = 1;

        for (var i = 1; i < length; i++)
        {
            x[i] = startX + i * step;
            y[i] = Math.Sin(0.5 * i) / (0.5 * i);
        }

        _series = new WavesPointSeries(x, y)
        {
            Color = WavesColor.Green,
            Type = SelectedSeriesType,
            DotType = WavesDotType.FilledCircle,
        };

        Series.Add(_series);

        var xmin = x.Min();
        var xmax = x.Max();
        var ymin = y.Min();
        var ymax = y.Max();

        ymin -= 10 * (ymax - ymin) / 100;
        ymax += 10 * (ymax - ymin) / 100;

        XMin = xmin;
        XMax = xmax;
        YMin = ymin;
        YMax = ymax;
    }

    private void Update()
    {
        _series.Color = SelectedSeriesColor;
        _series.Type = SelectedSeriesType;
        _series.DotType = SelectedDotType;

        _series.Update();
    }
}
