using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class InverseBooleanConverter : IValueConverter
{
    private const string UnableToConvertMessage = "The value must be a boolean.";

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }

        throw new InvalidOperationException(UnableToConvertMessage);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}
