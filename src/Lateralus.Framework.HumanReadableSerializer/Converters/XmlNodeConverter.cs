﻿using System.Xml;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class XmlNodeConverter : HumanReadableConverter
{
    public override bool CanConvert(Type type) => typeof(XmlNode).IsAssignableFrom(type);

    public override void WriteValue(HumanReadableTextWriter writer, object? value, HumanReadableSerializerOptions options)
    {
        var xml = (XmlNode)value!;
        writer.WriteValue(options.FormatValue("application/xml", xml.OuterXml));
    }
}
