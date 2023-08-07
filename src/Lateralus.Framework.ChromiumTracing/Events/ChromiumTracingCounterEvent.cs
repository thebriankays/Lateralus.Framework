using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingCounterEvent : ChromiumTracingEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "C";

    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
