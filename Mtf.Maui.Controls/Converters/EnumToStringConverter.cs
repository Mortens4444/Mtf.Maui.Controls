using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class EnumToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string enumString && Enum.IsDefined(targetType, enumString))
        {
            return Enum.Parse(targetType, enumString);
        }

        return null;
    }
}