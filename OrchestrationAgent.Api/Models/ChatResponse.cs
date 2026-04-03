using System.Text.Json.Nodes;

namespace OrchestrationAgent.Api.Models;

public class ChatResponse
{
    public bool IsSuccess;
    public string ErrorMessage;
    public JsonObject Result;

    private ChatResponse() { }

    public static ChatResponse Success(JsonObject result) => new ChatResponse { IsSuccess = true, Result = result };
    public static ChatResponse Failure(string errorMessage) => new ChatResponse { IsSuccess = false, ErrorMessage = errorMessage };
}
