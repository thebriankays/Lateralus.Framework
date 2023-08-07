using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public abstract class ChromiumTracingObjectEvent : ChromiumTracingEvent
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
