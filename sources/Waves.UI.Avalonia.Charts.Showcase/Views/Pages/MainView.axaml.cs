using Avalonia.Controls;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;

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
}
