using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingMemoryDumpProcessEvent : ChromiumTracingMemoryDumpEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "v";
}
