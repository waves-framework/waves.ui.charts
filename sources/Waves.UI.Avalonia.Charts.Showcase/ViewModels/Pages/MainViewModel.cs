﻿using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
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

    /// <summary>
    /// Gets go back command.
    /// </summary>
    public ICommand GoBackCommand { get; private set; }

    /// <inheritdoc/>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        GoBackCommand = ReactiveCommand.CreateFromTask(OnGoBack);

        await _navigationService.NavigateAsync<SandboxViewModel>();
    }

    private async Task OnGoBack()
    {
        await _navigationService.GoBackAsync(Constants.ExampleRegionKey);
    }
}
