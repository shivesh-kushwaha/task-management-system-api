using TaskManagementSystem.Application.Commands.PermissionGroup.UpsertPermissionGroup;
using TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;
using TaskManagementSystem.Application.Queries.PermissionGroup.GetPermissionGroupListItem;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;
using TaskManagementSystem.Core.Dtos.PermissionGroup.UpsertPermissionGroup;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PermissionGroupController(IMediator mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] UpsertPermissionGroupDto request)
    {
        await mediator.Send(new UpsertPermissionGroupCommand
        {
            Key = request.Key,
            Value = request.Value
        });
        return Ok();
    }

    [HttpGet("select-list-item")]
    [ProducesResponseType(typeof(List<SelectListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSelectListItem()
    {
        return Ok(await mediator.Send(new GetPermissionGroupListItemQuery()));
    }
}
