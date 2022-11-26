using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mime;
using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Series.Enums;
using Waves.UI.Charts.Series.Interfaces;
using Waves.UI.Charts.Utils;

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
    /// <param name="point">New points.</param>
    /// <param name="description">New description.</param>
    public void UpdateSeries(int index, WavesPoint[] point, string[] description = null)
    {
        if (index >= Series.Count)
        {
            throw new Exception("Series index is not correct");
        }

        Series[index].Update(point, description);

        InvalidateVisual();
    }

    /// <summary>
    ///     Update series.
    /// </summary>
    /// <param name="index">Series index.</param>
    /// <param name="x">New X values.</param>
    /// <param name="y">New Y values.</param>
    /// <param name="description">New description.</param>
    public void UpdateSeries(int index, double[] x, double[] y, string[] description = null)
    {
        if (index >= Series.Count)
        {
            throw new Exception("Series index is not correct");
        }

        Series[index].Update(x, y, description);

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
                    GenerateLineSeries(series);
                    break;
                case WavesPointSeriesType.Bar:
                    GenerateBarSeries(series);
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
    ///     Generates line series.
    /// </summary>
    /// <param name="series">Series.</param>
    private void GenerateLineSeries(IWavesPointSeries series)
    {
        if (series.Points == null)
        {
            return;
        }

        if (series.Points.Length == 0)
        {
            return;
        }

        var visiblePoints = new List<WavesPoint> { new () };
        foreach (var point in series.Points)
        {
            if (point.X < CurrentXMin)
            {
                visiblePoints[0] = point;
                continue;
            }

            if (point.X >= CurrentXMin && point.X <= CurrentXMax)
            {
                visiblePoints.Add(point);
            }
            else if (point.X > CurrentXMax)
            {
                visiblePoints.Add(point);
                break;
            }
        }

        var length = (int)Bounds.Width;
        var points = visiblePoints.Count > length
            ? Resampling.LargestTriangleThreeBucketsDecimation(visiblePoints.ToArray(), length)
            : Resampling.SplineInterpolation(visiblePoints.ToArray(), length);

        for (var i = 0; i < points.Length; i++)
        {
            points[i] = Valuation.NormalizePoint(
                points[i],
                Bounds.Width,
                Bounds.Height,
                CurrentXMin,
                CurrentYMin,
                CurrentXMax,
                CurrentYMax);
        }

        for (var i = 1; i < points.Length; i++)
        {
            var line = new WavesLine
            {
                Color = series.Color,
                IsAntialiased = true,
                IsVisible = true,
                Thickness = 2,
                Point1 = points[i - 1],
                Point2 = points[i],
                Opacity = series.Opacity
            };

            if (visiblePoints.Count > length)
            {
                line.IsAntialiased = false;
            }

            AddDrawingObject(line);
        }

        // if (IsMouseOver)
        // {
        //     var x = Valuation.DenormalizePointX2D(LastMousePosition.X, Width, CurrentXMin, CurrentXMax);
        //     var y = Valuation.DenormalizePointY2D(LastMousePosition.Y, Height, CurrentYMin, CurrentYMax);
        //
        //     var ep = new Point();
        //     var ed = new Point();
        //
        //     for (var i = 0; i < dataSet.Data.Length - 1; i++)
        //         if (x > dataSet.Data[i].X && x < dataSet.Data[i + 1].X)
        //         {
        //             ep = new Point(dataSet.Data[i].X, dataSet.Data[i].Y);
        //             ed = new Point(dataSet.Data[i].X, dataSet.Data[i].Y);
        //         }
        //
        //     ep = Valuation.NormalizePoint(ep, Width, Height, CurrentXMin, CurrentYMin,
        //         CurrentXMax,
        //         CurrentYMax);
        //
        //     var ellipse = new Ellipse
        //     {
        //         Radius = 4,
        //         Fill = dataSet.Color,
        //         Stroke = Background,
        //         StrokeThickness = 1,
        //         Location = ep,
        //         IsVisible = true,
        //         IsAntialiased = true
        //     };
        //
        //     AddTempObject(ellipse);
        //
        //     var paint = new TextPaint
        //     {
        //         TextStyle = TextStyle,
        //         Fill = Foreground,
        //         IsAntialiased = true
        //     };
        //
        //     var value = Math.Round(ed.Y, 2).ToString(CultureInfo.InvariantCulture) + " " + YAxisUnit;
        //
        //     var text = new MediaTypeNames.Text
        //     {
        //         Location = new Point(ep.X, ep.Y),
        //         Style = paint.TextStyle,
        //         Value = value,
        //         IsVisible = true,
        //         IsAntialiased = paint.IsAntialiased,
        //         Stroke = Foreground,
        //         Fill = Foreground
        //     };
        //
        //     var size = DrawingElement.MeasureText(value, paint);
        //
        //     text.Location = new Point(text.Location.X + 12, text.Location.Y + size.Height / 2);
        //
        //     AddTempObject(text);
        // }
    }

    /// <summary>
    ///     Generates bar series.
    /// </summary>
    /// <param name="series">Series.</param>
    private void GenerateBarSeries(IWavesPointSeries series)
    {
        if (series.Points == null)
        {
            return;
        }

        if (series.Points.Length == 0)
        {
            return;
        }

        var visiblePoints = new List<WavesPoint>();
        {
            foreach (var point in series.Points)
            {
                if (point.X < CurrentXMin)
                {
                    if (visiblePoints.Count == 0)
                    {
                        visiblePoints.Add(new WavesPoint());
                    }

                    visiblePoints[0] = point;
                    continue;
                }

                if (point.X >= CurrentXMin && point.X <= CurrentXMax)
                {
                    visiblePoints.Add(point);
                }
                else if (point.X > CurrentXMax)
                {
                    visiblePoints.Add(point);
                    break;
                }
            }
        }

        var length = (int)Bounds.Width;
        var points = visiblePoints.Count > length
                ? Resampling.LargestTriangleThreeBucketsDecimation(visiblePoints.ToArray(), length)
                : Resampling.SplineInterpolation(visiblePoints.ToArray(), length);

        for (var i = 0; i < points.Length; i++)
        {
            points[i] = Valuation.NormalizePoint(
                points[i],
                Bounds.Width,
                Bounds.Height,
                CurrentXMin,
                CurrentYMin,
                CurrentXMax,
                CurrentYMax);
        }

        for (var i = 0; i < points.Length - 1; i++)
        {
            var width = points[i + 1].X - points[i].X;
            var height = Bounds.Height - points[i].Y;

            var rectangle = new WavesRectangle
            {
                Fill = series.Color,
                IsAntialiased = false,
                IsVisible = true,
                StrokeThickness = 2,
                Stroke = GetWavesColor(Background),
                Location = points[i],
                Width = width,
                Height = height,
                Opacity = 0.8f,
            };

            if (visiblePoints.Count > length / 4)
            {
                rectangle.StrokeThickness = 0;
                rectangle.IsAntialiased = false;
            }

            if (visiblePoints.Count <= length / 32)
            {
                // Добавляем подписи на столбцы
                var ep = new Point(points[i].X + (points[i + 1].X - points[i].X) / 2, points[i].Y);
                var value = Valuation.DenormalizePointY2D(points[i].Y, Bounds.Height, CurrentYMin, CurrentYMax);

                var v = Math.Round(value, 2).ToString(CultureInfo.CurrentCulture);

                var text = new WavesText()
                {
                    Color = GetWavesColor(Foreground),
                    Location = new WavesPoint(ep.X, ep.Y),
                    Style = new WavesTextStyle(),
                    Value = v,
                    IsVisible = true,
                    IsAntialiased = true
                };

                var size = Renderer.MeasureText(text);

                text.Location = new WavesPoint(text.Location.X - size.Width / 2, text.Location.Y - 6);

                AddDrawingObject(text);
            }

            AddDrawingObject(rectangle);
        }

        if (points.Length == 0)
        {
            return;
        }

        var lastIndex = points.Length - 1;
        var lastWidth = Width - points[lastIndex].X;
        var lastHeight = Height - points[lastIndex].Y;

        var lastRectangle = new WavesRectangle
        {
            Fill = series.Color,
            IsAntialiased = false,
            IsVisible = true,
            StrokeThickness = 2,
            Stroke = GetWavesColor(Background),
            Location = points[lastIndex],
            Width = lastWidth,
            Height = lastHeight,
            Opacity = 0.8f,
        };

        if (visiblePoints.Count > length / 4)
        {
            lastRectangle.StrokeThickness = 0;
            lastRectangle.IsAntialiased = false;
        }

        if (visiblePoints.Count <= length / 32)
        {
            var ep = new Point(
                points[lastIndex].X + (points[lastIndex].X - points[lastIndex].X) / 2,
                points[lastIndex].Y);
            var value = Valuation.DenormalizePointY2D(
                points[lastIndex].Y,
                Bounds.Height,
                CurrentYMin,
                CurrentYMax);

            var v = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);

            var text = new WavesText
            {
                Color = GetWavesColor(Foreground),
                Location = new WavesPoint(ep.X, ep.Y),
                Style = new WavesTextStyle(),
                Value = v,
                IsVisible = true,
                IsAntialiased = true,
            };

            var size = Renderer.MeasureText(text);

            text.Location = new WavesPoint(text.Location.X - size.Width / 2 + lastWidth / 2, text.Location.Y - 6);

            AddDrawingObject(text);
        }

        AddDrawingObject(lastRectangle);
    }

    /// <summary>
    ///     Adds object.
    /// </summary>
    /// <param name="obj">Drawing object.</param>
    private void AddDrawingObject(IWavesDrawingObject obj)
    {
        _drawingObjectsCache.Add(obj);
        DrawingObjects?.Add(obj);
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
