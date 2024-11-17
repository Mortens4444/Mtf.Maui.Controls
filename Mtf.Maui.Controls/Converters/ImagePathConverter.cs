using Mtf.Maui.Controls.Models;
using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class ImagePathConverter : IValueConverter
{
    private const string ImageExtension = ".png";
    private const string RelativeDirectoryPath = "../../../../../Resources/Images";

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is List<string> stringList
            ? parameter is int fileIndex ? GetImagePath(stringList, fileIndex) : (object)GetImagePath(stringList)
            : GetImagePath([(value as string)!]);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();

    public ImageSource GetImagePath(IList<string> relativePaths, int fileIndex = 0)
    {
        ArgumentNullException.ThrowIfNull(relativePaths);

        var name = fileIndex == -1 ? $"Icons/{relativePaths[0]}_30x30" : relativePaths[fileIndex];
        var filename = String.Concat(name, ImageExtension);

        if (fileIndex >= -1 && fileIndex < relativePaths.Count)
        {
            if (ImageSettings.UseOfflineImages)
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RelativeDirectoryPath, filename);
            }

            var uri = String.Concat(ImageSettings.ImagesUrl, filename);
            var cacheValidity = new TimeSpan(ImageSettings.NumberOfDaysToCacheImages, 0, 0, 0);
            return new UriImageSource
            {
                Uri = new Uri(uri),
                CacheValidity = cacheValidity,
                CachingEnabled = true
            };
        }

        throw new ArgumentOutOfRangeException(nameof(fileIndex));
    }
}