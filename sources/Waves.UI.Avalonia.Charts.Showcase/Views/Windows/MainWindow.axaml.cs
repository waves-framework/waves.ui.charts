using Waves.UI.Avalonia.Charts.Showcase.ViewModels.Windows;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;

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
}
