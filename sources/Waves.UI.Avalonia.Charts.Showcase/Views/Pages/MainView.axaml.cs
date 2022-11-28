using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.Pages;

/// <summary>
/// Main view.
/// </summary>
[WavesView(typeof(MainViewModel))]
public partial class MainView : WavesPage
{
    /// <summary>
    /// Creates new instance of <see cref="MainView"/>.
    /// </summary>
    public MainView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creates new instance of <see cref="MainView"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Navigation service.</param>
    public MainView(
        ILogger<MainView> logger,
        IWavesNavigationService navigationService)
        : base(logger, navigationService)
    {
        InitializeComponent();
    }
}
