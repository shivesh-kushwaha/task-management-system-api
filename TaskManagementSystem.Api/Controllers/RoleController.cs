using TaskManagementSystem.Application.Commands.Role.AddRole;
using TaskManagementSystem.Application.Commands.Role.Dtos;
using TaskManagementSystem.Application.Queries.Role.GetRoleListItem;
using TaskManagementSystem.Core.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class RoleController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddRoleDto request)
    {
        var command = mapper.Map<AddRoleCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [ProducesResponseType(typeof(List<SelectListItemDto>), StatusCodes.Status200OK)]
    [HttpGet("select-list-item")]
    public async Task<IActionResult> GetListItem()
    {
        return Ok(await mediator.Send(new GetRoleListItemQuery()));
    }
}
