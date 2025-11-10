namespace Mtf.Maui.Controls.Services;

public static class UriOpener
{
    public static Task OpenUriAsync(string uri) => Launcher.OpenAsync(new Uri(uri));
}
