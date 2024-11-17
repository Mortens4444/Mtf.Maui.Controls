namespace Mtf.Maui.Controls.Services;

public static class NavigationService
{
    //public static async Task NavigateToPageAsync<TPage>(object? parameter = null)
    //    where TPage : Page
    //{
    //    await NavigateToPageAsync(typeof(TPage), parameter);
    //}

    public static async Task<Page> NavigateToPageAsync(Type pageType, object? parameter = null)
    {
        var serviceProvider = Application.Current?.Handler?.MauiContext?.Services
                              ?? throw new InvalidOperationException("Service provider is not available.");

        var page = parameter == null ?
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType) :
            (Page)ActivatorUtilities.CreateInstance(serviceProvider, pageType, [parameter]);
        await Application.Current!.MainPage!.Navigation.PushAsync(page).ConfigureAwait(true);

        return page;
    }
}
