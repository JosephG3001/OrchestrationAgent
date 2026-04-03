using OrchestrationAgent.Api.Models;

namespace OrchestrationAgent.Api.Interfaces.Specialist;

public interface IGeneralSpecialist
{
    Task<QueryResponse> RunGeneralSpecialistAsync(string question);
}
