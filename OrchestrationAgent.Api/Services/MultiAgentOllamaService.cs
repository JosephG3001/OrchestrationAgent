using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Interfaces.Specialist;
using OrchestrationAgent.Api.Models;
using OrchestrationAgent.Api.Schema;
using OrchestrationAgent.Api.Services.Specialist;

namespace OrchestrationAgent.Api.Services;

public class MultiAgentOllamaService(
    IOllamaChatService ollamaChatService,
    IEchoSpecialist echoSpecialist,
    IGeneralSpecialist generalSpecialist) : IMultiAgentOllamaService
{
    public async Task<QueryResponse> QueryAsync(string question, CancellationToken cancellationToken = default)
    {
        var conversation = new OllamaConversation
        {
            Model = "qwen2.5:3b",
            Stream = false,
            Messages = 
            [
                new OllamaMessage
                {
                    Role = "system",
                    Content = $"You are an orchestrator. Decide which specialist should handle the user's question. " +
                              $"Reply with exactly one of these words and nothing else: {nameof(EchoSpecialist)} or {nameof(GeneralSpecialist)}. " +
                              $"{nameof(EchoSpecialist)}: handles testing and debugging questions using the echo tool. " +
                              $"{nameof(GeneralSpecialist)}: handles all other questions."
                },
                new OllamaMessage
                {
                    Role = "user",
                    Content = question
                }
            ]
        };

        var orchestratorResponse = await ollamaChatService.ChatAsync(conversation);
        if (!orchestratorResponse.IsSuccess)
        {
            return new QueryResponse(false, $"Orchestrator failed: {orchestratorResponse.ErrorMessage}");
        }

        var routingDecision = orchestratorResponse.Result?["message"]?["content"]?.GetValue<string>()?.Trim() ?? string.Empty;
        switch (routingDecision)
        {
            case nameof(EchoSpecialist):
                return await echoSpecialist.RunEchoSpecialistAsync(question);

            case nameof(GeneralSpecialist):
                return await generalSpecialist.RunGeneralSpecialistAsync(question);

            default:
                return new QueryResponse(false, $"Orchestrator returned an invalid routing decision: '{routingDecision}'");
        }
    }
}
