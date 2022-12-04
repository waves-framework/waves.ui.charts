using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Enums;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators;
using Waves.UI.Avalonia.Charts.Showcase.Models.Generators.Interfaces;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Dialogs;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Dialogs;

/// <summary>
/// Add series dialog view model.
/// </summary>
[WavesViewModel(typeof(AddSeriesDialogViewModel))]
public class AddSeriesDialogViewModel : WavesDialogViewModelBase<IWaves2DSeries>, IDisposable
{
    private readonly List<IDisposable> _disposables = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="AddSeriesDialogViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger.</param>
    public AddSeriesDialogViewModel(
        IWavesNavigationService navigationService,
        ILogger<AddSeriesDialogViewModel> logger)
        : base(navigationService, logger)
    {
    }

    /// <summary>
    /// Gets generator.
    /// </summary>
    [Reactive]
    public IWaves2DSeriesGenerator Generator { get; private set; }

    /// <summary>
    /// Gets or sets selected series type.
    /// </summary>
    [Reactive]
    public WavesSeriesType SelectedSeriesType { get; set; }

    /// <summary>
    /// Gets or sets selected series generator type.
    /// </summary>
    [Reactive]
    public WavesSeriesGeneratorType SelectedSeriesGeneratorType { get; set; }

    /// <summary>
    /// Gets available series types.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesSeriesType> AvailableSeriesTypes { get; set; }

    /// <summary>
    /// Gets available series generator types.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesSeriesGeneratorType> AvailableSeriesGeneratorTypes { get; set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        // default properties
        SelectedSeriesType = WavesSeriesType.Line;
        SelectedSeriesGeneratorType = WavesSeriesGeneratorType.Random;

        // collections
        AvailableSeriesTypes = new ObservableCollection<WavesSeriesType>
        {
            WavesSeriesType.Line,
            WavesSeriesType.Bar,
            WavesSeriesType.Candle,
        };
        AvailableSeriesGeneratorTypes = new ObservableCollection<WavesSeriesGeneratorType>
        {
            WavesSeriesGeneratorType.Random,
        };

        // changes
        _disposables.Add(this.WhenAnyValue(
                x => x.SelectedSeriesType,
                x => x.SelectedSeriesGeneratorType)
            .Subscribe(_ => ChangeGenerator()));
    }

    /// <inheritdoc/>
    public override async Task OnDone()
    {
        var series = await Generator.Generate();
        Result = series;
        await base.OnDone();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }

        _disposables.Clear();
    }

    private async Task ChangeGenerator()
    {
        switch (SelectedSeriesType)
        {
            case WavesSeriesType.Line or WavesSeriesType.Bar:
                Generator = SelectedSeriesGeneratorType switch
                {
                    WavesSeriesGeneratorType.Random => new RandomDataPointSeriesGenerator(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                break;
            case WavesSeriesType.Candle:
                Generator = new RandomDataCandleSeriesGenerator();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
