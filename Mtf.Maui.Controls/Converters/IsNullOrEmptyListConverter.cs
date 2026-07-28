using System.Collections;
using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class IsNullOrEmptyListConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is not IEnumerable list || !list.Cast<object>().Any();

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
