using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Showcase.ViewModels;

/// <summary>
/// Main view model.
/// </summary>
public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Creates new instance of <see cref="MainViewModel"/>.
    /// </summary>
    public MainViewModel()
    {
        InitializeChart();
    }

    /// <summary>
    /// Gets or sets series.
    /// </summary>
    [Reactive]
    public ObservableCollection<IWavesPointSeries> Series { get; set; }

    /// <summary>
    /// Initializes chart.
    /// </summary>
    private void InitializeChart()
    {
        Series = new ObservableCollection<IWavesPointSeries>();

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

        Series.Add(series);

        var phase = 0d;
        var task = new Task(async () =>
        {
            await Task.Delay(1000);

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

                await Dispatcher.UIThread.InvokeAsync(() => { series?.Update(x, y); });
                await Task.Delay(33);
            }
            while (true);
        });
        task.Start();
    }
}
