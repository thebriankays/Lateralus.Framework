using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingContextEndEvent : ChromiumTracingContextEvent
{
    [JsonPropertyName("ph")]
    public override string Type => ")";
}
