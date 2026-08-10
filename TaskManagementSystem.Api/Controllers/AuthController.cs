using TaskManagementSystem.Application.Commands.Auth.AuthLogin;
using TaskManagementSystem.Application.Commands.Auth.AuthLogout;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Auth.AuthLogin;
using TaskManagementSystem.Core.Dtos.Auth.AuthLogout;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthLoginDto request)
    {
        var command = mapper.Map<AuthLoginCommand>(request);
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout([FromBody] AuthLogoutDto request)
    {
        var command = mapper.Map<AuthLogoutCommand>(request);
        await mediator.Send(command);
        return Ok();
    }
}
