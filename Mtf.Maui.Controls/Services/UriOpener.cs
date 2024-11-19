namespace Mtf.Maui.Controls.Services;

public static class UriOpener
{
    public static async void OpenUri(string uri) => _ = await Launcher.OpenAsync(new Uri(uri)).ConfigureAwait(false);
}
