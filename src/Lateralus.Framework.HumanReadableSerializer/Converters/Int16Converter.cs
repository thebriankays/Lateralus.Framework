﻿using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class Int16Converter : HumanReadableConverter<short>
{
    protected override void WriteValue(HumanReadableTextWriter writer, short value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
