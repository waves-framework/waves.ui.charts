using System;
using System.Drawing;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Waves.Core.Extensions;
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
        // var surface = this.FindControl<WavesSurface>("Surface");
        //
        // var task = new Task(async () =>
        // {
        //     var random = new Random();
        //     do
        //     {
        //         var value = random.Next(100);
        //         var objects = new WavesDrawingObjects
        //         {
        //             new WavesLine()
        //             {
        //                 Fill = Color.Red,
        //                 StrokeThickness = 2,
        //                 Point1 = new WavesPoint(0, 0),
        //                 Point2 = new WavesPoint(value, value),
        //             },
        //             new WavesLine()
        //             {
        //                 Fill = Color.Red,
        //                 StrokeThickness = 2,
        //                 Point1 = new WavesPoint(0, value),
        //                 Point2 = new WavesPoint(value, 0),
        //             },
        //         };
        //
        //         await Dispatcher.UIThread.InvokeAsync(() => { surface.DrawingObjects = objects; });
        //         await Task.Delay(1);
        //     }
        //     while (true);
        // });
        // task.Start();
    }
}
