using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Models;
using OrchestrationAgent.Api.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace OrchestrationAgent.Api.Services;

public class OllamaChatService(HttpClient httpClient) : IOllamaChatService
{
    public async Task<ChatResponse> ChatAsync(OllamaConversation ollamaConversation)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/chat", ollamaConversation);
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            return ChatResponse.Success(JsonSerializer.Deserialize<JsonObject>(responseText));
        }
        catch (Exception ex)
        {
            return ChatResponse.Failure($"ChatAsync failed: {ex.Message}");
        }
    }
}
