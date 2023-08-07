namespace Lateralus.Framework;

public static class FormattableExtensions
{
    public static string ToStringInvariant<T>(this T value) where T : IFormattable => ToStringInvariant(value, format: null);

    public static string ToStringInvariant<T>(this T value, string? format) where T : IFormattable
    {
        if (value == null)
            return "";

        return value.ToString(format, CultureInfo.InvariantCulture) ?? "";
    }
}
