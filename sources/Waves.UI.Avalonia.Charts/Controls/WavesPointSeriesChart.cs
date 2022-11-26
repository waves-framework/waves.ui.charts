using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Extensions;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
///     Waves point series chart.
/// </summary>
public class WavesPointSeriesChart : WavesChart, IStyleable
{
    /// <summary>
    ///     Defines <see cref="Series" /> property.
    /// </summary>
    public static readonly AttachedProperty<ObservableCollection<IWavesPointSeries>> SeriesProperty =
        AvaloniaProperty.RegisterAttached<WavesSurface, WavesSurface, ObservableCollection<IWavesPointSeries>>(
            nameof(Series),
            new ObservableCollection<IWavesPointSeries>(),
            true);

    private readonly object _seriesLocker = new ();
    private readonly List<IWavesDrawingObject> _drawingObjectsCache = new ();

    /// <summary>
    ///     Creates new instance of <see cref="WavesChart" />.
    /// </summary>
    public WavesPointSeriesChart()
    {
        AffectsRender<WavesPointSeriesChart>(SeriesProperty);
        SeriesProperty.Changed.Subscribe(OnSeriesChanged);
    }

    /// <summary>
    ///     Gets or sets waves point series.
    /// </summary>
    public ObservableCollection<IWavesPointSeries> Series
    {
        get => GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);

    /// <summary>
    ///     Adds series.
    /// </summary>
    /// <param name="series">Series.</param>
    public void AddSeries(IWavesPointSeries series)
    {
        lock (_seriesLocker)
        {
            series.Updated += OnSeriesUpdated;
            Series.Add(series);
        }

        InvalidateVisual();
    }

    /// <summary>
    ///     Update series.
    /// </summary>
    /// <param name="index">Series index.</param>
    /// <param name="points">New points.</param>
    public void UpdateSeries(int index, WavesPoint[] points)
    {
        if (index >= Series.Count)
        {
            throw new Exception("Series index is not correct");
        }

        Series[index].Update(points);

        InvalidateVisual();
    }

    /// <summary>
    ///     Update series.
    /// </summary>
    /// <param name="index">Series index.</param>
    /// <param name="x">New X values.</param>
    /// <param name="y">New Y values.</param>
    public void UpdateSeries(int index, double[] x, double[] y)
    {
        if (index >= Series.Count)
        {
            throw new Exception("Series index is not correct");
        }

        Series[index].Update(x, y);

        InvalidateVisual();
    }

    /// <summary>
    ///     Removes series.
    /// </summary>
    /// <param name="index">Series index.</param>
    public void RemoveSeries(int index)
    {
        if (index >= Series.Count)
        {
            return;
        }

        lock (_seriesLocker)
        {
            var series = Series[index];
            series.Updated -= OnSeriesUpdated;
            Series.Remove(series);
        }

        InvalidateVisual();
    }

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        PrepareBackground();
        ClearCacheObjects();

        foreach (var series in Series)
        {
            switch (series.Type)
            {
                case WavesPointSeriesType.Line:
                    this.GenerateLineSeries(
                        series,
                        _drawingObjectsCache,
                        Bounds.Width,
                        Bounds.Height,
                        GetWavesColor(Background),
                        GetWavesColor(Foreground));
                    break;
                case WavesPointSeriesType.Bar:
                    this.GenerateBarSeries(
                        series,
                        _drawingObjectsCache,
                        Bounds.Width,
                        Bounds.Height,
                        GetWavesColor(Background),
                        GetWavesColor(Foreground));
                    break;
            }
        }

        PrepareGrid();

        if (!DrawingObjects.Any())
        {
            return;
        }

        RenderUpdate(context);
    }

    /// <summary>
    ///     Clears cache objects.
    /// </summary>
    private void ClearCacheObjects()
    {
        foreach (var obj in _drawingObjectsCache)
        {
            DrawingObjects?.Remove(obj);
        }

        _drawingObjectsCache.Clear();
    }

    /// <summary>
    /// On series updated callback.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments.</param>
    private void OnSeriesUpdated(object? sender, EventArgs e)
    {
        InvalidateVisual();
    }

    /// <summary>
    /// Callback when series changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    private void OnSeriesChanged(AvaloniaPropertyChangedEventArgs<ObservableCollection<IWavesPointSeries>> obj)
    {
        var series = obj.NewValue.Value;

        if (Series != null)
        {
            foreach (var item in Series)
            {
                item.Updated -= OnSeriesUpdated;
            }

            Series.CollectionChanged -= OnCollectionChanged;
        }

        Series = series;

        if (Series != null)
        {
            foreach (var item in Series)
            {
                item.Updated += OnSeriesUpdated;
            }

            Series.CollectionChanged += OnCollectionChanged;
        }
    }

    /// <summary>
    /// Callback when collection changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Args.</param>
    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is IWavesPointSeries series)
                    {
                        series.Updated -= OnSeriesUpdated;
                    }
                }
            }
        }

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is IWavesPointSeries series)
                    {
                        series.Updated += OnSeriesUpdated;
                    }
                }
            }
        }
    }
}
