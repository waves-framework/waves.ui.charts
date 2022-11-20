using System.Drawing;
using Avalonia.Controls;
using Waves.UI.Avalonia.Charts.Controls;
using Waves.UI.Avalonia.Charts.Primitives;
using Waves.UI.Charts.Drawing.Primitives;

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
        var surface = this.FindControl<WavesSurface>("Surface");
        var objects = new WavesDrawingObjects
        {
            new WavesLine()
            {
                Point1 = new Point(0, 0),
                Point2 = new Point(50, 50),
            },
            new WavesLine()
            {
                Point1 = new Point(0, 50),
                Point2 = new Point(50, 0),
            },
        };

        surface.DrawingObjects = objects;
    }
}
