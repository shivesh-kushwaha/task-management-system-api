using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class WorkItemController(
    IMapper mapper,
    IMediator mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddWorkItem([FromBody] AddWorkItemDto request)
    {
        var command = mapper.Map<AddWorkItemCommand>(request);
        await mediator.Send(command);
        return Ok();
    }
}
