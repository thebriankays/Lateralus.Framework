using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class SByteConverter : HumanReadableConverter<sbyte>
{
    protected override void WriteValue(HumanReadableTextWriter writer, sbyte value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
