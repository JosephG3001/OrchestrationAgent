using System.Text.Json.Serialization;

namespace OrchestrationAgent.Api.Schema;

public class OllamaFunction
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("parameters")]
    public object Parameters { get; init; }
}
