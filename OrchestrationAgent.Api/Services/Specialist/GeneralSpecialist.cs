using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Interfaces.Specialist;
using OrchestrationAgent.Api.Models;
using OrchestrationAgent.Api.Schema;

namespace OrchestrationAgent.Api.Services.Specialist;

public class GeneralSpecialist(IOllamaChatService ollamaChatService) : IGeneralSpecialist
{
    public async Task<QueryResponse> RunGeneralSpecialistAsync(string question)
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
                    Content = "You are a general knowledge specialist. You are an expert in science, history, geography, technology, and everyday questions. Answer clearly and thoroughly from your own knowledge.",
                },
                new OllamaMessage
                {
                    Role = "user",
                    Content = question,
                }
            ]
        };        

        var generalResponse = await ollamaChatService.ChatAsync(conversation);
        if (!generalResponse.IsSuccess)
        {
            return new QueryResponse(false, $"General specialist failed: {generalResponse.ErrorMessage}");
        }

        return new QueryResponse(true, generalResponse.Result?["message"]?["content"]?.GetValue<string>() ?? "No response.");
    }
}
