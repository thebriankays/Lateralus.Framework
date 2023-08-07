using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingMarkEvent : ChromiumTracingEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "R";
}
