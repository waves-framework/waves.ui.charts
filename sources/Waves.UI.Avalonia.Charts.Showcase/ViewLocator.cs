using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Waves.UI.Avalonia.Charts.Showcase.ViewModels;

namespace Waves.UI.Avalonia.Charts.Showcase;

/// <summary>
///     View locator.
/// </summary>
public class ViewLocator : IDataTemplate
{
    /// <inheritdoc />
    public IControl? Build(object? data)
    {
        if (data is null)
        {
            return null;
        }

        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type) !;
        }

        return new TextBlock { Text = name };
    }

    /// <inheritdoc />
    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
