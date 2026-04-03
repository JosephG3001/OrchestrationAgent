using OrchestrationAgent.Api.Models;
using OrchestrationAgent.Api.Schema;

namespace OrchestrationAgent.Api.Interfaces;

public interface IOllamaChatService
{
    Task<ChatResponse> ChatAsync(OllamaConversation ollamaConversation);
}
