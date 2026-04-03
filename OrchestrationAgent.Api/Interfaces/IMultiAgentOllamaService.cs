using OrchestrationAgent.Api.Models;

namespace OrchestrationAgent.Api.Interfaces;

public interface IMultiAgentOllamaService
{
    Task<QueryResponse> QueryAsync(string question, CancellationToken cancellationToken = default);
}
