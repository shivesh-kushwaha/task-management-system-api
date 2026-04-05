using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.Dtos;

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
}
