using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Dialogs;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Series;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="SandboxViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public SandboxViewModel(IWavesNavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Gets or sets series.
    /// </summary>
    [Reactive]
    public ObservableCollection<WavesSeries> Series { get; set; }

    /// <summary>
    /// Add series command.
    /// </summary>
    public ICommand AddSeriesCommand { get; private set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        // commands
        AddSeriesCommand = ReactiveCommand.CreateFromTask(OnAddSeries);
    }

    private async Task OnAddSeries()
    {
        var result = await _navigationService.NavigateAsync<AddSeriesDialogViewModel, WavesSeries>();
    }
}
