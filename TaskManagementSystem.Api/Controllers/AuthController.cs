using TaskManagementSystem.Application.Commands.Auth.AuthLogin;
using TaskManagementSystem.Application.Commands.Auth.Dtos;
using TaskManagementSystem.Core.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] AuthLoginDto request)
    {
        try
        {
            var command = mapper.Map<AuthLoginCommand>(request);
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
