using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Enums;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Dialogs;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;
using Waves.UI.Presentation;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages.Examples;

/// <summary>
/// Sandbox view model.
/// </summary>
[WavesViewModel(typeof(SandboxViewModel))]
public class SandboxViewModel : WavesViewModelBase
{
    private readonly IWavesNavigationService _navigationService;
    private readonly List<IDisposable> _disposables = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="SandboxViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public SandboxViewModel(IWavesNavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Gets or sets auto scale.
    /// </summary>
    [Reactive]
    public bool IsAutoScale { get; set; }

    /// <summary>
    /// Gets or sets selected signatures format type.
    /// </summary>
    [Reactive]
    public WavesSignaturesFormatType SelectedSignaturesFormatType { get; set; }

    /// <summary>
    /// Gets or sets signature format types.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesSignaturesFormatType> AvailableSignaturesFormatTypes { get; set; }

    /// <summary>
    /// Gets or sets X Min.
    /// </summary>
    [Reactive]
    public object XMin { get; set; }

    /// <summary>
    /// Gets or sets X Max.
    /// </summary>
    [Reactive]
    public object XMax { get; set; }

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
    /// Gets or sets series.
    /// </summary>
    [Reactive]
    public ObservableCollection<IWaves2DSeries> Series { get; set; }

    /// <summary>
    /// Add series command.
    /// </summary>
    public ICommand AddSeriesCommand { get; private set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        // default values
        IsAutoScale = true;
        XMin = 0d;
        XMax = 1d;
        YMin = -0.2d;
        YMax = 1.2d;
        SelectedSignaturesFormatType = WavesSignaturesFormatType.DateTime;

        // collections
        Series = new ObservableCollection<IWaves2DSeries>();
        AvailableSignaturesFormatTypes = new ObservableCollection<WavesSignaturesFormatType>()
        {
            WavesSignaturesFormatType.Double,
            WavesSignaturesFormatType.DateTime,
        };

        // commands
        AddSeriesCommand = ReactiveCommand.CreateFromTask(OnAddSeries);

        // observables
        _disposables.Add(this.WhenAnyValue(
                x => x.SelectedSignaturesFormatType)
            .Subscribe(_ => OnSignaturesFormatChanged()));
    }

    private void OnSignaturesFormatChanged()
    {
        if (IsAutoScale)
        {
            AutoScale();
        }
    }

    private async Task OnAddSeries()
    {
        var result = await _navigationService.NavigateAsync<AddSeriesDialogViewModel, IWaves2DSeries>();
        Series.Add(result);

        if (IsAutoScale)
        {
            AutoScale();
        }
    }

    private void AutoScale()
    {
        if (Series == null || Series.Count == 0)
        {
            return;
        }

        var xMin = Series.Min(x => ValuesUtils.GetValue(x.XMin));
        var xMax = Series.Max(x => ValuesUtils.GetValue(x.XMax));
        var yMin = Series.Min(x => ValuesUtils.GetValue(x.YMin));
        var yMax = Series.Max(x => ValuesUtils.GetValue(x.YMax));

        switch (SelectedSignaturesFormatType)
        {
            case WavesSignaturesFormatType.Double:
                XMin = xMin;
                XMax = xMax;
                break;
            case WavesSignaturesFormatType.DateTime:
                XMin = DateTime.FromOADate(xMin);
                XMax = DateTime.FromOADate(xMax);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        YMin = yMin;
        YMax = yMax;
    }
}
