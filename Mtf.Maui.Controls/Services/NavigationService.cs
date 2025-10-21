using System.Diagnostics;

namespace Mtf.Maui.Controls.Services;

public static class NavigationService
{
    public static async Task<Page> NavigateToPageAsync(Type pageType, object? parameter = null)
    {
        var serviceProvider = Shell.Current?.Handler?.MauiContext?.Services
                              ?? throw new InvalidOperationException("Service provider is not available.");

        var page = parameter == null ?
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType) :
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType, [parameter]);
        await Shell.Current.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                //await Application.Current!.MainPage!.Navigation.PushAsync(page).ConfigureAwait(true);
                await Shell.Current.Navigation.PushAsync(page).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                var message = $"Navigation error: {ex}";
                Debug.WriteLine(message);
                Console.WriteLine(message);
            }
        }).ConfigureAwait(true);

        return page;
    }
}
