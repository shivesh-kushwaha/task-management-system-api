using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.DeleteUser;
using TaskManagementSystem.Application.Commands.User.Dtos;
using TaskManagementSystem.Application.Queries.User.GetUserListItem;
using TaskManagementSystem.Application.Queries.User.GetUserPagedList;
using TaskManagementSystem.Application.Queries.User.GetWorkItemListById;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;

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
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedListRequestDto request)
    {
        var query = mapper.Map<GetUserPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpGet("select-list-item")]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListItem()
    {
        return Ok(await mediator.Send(new GetUserListItemQuery()));
    }

    [HttpGet("work-item-list/{id:int}")]
    [ProducesResponseType(typeof(IList<GetWorkItemListByIdDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkItemListById([FromRoute] int id)
    {
        return Ok(await mediator.Send(new GetWorkItemListByIdQuery { Id = id }));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteUserCommand { Id = id });
        return Ok();
    }
}
