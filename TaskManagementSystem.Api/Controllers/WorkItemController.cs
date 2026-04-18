using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.Dtos;
using TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemById;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;

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

    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetWorkItemPagedListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedList([FromQuery] WorkItemPagedListRequestDto request)
    {
        var query = mapper.Map<GetWorkItemPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetWorkItemPagedListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return Ok(await mediator.Send(new GetWorkItemByIdQuery { Id = id }));
    }
}
