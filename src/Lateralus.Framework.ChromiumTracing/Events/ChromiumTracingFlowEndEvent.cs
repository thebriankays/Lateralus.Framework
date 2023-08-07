using System.Text.Json.Serialization;

namespace Lateralus.Framework.ChromiumTracing;

public sealed class ChromiumTracingFlowEndEvent : ChromiumTracingFlowEvent
{
    [JsonPropertyName("ph")]
    public override string Type => "f";

    [JsonPropertyName("bp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(BindingPointJsonConverter))]
    public BindingPoint BindingPoint { get; set; }
}
