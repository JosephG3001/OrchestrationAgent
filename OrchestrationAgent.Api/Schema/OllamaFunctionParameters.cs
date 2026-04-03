using System.Text.Json.Serialization;

namespace OrchestrationAgent.Api.Schema;

public class OllamaFunctionParameters
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("properties")]
    public object Properties { get; init; }

    [JsonPropertyName("required")]
    public string[] Required { get; init; }
}
