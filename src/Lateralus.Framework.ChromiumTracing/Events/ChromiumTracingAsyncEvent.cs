using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public abstract class ChromiumTracingAsyncEvent : ChromiumTracingEvent
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
