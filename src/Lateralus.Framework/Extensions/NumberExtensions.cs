namespace Lateralus.Framework;

public static class NumberExtensions
{
    [Pure]
    public static decimal MakeSameSignAs(this decimal number, decimal sign) => Math.Abs(number) * Math.Sign(sign);

    [Pure]
    public static int MakeSameSignAs(this int number, int sign) => Math.Abs(number) * Math.Sign(sign);

    [Pure]
    public static long MakeSameSignAs(this long number, long sign) => Math.Abs(number) * Math.Sign(sign);

    [Pure]
    public static float MakeSameSignAs(this float number, float sign) => MathF.CopySign(number, sign);

    [Pure]
    public static double MakeSameSignAs(this double number, double sign) => Math.CopySign(number, sign);

    [Pure]
    public static string ToEnglishOrdinal(int num) => ToEnglishOrdinal(num, CultureInfo.CurrentCulture);

    [Pure]
    public static string ToEnglishOrdinal(int num, IFormatProvider formatProvider)
    {
        if (num <= 0)
            return num.ToString(formatProvider);

        return (num % 100) switch
        {
            11 or 12 or 13 => string.Format(formatProvider, "{0}th", num),
            _ => (num % 10) switch
            {
                1 => string.Format(formatProvider, "{0}st", num),
                2 => string.Format(formatProvider, "{0}nd", num),
                3 => string.Format(formatProvider, "{0}rd", num),
                _ => string.Format(formatProvider, "{0}th", num),
            },
        };
    }

    [Pure]
    public static string ToFrenchOrdinal(int num) => ToFrenchOrdinal(num, CultureInfo.CurrentCulture);

    [Pure]
    public static string ToFrenchOrdinal(int num, IFormatProvider formatProvider)
    {
        if (num <= 0)
            return num.ToString(formatProvider);

        return num switch
        {
            1 => string.Format(formatProvider, "{0}er", num),
            _ => string.Format(formatProvider, "{0}e", num),
        };
    }

    [Pure]
    public static string ToStringInvariant(this byte number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this byte number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this sbyte number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this sbyte number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this short number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this short number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this ushort number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this ushort number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this int number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this int number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this uint number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this uint number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this long number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this long number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this ulong number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this ulong number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this Half number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this Half number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this float number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this float number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this double number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this double number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }

    [Pure]
    public static string ToStringInvariant(this decimal number) => ToStringInvariant(number, format: null);

    [Pure]
    public static string ToStringInvariant(this decimal number, string? format)
    {
        if (format != null)
            return number.ToString(format, CultureInfo.InvariantCulture);

        return number.ToString(CultureInfo.InvariantCulture);
    }
}
