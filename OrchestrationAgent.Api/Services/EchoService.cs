using OrchestrationAgent.Api.Interfaces;

namespace OrchestrationAgent.Api.Services;

public class EchoService : IEchoService
{
    public string EchoBackInReverse(string message) => new string(message.Reverse().ToArray());
}
