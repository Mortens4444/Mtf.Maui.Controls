namespace Mtf.Maui.Controls.Services;

public static class NavigationService
{
    public static async Task<Page> NavigateToPageAsync(Type pageType, object? parameter = null)
    {
        var serviceProvider = Shell.Current?.Handler?.MauiContext?.Services
                              ?? Application.Current?.Handler?.MauiContext?.Services
                              ?? throw new InvalidOperationException("Service provider is not available.");

        var navigation = GetNavigation()
                         ?? throw new InvalidOperationException("Navigation is not available.");

        var dispatcher = Shell.Current?.Dispatcher
                         ?? Application.Current?.Dispatcher
                         ?? throw new InvalidOperationException("Dispatcher is not available.");

        var page = parameter == null ?
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType) :
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType, [parameter]);
        await dispatcher.DispatchAsync(() => navigation.PushAsync(page)).ConfigureAwait(true);

        return page;
    }

    private static INavigation? GetNavigation()
    {
        if (Shell.Current != null)
        {
            return Shell.Current.Navigation;
        }

        var windows = Application.Current?.Windows;
        if (windows == null || windows.Count == 0)
        {
            return null;
        }

        return windows[0].Page?.Navigation;
    }
}
