using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class Int32Converter : HumanReadableConverter<int>
{
    protected override void WriteValue(HumanReadableTextWriter writer, int value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}

