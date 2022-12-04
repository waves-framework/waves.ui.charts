using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.Models.Enums;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Series;
using Waves.UI.Dialogs;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Dialogs;

/// <summary>
/// Add series dialog view model.
/// </summary>
[WavesViewModel(typeof(AddSeriesDialogViewModel))]
public class AddSeriesDialogViewModel : WavesDialogViewModelBase<WavesSeries>
{
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
    }
}
