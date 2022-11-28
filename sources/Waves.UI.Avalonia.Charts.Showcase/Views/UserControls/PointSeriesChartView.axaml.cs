using Waves.UI.Avalonia.Charts.Showcase.ViewModels;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels.UserControls;
using Waves.UI.Avalonia.Controls;
using Waves.UI.Base.Attributes;

namespace Waves.UI.Avalonia.Charts.Showcase.Views.UserControls;

/// <summary>
/// Main view.
/// </summary>
[WavesView(typeof(PointSeriesChartViewModel), Constants.ExampleRegionKey)]
public partial class PointSeriesChartView : WavesPage
{
    /// <summary>
    /// Creates new instance of <see cref="PointSeriesChartView"/>.
    /// </summary>
    public PointSeriesChartView()
    {
        InitializeComponent();
    }
}
