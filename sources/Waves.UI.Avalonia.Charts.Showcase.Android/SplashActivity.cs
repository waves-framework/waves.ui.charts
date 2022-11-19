using Android.App;
using Android.Content;
using Android.OS;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using Application = Android.App.Application;

namespace Waves.UI.Avalonia.Charts.Showcase.Android;

/// <summary>
/// Splash activity.
/// </summary>
[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
public class SplashActivity : AvaloniaSplashActivity<App>
{
    /// <inheritdoc />
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .UseReactiveUI();
    }

    /// <inheritdoc />
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
    }

    /// <inheritdoc />
    protected override void OnResume()
    {
        base.OnResume();

        StartActivity(new Intent(Application.Context, typeof(MainActivity)));
    }
}
