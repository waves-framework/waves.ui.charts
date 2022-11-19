using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels;
using Waves.UI.Avalonia.Charts.Showcase.Views;

namespace Waves.UI.Avalonia.Charts.Showcase;

/// <summary>
/// Application.
/// </summary>
public partial class App : Application
{
    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(),
                };
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel(),
                };
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
