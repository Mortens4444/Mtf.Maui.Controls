using System.Reflection;

namespace Mtf.Maui.Controls.Extensions;

public static class ContentPageExtensions
{
    public static Task<bool> ShowPageAsync(this ContentPage contentPage, Type itemType, Type pageType, object bindingContext)
    {
        var genericAsyncMethod = typeof(ContentPageExtensions).GetMethod(nameof(CreatePageWithTypeAsync), BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(itemType);
        return (Task<bool>)genericAsyncMethod.Invoke(null, [contentPage, bindingContext, pageType]);
    }

    private static async Task<bool> CreatePageWithTypeAsync<T>(ContentPage contentPage, T matchingItem, Type type)
    {
        if (!Equals(matchingItem, default(T)))
        {
            var pageInstance = Activator.CreateInstance(type, matchingItem);
            if (pageInstance is Page page)
            {
                await contentPage.Navigation.PushAsync(page).ConfigureAwait(true);
            }
            return true;
        }

        return false;
    }
}
