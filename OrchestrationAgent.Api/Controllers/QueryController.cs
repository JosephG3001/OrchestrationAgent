using Microsoft.AspNetCore.Mvc;
using OrchestrationAgent.Api.Interfaces;
using OrchestrationAgent.Api.Models;

namespace OrchestrationAgent.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController(IMultiAgentOllamaService multiAgentOllamaService) : ControllerBase
{
    [HttpPost()]
    public async Task<IActionResult> Query([FromBody] QueryRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest();
        }

        var response = await multiAgentOllamaService.QueryAsync(request.Question, cancellationToken);
        return Ok(response);
    }
}
