using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class WorkItemController(
    ILogger<WorkItemController> logger,
    IMapper mapper,
    IMediator mediator): BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddWorkItem([FromBody] AddWorkItemDto request)
    {
        try
        {
            var command = mapper.Map<AddWorkItemCommand>(request);
            await mediator.Send(command);
            return Ok();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Add Work Item.");
            return BadRequest(ex.Message);
        }
    }
}
