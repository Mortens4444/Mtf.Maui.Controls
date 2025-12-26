using Mtf.Maui.Controls.Extensions;

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
                await ex.ShowErrorAsync().ConfigureAwait(false);
            }
        }).ConfigureAwait(true);

        return page;
    }
}
