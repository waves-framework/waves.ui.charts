using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace Waves.UI.Avalonia.Charts.Showcase.Android;

/// <summary>
/// Main activity.
/// </summary>
[Activity(
    Label = "Waves.UI.Avalonia.Charts.Showcase.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
}
