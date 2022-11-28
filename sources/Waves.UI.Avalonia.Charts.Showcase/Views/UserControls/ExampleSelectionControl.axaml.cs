using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Pages;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.UserControls;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.UserControls;

/// <summary>
/// Main view.
/// </summary>
[WavesView(typeof(ExampleSelectionControlViewModel), "Charts")]
public partial class ExampleSelectionControl : WavesPage
{
    /// <summary>
    /// Creates new instance of <see cref="ExampleSelectionControl"/>.
    /// </summary>
    public ExampleSelectionControl()
    {
        InitializeComponent();
    }
}
