using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages.Examples;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.Pages.Examples;

/// <summary>
/// Main view.
/// </summary>
[WavesView(typeof(SandboxViewModel), Constants.ExampleRegionKey)]
public partial class SandboxView : WavesPage
{
    /// <summary>
    /// Creates new instance of <see cref="SandboxView"/>.
    /// </summary>
    public SandboxView()
    {
        InitializeComponent();
    }
}
