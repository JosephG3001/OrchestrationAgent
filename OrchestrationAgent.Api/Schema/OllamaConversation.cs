using OrchestrationAgent.Api.Models;
using System.Text.Json.Serialization;

namespace OrchestrationAgent.Api.Schema;

public class OllamaConversation
{
    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("messages")]
    public IList<OllamaMessage> Messages { get; init; }

    [JsonPropertyName("tools")]
    public IList<OllamaTool> Tools { get; init; }

    [JsonPropertyName("think")]
    public bool Think { get; init; }

    [JsonPropertyName("stream")]
    public bool Stream { get; init; }
}
