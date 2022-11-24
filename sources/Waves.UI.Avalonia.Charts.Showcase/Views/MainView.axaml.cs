using System;
using System.Drawing;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Extensions;
using Waves.UI.Avalonia.Charts.Controls;
using Waves.UI.Avalonia.Charts.Primitives;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series;

namespace Waves.UI.Avalonia.Charts.Showcase.Views;

/// <summary>
/// Main view.
/// </summary>
public partial class MainView : UserControl
{
    /// <summary>
    /// Creates new instance of <see cref="MainView"/>.
    /// </summary>
    public MainView()
    {
        InitializeComponent();
        InitializeChart();
    }

    /// <summary>
    /// Initializes chart.
    /// </summary>
    private void InitializeChart()
    {
        var surface = this.FindControl<WavesPointSeriesChart>("Surface");
    }
}
