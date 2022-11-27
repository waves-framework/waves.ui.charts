using UIKit;

namespace Waves.UI.Avalonia.Charts.Showcase.iOS;

/// <summary>
/// Application.
/// </summary>
public class Application
{
    /// <summary>
    /// This is the main entry point of the application.
    /// </summary>
    /// <param name="args">Arguments.</param>
    private static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
