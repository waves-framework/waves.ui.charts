using System.Runtime.Versioning;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Web;

[assembly: SupportedOSPlatform("browser")]

namespace Waves.UI.Avalonia.Charts.Showcase.Web;

/// <summary>
///     Program.
/// </summary>
internal class Program
{
    /// <summary>
    ///     Builds avalonia app.
    /// </summary>
    /// <returns>App.</returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>();
    }

    /// <summary>
    ///     Main.
    /// </summary>
    /// <param name="args">Args.</param>
    private static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .UseReactiveUI()
            .SetupBrowserApp("out");
    }
}
