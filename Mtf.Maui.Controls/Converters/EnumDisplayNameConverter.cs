using System.Globalization;
using System.Text;

namespace Mtf.Maui.Controls.Converters;

internal class EnumDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var result = new StringBuilder();
        var text = value?.ToString() ?? String.Empty;
        foreach (var ch in text)
        {
            if (Char.IsUpper(ch) && result.Length > 0)
            {
                result.Append(' ');
            }

            result.Append(ch);
        }
        return result.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value;
}