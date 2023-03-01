using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using ReactiveUI;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series;
using Waves.UI.Charts.Series.Interfaces;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves series chart abstraction.
/// </summary>
public class Waves2DSeriesChart :
    WavesChart,
    IStyleable
{
    /// <summary>
    ///     Defines <see cref="Series" /> property.
    /// </summary>
    public static readonly AttachedProperty<ObservableCollection<IWaves2DSeries>> SeriesProperty =
        AvaloniaProperty.RegisterAttached<Waves2DSeriesChart, Waves2DSeriesChart, ObservableCollection<IWaves2DSeries>>(
            nameof(Series),
            new ObservableCollection<IWaves2DSeries>(),
            true);

    private readonly object _seriesLocker = new ();
    private readonly Dictionary<IWaves2DSeries, IDisposable> _disposables = new ();

    /// <summary>
    ///     Creates new instance of <see cref="Waves2DSeriesChart" />.
    /// </summary>
    public Waves2DSeriesChart()
    {
        AffectsRender<Waves2DSeriesChart>(SeriesProperty);
        SeriesProperty.Changed.Subscribe(OnSeriesChanged);
    }

    /// <summary>
    ///     Gets or sets waves point series.
    /// </summary>
    public ObservableCollection<IWaves2DSeries> Series
    {
        get => GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);

    /// <summary>
    /// Drawing objects cache.
    /// </summary>
    protected List<IWavesDrawingObject> DrawingObjectsCache { get; } = new ();

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        PrepareBackground();
        ClearCacheObjects();
        PrepareGrid();

        if (Series != null)
        {
            foreach (var series in Series)
            {
                series.Draw(this);
            }
        }

        if (!DrawingObjects.Any())
        {
            return;
        }

        RenderUpdate(context);
    }

    /// <summary>
    ///     Clears cache objects.
    /// </summary>
    protected void ClearCacheObjects()
    {
        foreach (var obj in DrawingObjectsCache)
        {
            DrawingObjects?.Remove(obj);
        }

        DrawingObjectsCache.Clear();
    }

    /// <summary>
    /// Callback when series changed.
    /// </summary>
    /// <param name="obj">Obj.</param>
    protected void OnSeriesChanged(AvaloniaPropertyChangedEventArgs<ObservableCollection<IWaves2DSeries>> obj)
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
                    if (item is IWaves2DSeries series)
                    {
                        series.Dispose();
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
                    if (item is IWaves2DSeries series)
                    {
                        series.Updated += OnSeriesUpdated;
                    }
                }
            }
        }

        InvalidateVisual();
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
}
