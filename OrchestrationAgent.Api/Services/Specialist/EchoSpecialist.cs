using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Interfaces.Specialist;
using OrchestrationAgent.Api.Models;
using OrchestrationAgent.Api.Schema;

namespace OrchestrationAgent.Api.Services.Specialist;

public class EchoSpecialist(
    IOllamaChatService ollamaChatService,
    IEchoService echoService) : IEchoSpecialist
{
    private const string EchoTool = nameof(EchoTool);

    private static readonly List<OllamaTool> _echoTools = new List<OllamaTool>();

    static EchoSpecialist()
    {
        var toolsSchema = new Dictionary<string, string>
        {
            { EchoTool, "Echoes back the user's question. Useful for testing and debugging." },
        };

        foreach (var (toolName, description) in toolsSchema)
        {
            var function = new OllamaFunction
            {
                Name = toolName,
                Description = description,
                Parameters = new OllamaFunctionParameters
                {
                    Type = "object",
                    Required = ["question"],
                    Properties = new 
                    {
                        question = new 
                        {
                            type = "string", 
                            description = "The user's question to answer." 
                        }
                    },
                }
            };

            _echoTools.Add(new OllamaTool
            {
                Type = "function",
                Function = function,
            });
        }
    }

    public async Task<QueryResponse> RunEchoSpecialistAsync(string question)
    {
        var conversation = new OllamaConversation
        {
            Model = "qwen2.5:3b",
            Tools = _echoTools.ToArray(),
            Stream = false,
            Messages = 
            [
                new OllamaMessage
                {
                    Role = "system",
                    Content = $"You are an echo specialist. You have access to the {EchoTool} tool. Once you have the result from the tool, return your final answer only."
                },
                new OllamaMessage
                {
                    Role = "user",
                    Content = question
                }
            ]
        };

        var maxIterations = 5;

        for (int i = 0; i < maxIterations; i++)
        {
            var echoResponse = await ollamaChatService.ChatAsync(conversation);
            if (!echoResponse.IsSuccess)
            {
                return new QueryResponse(false, $"Echo specialist failed: {echoResponse.ErrorMessage}");
            }

            var assistantMessage = echoResponse.Result["message"];
            var toolCalls = assistantMessage?["tool_calls"]?.AsArray();

            if (toolCalls == null || toolCalls.Count == 0)
            {
                return new QueryResponse(true, assistantMessage?["content"]?.GetValue<string>() ?? "No response.");
            }

            var requestedTool = toolCalls[0]?["function"]?["name"]?.GetValue<string>();
            switch (requestedTool)
            {
                case EchoTool:
                    var echoToolResult = echoService.EchoBackInReverse(question);
                    conversation.Messages.Add(new OllamaMessage
                    {
                        Role = "tool",
                        Content = echoToolResult
                    });
                    break;

                default:
                    return new QueryResponse(false, $"Echo specialist requested unknown tool: '{requestedTool}'");
            }
        }

        return new QueryResponse(false, "Iteration limit reached. Agent was unable to process the message");
    }
}
