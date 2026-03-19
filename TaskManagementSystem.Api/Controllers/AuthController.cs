using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Api.Models.Auth;
using TaskManagementSystem.Application.Auth.Commands.AddUser;
using TaskManagementSystem.Application.Auth.Commands.GetAuthLogin;
using TaskManagementSystem.Application.Auth.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController(IMapper mapper, IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AddUserDto request)
    {
        try
        {
            var command = mapper.Map<AddUserCommand>(request);
            await mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(GetAuthLoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        try
        {
            var command = new GetAuthLoginCommand { Email = request.Email, Password = request.Password };
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
