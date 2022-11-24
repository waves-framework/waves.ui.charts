using System.Globalization;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls.Shapes;
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
    public static readonly StyledProperty<IList<IWavesPointSeries>> SeriesProperty =
        AvaloniaProperty.Register<WavesPointSeriesChart, IList<IWavesPointSeries>>(
            nameof(Series),
            new List<IWavesPointSeries>());

    private readonly object _seriesLocker = new ();
    private readonly List<IWavesDrawingObject> _drawingObjectsCache = new ();

    /// <summary>
    ///     Creates new instance of <see cref="WavesChart" />.
    /// </summary>
    public WavesPointSeriesChart()
    {
        AffectsRender<WavesPointSeriesChart>(SeriesProperty);
    }

    /// <summary>
    ///     Gets or sets waves point series.
    /// </summary>
    public IList<IWavesPointSeries> Series
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
            Series.RemoveAt(index);
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
}
