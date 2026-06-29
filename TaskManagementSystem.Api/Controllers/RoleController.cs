using TaskManagementSystem.Application.Commands.Role.UpsertRole;
using TaskManagementSystem.Application.Queries.Role.GetRoleListItem;
using TaskManagementSystem.Application.Queries.Role.GetRolePagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;
using TaskManagementSystem.Core.Dtos.Role.UpsertRole;

namespace TaskManagementSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class RoleController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UpsertRoleDto request)
    {
        var command = mapper.Map<UpsertRoleCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetRolePagedListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedListRequestDto request)
    {
        var query = mapper.Map<GetRolePagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [ProducesResponseType(typeof(List<SelectListItemDto>), StatusCodes.Status200OK)]
    [HttpGet("select-list-item")]
    public async Task<IActionResult> GetListItem()
    {
        return Ok(await mediator.Send(new GetRoleListItemQuery()));
    }
}
