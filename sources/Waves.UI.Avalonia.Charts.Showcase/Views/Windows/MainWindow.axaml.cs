using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Windows;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.Windows;

/// <summary>
/// Main window.
/// </summary>
[WavesView(typeof(MainWindowViewModel))]
public partial class MainWindow : WavesWindow
{
    /// <summary>
    /// Creates new instance of <see cref="MainWindow"/>.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Navigation service.</param>
    public MainWindow(
        ILogger<MainWindow> logger,
        IWavesNavigationService navigationService)
        : base(logger, navigationService)
    {
        InitializeComponent();
    }
}
