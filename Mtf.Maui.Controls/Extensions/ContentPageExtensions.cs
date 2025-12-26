using System.Reflection;

namespace Mtf.Maui.Controls.Extensions;

public static class ContentPageExtensions
{
    public static Task<bool> ShowPageAsync(this ContentPage contentPage, Type itemType, Type pageType, object bindingContext)
    {
        var genericAsyncMethod = typeof(ContentPageExtensions).GetMethod(nameof(CreatePageWithTypeAsync), BindingFlags.NonPublic | BindingFlags.Static) ?? throw new InvalidOperationException($"Method '{nameof(CreatePageWithTypeAsync)}' not found.");
        var constructedMethod = genericAsyncMethod.MakeGenericMethod(itemType);
        var result = constructedMethod.Invoke(null, new object[] { contentPage, bindingContext, pageType });
        if (result is Task<bool> taskResult)
        {
            return taskResult;
        }
        throw new InvalidOperationException("Invoked method did not return Task<bool>.");
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
