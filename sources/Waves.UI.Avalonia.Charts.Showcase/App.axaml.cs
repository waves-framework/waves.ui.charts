using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Windows;

namespace Waves.UI.Avalonia.Charts.Showcase;

/// <summary>
/// Application.
/// </summary>
public partial class App : WavesApplication
{
    /// <inheritdoc />
    public override void Initialize()
    {
        base.Initialize();
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override async void OnFrameworkInitializationCompleted()
    {
        try
        {
            switch (ApplicationLifetime)
            {
                case IClassicDesktopStyleApplicationLifetime desktop:
                    await NavigationService.NavigateAsync<MainWindowViewModel>();
                    await NavigationService.NavigateAsync<MainViewModel>();
                    break;
                case ISingleViewApplicationLifetime singleViewPlatform:
                    await NavigationService.NavigateAsync<MainViewModel>();
                    break;
            }

            base.OnFrameworkInitializationCompleted();
        }
        catch (Exception e)
        {
            Logger.LogError("Error occured while doing initial navigation: {Message}", e);
        }
    }
}
