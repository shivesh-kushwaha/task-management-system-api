using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.DeleteWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.UpdateWorkItem;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemById;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemListItem;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.WorkItem.AddWorkItem;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Dtos.WorkItem.UpdateWorkItem;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class WorkItemController(
    IMapper mapper,
    IMediator mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    [HttpGet("select-list-item")]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkItemListItem()
    {
        return Ok(await mediator.Send(new GetWorkItemListItemQuery()));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateWorkItem([FromBody] UpdateWorkItemDto request)
    {
        var command = mapper.Map<UpdateWorkItemCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteWorkItem([FromRoute] int id)
    {
        await mediator.Send(new DeleteWorkItemCommand { Id = id });
        return Ok();
    }
}
