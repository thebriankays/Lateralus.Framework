﻿using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class DecimalConverter : HumanReadableConverter<decimal>
{
    protected override void WriteValue(HumanReadableTextWriter writer, decimal value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}

