using System.Text.Json.Serialization;

namespace OrchestrationAgent.Api.Schema;

public class OllamaTool
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("function")]
    public OllamaFunction Function { get; init; }
}
