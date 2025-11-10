using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class BoolToLikeTextConverter : IValueConverter
{
    private const string Liked = "";
    private const string LikeMe = "👍";

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool boolValue)
        {
            return String.Empty;
        }

        return boolValue ? Liked : LikeMe;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
