﻿using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class RegexConverter : HumanReadableConverter<Regex>
{
    protected override void WriteValue(HumanReadableTextWriter writer, Regex? value, HumanReadableSerializerOptions options)
    {
        Debug.Assert(value != null);

        writer.StartObject();
        writer.WritePropertyName("Pattern");
        writer.WriteValue(value.ToString());

        writer.WritePropertyName(nameof(Regex.Options));
        writer.WriteValue(value.Options.ToString());

        if (value.MatchTimeout != Timeout.InfiniteTimeSpan)
        {
            writer.WritePropertyName(nameof(Regex.MatchTimeout));
            TimeSpanConverter.Write(writer, value.MatchTimeout);
        }

        writer.EndObject();
    }
}
