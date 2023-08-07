﻿using System.Text.Json;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class JsonDocumentConverter : HumanReadableConverter<JsonDocument>
{
    protected override void WriteValue(HumanReadableTextWriter writer, JsonDocument? value, HumanReadableSerializerOptions options)
    {
        var str = JsonSerializer.Serialize(value, JsonElementConverter.IndentedOptions);
        writer.WriteValue(options.FormatValue("application/json", str));
    }
}
