using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingAsyncInstantEvent : ChromiumTracingAsyncEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "n";
}
