namespace Mtf.Maui.Controls.Services;

public static class UriOpener
{
    public static Task OpenUriAsync(Uri uri) => Launcher.OpenAsync(uri);
}
