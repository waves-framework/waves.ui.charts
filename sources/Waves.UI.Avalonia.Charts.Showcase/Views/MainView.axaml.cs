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

        var length = 10000;
        var x = new double[length];
        var y = new double[length];
        for (var i = 0; i < length; i++)
        {
            x[i] = i / (double)length;
            y[i] = 0.8 * Math.Sin(i / 1000.0d);
        }

        var series = new WavesPointSeries(x, y)
        {
            Color = WavesColor.Red
        };

        surface?.AddSeries(series);

        var phase = 0d;
        var task = new Task(async () =>
        {
            do
            {
                var random = new Random();
                var x = new double[length];
                var y = new double[length];
                phase += 0.01;
                for (var i = 0; i < length; i++)
                {
                    var randomValue = random.NextDouble() / 10;
                    x[i] = i / (double)length;
                    y[i] = randomValue + 0.8 * Math.Sin(i / 1000.0d + phase);
                }

                if (phase > 2 * Math.PI)
                {
                    phase = 0;
                }

                await Dispatcher.UIThread.InvokeAsync(() => { surface?.UpdateSeries(0, x, y); });
                await Task.Delay(33);
            }
            while (true);
        });
        task.Start();
    }
}
