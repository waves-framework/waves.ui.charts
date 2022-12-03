// using System.Collections.ObjectModel;
// using System.Collections.Specialized;
// using Avalonia;
// using Avalonia.Styling;
// using Waves.UI.Charts.Drawing.Primitives;
// using Waves.UI.Charts.Drawing.Primitives.Interfaces;
// using Waves.UI.Charts.Series.Interfaces;
//
// namespace Waves.UI.Avalonia.Charts.Controls;
//
// /// <summary>
// /// Waves series chart abstraction.
// /// </summary>
// /// <typeparam name="T1">Type of series.</typeparam>
// /// <typeparam name="T2">Type of series data..</typeparam>
// public abstract class WavesSeriesChart<T1, T2> :
//     WavesChart, IStyleable
//     where T1 : IWavesSeries<T2>
//     where T2 : struct
// {
//     /// <summary>
//     ///     Defines <see cref="Series" /> property.
//     /// </summary>
//     public static readonly AttachedProperty<ObservableCollection<T1>> SeriesProperty =
//         AvaloniaProperty.RegisterAttached<WavesSeriesChart<T1, T2>, WavesSeriesChart<T1, T2>, ObservableCollection<T1>>(
//             nameof(Series),
//             new ObservableCollection<T1>(),
//             true);
//
//     private readonly object _seriesLocker = new ();
//
//     /// <summary>
//     ///     Creates new instance of <see cref="WavesCandleSeriesChart" />.
//     /// </summary>
//     public WavesSeriesChart()
//     {
//         AffectsRender<WavesSeriesChart<T1, T2>>(SeriesProperty);
//         SeriesProperty.Changed.Subscribe(OnSeriesChanged);
//     }
//
//     /// <summary>
//     ///     Gets or sets waves point series.
//     /// </summary>
//     public ObservableCollection<T1> Series
//     {
//         get => GetValue(SeriesProperty);
//         set => SetValue(SeriesProperty, value);
//     }
//
//     /// <inheritdoc />
//     Type IStyleable.StyleKey => typeof(WavesChart);
//
//     /// <summary>
//     /// Drawing objects cache.
//     /// </summary>
//     protected List<IWavesDrawingObject> DrawingObjectsCache { get; } = new ();
//
//     /// <summary>
//     ///     Adds series.
//     /// </summary>
//     /// <param name="series">Series.</param>
//     public void AddSeries(T1 series)
//     {
//         lock (_seriesLocker)
//         {
//             series.Updated += OnSeriesUpdated;
//             Series.Add(series);
//         }
//
//         InvalidateVisual();
//     }
//
//     /// <summary>
//     ///     Update series.
//     /// </summary>
//     /// <param name="index">Series index.</param>
//     /// <param name="data">New data.</param>
//     public void UpdateSeries(int index, T2[] data)
//     {
//         if (index >= Series.Count)
//         {
//             throw new Exception("Series index is not correct");
//         }
//
//         Series[index].Update(data);
//
//         InvalidateVisual();
//     }
//
//     /// <summary>
//     ///     Removes series.
//     /// </summary>
//     /// <param name="index">Series index.</param>
//     public void RemoveSeries(int index)
//     {
//         if (index >= Series.Count)
//         {
//             return;
//         }
//
//         lock (_seriesLocker)
//         {
//             var series = Series[index];
//             series.Updated -= OnSeriesUpdated;
//             Series.Remove(series);
//         }
//
//         InvalidateVisual();
//     }
//
//     /// <summary>
//     ///     Clears cache objects.
//     /// </summary>
//     protected void ClearCacheObjects()
//     {
//         foreach (var obj in DrawingObjectsCache)
//         {
//             DrawingObjects?.Remove(obj);
//         }
//
//         DrawingObjectsCache.Clear();
//     }
//
//     /// <summary>
//     /// Callback when series changed.
//     /// </summary>
//     /// <param name="obj">Obj.</param>
//     protected void OnSeriesChanged(AvaloniaPropertyChangedEventArgs<ObservableCollection<T1>> obj)
//     {
//         var series = obj.NewValue.Value;
//
//         if (Series != null)
//         {
//             foreach (var item in Series)
//             {
//                 item.Updated -= OnSeriesUpdated;
//             }
//
//             Series.CollectionChanged -= OnCollectionChanged;
//         }
//
//         Series = series;
//
//         if (Series != null)
//         {
//             foreach (var item in Series)
//             {
//                 item.Updated += OnSeriesUpdated;
//             }
//
//             Series.CollectionChanged += OnCollectionChanged;
//         }
//     }
//
//     /// <summary>
//     /// Callback when collection changed.
//     /// </summary>
//     /// <param name="sender">Sender.</param>
//     /// <param name="e">Args.</param>
//     private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
//     {
//         if (e.Action == NotifyCollectionChangedAction.Remove)
//         {
//             if (e.OldItems != null)
//             {
//                 foreach (var item in e.OldItems)
//                 {
//                     if (item is T1 series)
//                     {
//                         series.Updated -= OnSeriesUpdated;
//                     }
//                 }
//             }
//         }
//
//         if (e.Action == NotifyCollectionChangedAction.Add)
//         {
//             if (e.NewItems != null)
//             {
//                 foreach (var item in e.NewItems)
//                 {
//                     if (item is T1 series)
//                     {
//                         series.Updated += OnSeriesUpdated;
//                     }
//                 }
//             }
//         }
//     }
//
//     /// <summary>
//     /// On series updated callback.
//     /// </summary>
//     /// <param name="sender">Sender.</param>
//     /// <param name="e">Arguments.</param>
//     private void OnSeriesUpdated(object? sender, EventArgs e)
//     {
//         InvalidateVisual();
//     }
// }
