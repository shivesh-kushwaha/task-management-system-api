using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.RefreshToken.AddRefreshToken;
using TaskManagementSystem.Application.Common.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public sealed class RefreshTokenController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRefreshToken([FromQuery] string token)
    {
        try
        {
            var response = await mediator.Send(new AddRefreshTokenCommand { Token = token });
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
