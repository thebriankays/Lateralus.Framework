using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingContextBeginEvent : ChromiumTracingContextEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "(";
}
