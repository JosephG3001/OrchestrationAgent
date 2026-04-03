using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Interfaces.Specialist;
using OrchestrationAgent.Api.Services;
using OrchestrationAgent.Api.Services.Specialist;

namespace OrchestrationAgent.Api.Helpers;

public static class ServiceRegistrationHandler
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IEchoService, EchoService>();

        services.AddHttpClient<IOllamaChatService, OllamaChatService>(client =>
            client.BaseAddress = new Uri("http://127.0.0.1:11434"));

        services.AddTransient<IGeneralSpecialist, GeneralSpecialist>();
        services.AddTransient<IEchoSpecialist, EchoSpecialist>();

        services.AddTransient<IMultiAgentOllamaService, MultiAgentOllamaService>();
    }
}
