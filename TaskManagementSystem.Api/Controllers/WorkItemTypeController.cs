using TaskManagementSystem.Application.Queries.WorkItemType.GetWorkItemTypeListItem;
using TaskManagementSystem.Core.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class WorkItemTypeController(
    IMapper mapper,
    IMediator mediator) : BaseController
{
    [HttpGet("select-list-item")]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkItemListItem()
    {
        return Ok(await mediator.Send(new GetWorkItemTypeListItemQuery()));
    }
}
