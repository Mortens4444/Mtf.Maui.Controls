using System.Globalization;
using System.Net;

namespace Mtf.Maui.Controls.Converters;

public class StatusCodeToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.OK ? Colors.Green : Colors.Red;
        }

        return Colors.White;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
