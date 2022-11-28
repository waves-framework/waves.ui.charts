using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Waves.UI.Base.Attributes;
using Waves.UI.Presentation;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.UserControls;

/// <summary>
/// View-model for example selection control.
/// </summary>
[WavesViewModel(typeof(ExampleSelectionControlViewModel))]
public class ExampleSelectionControlViewModel : WavesViewModelBase
{
    /// <summary>
    /// Gets command to open PointSeriesChart.
    /// </summary>
    public ICommand OpenPointSeriesChartCommand { get; private set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        OpenPointSeriesChartCommand = ReactiveCommand.CreateFromTask(OnOpenPointSeriesChart);
    }

    private Task OnOpenPointSeriesChart()
    {
        throw new System.NotImplementedException();
    }
}
