using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace OrchestrationAgent.Api.Schema;

public class OllamaMessage
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; }

    [JsonPropertyName("tool_calls")]
    public JsonArray ToolCalls { get; init; }
}
