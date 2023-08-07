﻿using System.Diagnostics;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class StringConverter : HumanReadableConverter<string>
{
    protected override void WriteValue(HumanReadableTextWriter writer, string? value, HumanReadableSerializerOptions options)
    {
        Debug.Assert(value != null);

        writer.WriteValue(value);
    }
}

