using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Api.Models.Auth;
using TaskManagementSystem.Application.Auth.Commands;

namespace TaskManagementSystem.Api.Controllers;

public class AuthController(IMediator mediator) : ControllerBase
{

    [HttpGet("login")]
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
