using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Waves.UI.Base.Attributes;
using Waves.UI.Presentation;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages.Examples;

/// <summary>
/// View-model for example selection control.
/// </summary>
[WavesViewModel(typeof(ExampleSelectionControlViewModel))]
public class ExampleSelectionControlViewModel : WavesViewModelBase
{
    private readonly IWavesNavigationService _navigationService;

    /// <summary>
    /// Creates new instance of <see cref="ExampleSelectionControlViewModel"/>.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public ExampleSelectionControlViewModel(IWavesNavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Gets command to open PointSeriesChart.
    /// </summary>
    public ICommand OpenPointSeriesChartCommand { get; private set; }

    /// <summary>
    /// Gets command to open CandleSeriesChart.
    /// </summary>
    public ICommand OpenCandleSeriesChartCommand { get; private set; }

    /// <summary>
    /// Gets command to open sandbox.
    /// </summary>
    public ICommand OpenSandboxCommand { get; private set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        OpenPointSeriesChartCommand = ReactiveCommand.CreateFromTask(OnOpenPointSeriesChart);
        OpenCandleSeriesChartCommand = ReactiveCommand.CreateFromTask(OnOpenCandleSeriesChart);
        OpenSandboxCommand = ReactiveCommand.CreateFromTask(OnOpenSandbox);
    }

    private async Task OnOpenPointSeriesChart()
    {
        await _navigationService.NavigateAsync<PointSeriesChartViewModel>();
    }

    private async Task OnOpenCandleSeriesChart()
    {
        await _navigationService.NavigateAsync<CandleSeriesChartViewModel>();
    }

    private async Task OnOpenSandbox()
    {
        await _navigationService.NavigateAsync<SandboxViewModel>();
    }
}
