using System.Globalization;

namespace Mtf.Maui.Controls.Converters;

public class IsNullOrEmptyListConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is not IEnumerable<object> list || !list.Any();

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
