﻿using System.Globalization;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class SingleConverter : HumanReadableConverter<float>
{
    protected override void WriteValue(HumanReadableTextWriter writer, float value, HumanReadableSerializerOptions options)
    {
        writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
    }
}

