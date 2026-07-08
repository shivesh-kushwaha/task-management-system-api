using TaskManagementSystem.Application.Commands.Permission.AddPermission;
using TaskManagementSystem.Application.Queries.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;
using TaskManagementSystem.Core.Dtos.Permission.AddPermission;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PermissionController(IMediator mediator,
    IMapper mapper) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddPermissionDto request)
    {
        var command = mapper.Map<AddPermissionCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetPermissionListByUserIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListByUserId()
    {
        return Ok(await mediator.Send(new GetPermissionListByUserIdQuery()));
    }

    [HttpGet("grouped-list/{roleId:int}")]
    [ProducesResponseType(typeof(List<GetPermissionGroupedListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGroupedList([FromRoute] int roleId)
    {
        return Ok(await mediator.Send(new GetPermissionGroupedListQuery { RoleId = roleId }));
    }
}
