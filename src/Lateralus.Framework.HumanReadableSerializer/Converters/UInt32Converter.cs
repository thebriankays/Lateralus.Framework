﻿using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class UInt32Converter : HumanReadableConverter<uint>
{
    protected override void WriteValue(HumanReadableTextWriter writer, uint value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}

