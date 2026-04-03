using OrchestrationAgent.Api.Models;

namespace OrchestrationAgent.Api.Interfaces.Specialist;

public interface IEchoSpecialist
{
    Task<QueryResponse> RunEchoSpecialistAsync(string question);
}
