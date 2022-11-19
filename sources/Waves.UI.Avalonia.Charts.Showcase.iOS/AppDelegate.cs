using Avalonia;
using Avalonia.iOS;
using Avalonia.ReactiveUI;
using Foundation;

namespace Waves.UI.Avalonia.Charts.Showcase.iOS;

/// <summary>
///     The UIApplicationDelegate for the application. This class is responsible for launching the
///     User Interface of the application, as well as listening (and optionally responding) to
///     application events from iOS.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : AvaloniaAppDelegate<App>
{
    /// <inheritdoc />
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return builder.UseReactiveUI();
    }
}
