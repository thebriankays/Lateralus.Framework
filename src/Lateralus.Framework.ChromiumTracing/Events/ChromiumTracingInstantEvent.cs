using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingInstantEvent : ChromiumTracingEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "i";

    [JsonPropertyName("s")]
    [JsonConverter(typeof(ChromiumTracingInstantEventScopeJsonConverter))]
    public ChromiumTracingInstantEventScope? Scope { get; set; }
}
