using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Base.Attributes;
using Waves.UI.Charts.Series;
using Waves.UI.Presentation;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages.Examples;

/// <summary>
/// Sandbox view model.
/// </summary>
[WavesViewModel(typeof(SandboxViewModel))]
public class SandboxViewModel : WavesViewModelBase
{
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

    private Task OnAddSeries()
    {
        throw new System.NotImplementedException();
    }
}
