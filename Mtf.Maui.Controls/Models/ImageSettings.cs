namespace Mtf.Maui.Controls.Models;

public static class ImageSettings
{
    public static bool UseOfflineImages { get; set; } = true;

    public static string ImagesUrl { get; set; } = String.Empty;

    public static int NumberOfDaysToCacheImages { get; set; }
}