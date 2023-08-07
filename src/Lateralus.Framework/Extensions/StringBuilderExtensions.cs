namespace Lateralus.Framework;

public static class StringBuilderExtensions
{
    [Pure]
    public static bool StartsWith(this StringBuilder stringBuilder, char prefix)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);

        if (stringBuilder.Length == 0)
            return false;

        return stringBuilder[0] == prefix;
    }

    [Pure]
    public static bool StartsWith(this StringBuilder stringBuilder, string prefix)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);
        ArgumentNullException.ThrowIfNull(prefix);

        if (stringBuilder.Length < prefix.Length)
            return false;

        for (var i = 0; i < prefix.Length; i++)
        {
            if (stringBuilder[i] != prefix[i])
                return false;
        }

        return true;
    }

    [Pure]
    public static bool EndsWith(this StringBuilder stringBuilder, char suffix)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);

        if (stringBuilder.Length == 0)
            return false;

        return stringBuilder[^1] == suffix;
    }

    [Pure]
    public static bool EndsWith(this StringBuilder stringBuilder, string suffix)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);
        ArgumentNullException.ThrowIfNull(suffix);

        if (stringBuilder.Length < suffix.Length)
            return false;

        for (var index = 0; index < suffix.Length; index++)
        {
            if (stringBuilder[stringBuilder.Length - 1 - index] != suffix[suffix.Length - 1 - index])
                return false;
        }

        return true;
    }

    public static void TrimStart(this StringBuilder stringBuilder, char trimChar)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);

        for (var i = 0; i < stringBuilder.Length; i++)
        {
            if (stringBuilder[i] == trimChar)
                continue;

            if (i > 0)
            {
                stringBuilder.Remove(0, i);
            }

            return;
        }
    }

    public static void TrimEnd(this StringBuilder stringBuilder, char trimChar)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);

        for (var i = stringBuilder.Length - 1; i >= 0; i--)
        {
            if (stringBuilder[i] == trimChar)
                continue;

            if (i != stringBuilder.Length - 1)
            {
                stringBuilder.Remove(i + 1, stringBuilder.Length - i - 1);
            }

            return;
        }
    }

    public static void Trim(this StringBuilder stringBuilder, char trimChar)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);

        TrimEnd(stringBuilder, trimChar);
        TrimStart(stringBuilder, trimChar);
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, byte value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, byte? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, sbyte value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, sbyte? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, short value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, short? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, ushort value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, ushort? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, int value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, int? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, uint value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, uint? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, long value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, long? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, ulong value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, ulong? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

#if NET6_0_OR_GREATER
    public static StringBuilder AppendInvariant(this StringBuilder sb, Half value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, Half? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }
#endif

    public static StringBuilder AppendInvariant(this StringBuilder sb, float value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, float? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, double value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, double? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, decimal value) => sb.Append(value.ToString(CultureInfo.InvariantCulture));

    public static StringBuilder AppendInvariant(this StringBuilder sb, decimal? value)
    {
        if (value != null)
            return sb.Append(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, FormattableString? value)
    {
        if (value != null)
            return sb.Append(value.ToString(CultureInfo.InvariantCulture));

        return sb;
    }

    [SuppressMessage("Performance", "MA0028:Optimize StringBuilder usage", Justification = "Performance")]
    public static StringBuilder AppendInvariant<T>(this StringBuilder sb, T? value)
        where T : IFormattable
    {
        if (value != null)
            return sb.Append(value.ToString(format: null, CultureInfo.InvariantCulture));

        return sb;
    }

    public static StringBuilder AppendInvariant(this StringBuilder sb, object? value)
    {
        if (value != null)
            return sb.AppendFormat(CultureInfo.InvariantCulture, "{0}", value);

        return sb;
    }

    public static StringBuilder AppendFormatInvariant(this StringBuilder sb, string format, object? args0) => sb.AppendFormat(CultureInfo.InvariantCulture, format, args0);

    public static StringBuilder AppendFormatInvariant(this StringBuilder sb, string format, object? args0, object? args1) => sb.AppendFormat(CultureInfo.InvariantCulture, format, args0, args1);

    public static StringBuilder AppendFormatInvariant(this StringBuilder sb, string format, object? args0, object? args1, object? args2) => sb.AppendFormat(CultureInfo.InvariantCulture, format, args0, args1, args2);

    public static StringBuilder AppendFormatInvariant(this StringBuilder sb, string format, params object?[] args) => sb.AppendFormat(CultureInfo.InvariantCulture, format, args);
}
