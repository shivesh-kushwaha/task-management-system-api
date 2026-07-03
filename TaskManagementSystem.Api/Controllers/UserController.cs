using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.DeleteUser;
using TaskManagementSystem.Application.Commands.User.UpdateUser;
using TaskManagementSystem.Application.Queries.User.GetUserListItem;
using TaskManagementSystem.Application.Queries.User.GetUserPagedList;
using TaskManagementSystem.Application.Queries.User.GetWorkItemListById;
using TaskManagementSystem.Core.Constants;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.User.AddUser;
using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;
using TaskManagementSystem.Core.Dtos.User.UpdateUser;
using static TaskManagementSystem.Api.Filters.AuthorizationPermissionFilter;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] AddUserDto request)
    {
        var command = mapper.Map<AddUserCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("paged-list")]
    [AuthorizePermission(Core.Enums.PermissionMatchTypeEnum.Any, PermissionCodeConstant.User.AddUser)]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedListRequestDto request)
    {
        var query = mapper.Map<GetUserPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpGet("select-list-item")]
    [AuthorizePermission(Core.Enums.PermissionMatchTypeEnum.Any, PermissionCodeConstant.User.ViewUser)]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListItem()
    {
        return Ok(await mediator.Send(new GetUserListItemQuery()));
    }

    [HttpGet("work-item-list/{id:int}")]
    [AuthorizePermission(Core.Enums.PermissionMatchTypeEnum.Any, PermissionCodeConstant.User.ViewUser)]
    [ProducesResponseType(typeof(IList<GetWorkItemListByIdDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkItemListById([FromRoute] int id)
    {
        return Ok(await mediator.Send(new GetWorkItemListByIdQuery { Id = id }));
    }

    [HttpPut]
    [AuthorizePermission(Core.Enums.PermissionMatchTypeEnum.Any, PermissionCodeConstant.User.UpdateUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromBody] UpdateUserDto request)
    {
        await mediator.Send(mapper.Map<UpdateUserCommand>(request));
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [AuthorizePermission(Core.Enums.PermissionMatchTypeEnum.Any, PermissionCodeConstant.User.DeleteUser)]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteUserCommand { Id = id });
        return Ok();
    }
}
