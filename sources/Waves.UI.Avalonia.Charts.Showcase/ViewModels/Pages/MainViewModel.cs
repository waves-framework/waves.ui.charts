using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.UserControls;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Windows;
using Waves.UI.Base.Attributes;
using Waves.UI.Presentation;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;

/// <summary>
/// Main view model.
/// </summary>
[WavesViewModel(typeof(MainViewModel))]
public class MainViewModel : WavesViewModelBase
{
    private readonly IWavesNavigationService _navigationService;

    /// <summary>
    /// Creates new instance of <see cref="MainViewModel"/>.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public MainViewModel(IWavesNavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await _navigationService.NavigateAsync<ExampleSelectionControlViewModel>();
    }
}
