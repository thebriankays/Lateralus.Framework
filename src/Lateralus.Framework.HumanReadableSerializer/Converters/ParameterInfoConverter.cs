using System.Reflection;
using System.Text;
using Lateralus.Framework.HumanReadable.Utils;

namespace Lateralus.Framework.HumanReadable.Converters;

internal sealed class ParameterInfoConverter : HumanReadableConverter<ParameterInfo>
{
    protected override void WriteValue(HumanReadableTextWriter writer, ParameterInfo value, HumanReadableSerializerOptions options)
    {
        var sb = new StringBuilder();
        TypeUtils.GetHumanDisplayName(sb, value);
        writer.WriteValue(sb.ToString());
    }
}
