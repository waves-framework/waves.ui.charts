using Avalonia;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using Waves.UI.Avalonia.Charts.Extensions;
using Waves.UI.Charts.Drawing.Interfaces;
using Waves.UI.Charts.Drawing.Primitives;
using Waves.UI.Charts.Drawing.Primitives.Enums;
using Waves.UI.Charts.Drawing.Primitives.Interfaces;
using Waves.UI.Charts.Utils;

namespace Waves.UI.Avalonia.Charts.Controls;

/// <summary>
/// Waves point series chart.
/// </summary>
public class WavesPointSeriesChart : WavesChart, IStyleable
{
    private readonly List<IWavesDrawingObject> _drawingObjectsCache = new ();

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(WavesChart);

    /// <inheritdoc />
    protected override void Refresh(DrawingContext context)
    {
        DrawingObjects?.Clear();
        PrepareChart();
        base.Refresh(context);
    }
}
