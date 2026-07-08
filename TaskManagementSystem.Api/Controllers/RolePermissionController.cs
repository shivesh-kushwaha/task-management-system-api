using TaskManagementSystem.Application.Commands.Permission.AddPermission;
using TaskManagementSystem.Application.Commands.RolePermission.UpsertRolePermission;
using TaskManagementSystem.Application.Queries.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;
using TaskManagementSystem.Core.Dtos.Permission.AddPermission;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;
using TaskManagementSystem.Core.Dtos.RolePermission.UpsertRolePermission;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RolePermissionController(IMediator mediator,
    IMapper mapper) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] UpsertRolePermissionDto request)
    {
        var command = mapper.Map<UpsertRolePermissionCommand>(request);
        await mediator.Send(command);
        return Ok();
    }
}
