using System.Diagnostics;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class StringWriterConverter : HumanReadableConverter<StringWriter>
{
    protected override void WriteValue(HumanReadableTextWriter writer, StringWriter? value, HumanReadableSerializerOptions options)
    {
        Debug.Assert(value != null);
        writer.WriteValue(value.ToString());
    }
}

