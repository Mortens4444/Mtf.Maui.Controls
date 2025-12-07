using System.Text.RegularExpressions;

namespace Mtf.Maui.Controls.Extensions;
public static partial class StringExtensions
{
    public static string ToImageName(this string name)
    {
        var imageName = FindCommas().Replace(name, String.Empty);
        imageName = FindWhiteSpaces().Replace(imageName, "_");
        return imageName.ToLowerInvariant();
    }

    [GeneratedRegex("[,]")]
    private static partial Regex FindCommas();

    [GeneratedRegex("\\s+")]
    private static partial Regex FindWhiteSpaces();
}
