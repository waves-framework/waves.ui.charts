using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Dialogs;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.Dialogs;

/// <summary>
/// Main view.
/// </summary>
[WavesView(typeof(AddSeriesDialogViewModel))]
public partial class AddSeriesDialogView : WavesDialog
{
    /// <summary>
    /// Creates new instance of <see cref="AddSeriesDialogView"/>.
    /// </summary>
    public AddSeriesDialogView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creates new instance of <see cref="AddSeriesDialogView"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="navigationService">Navigation service.</param>
    public AddSeriesDialogView(
        ILogger<AddSeriesDialogView> logger,
        IWavesNavigationService navigationService)
        : base(logger, navigationService)
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initializes components.
    /// </summary>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
