using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.Role.AddRole;
using TaskManagementSystem.Application.Commands.Role.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class RoleController(IMapper mapper, IMediator mediator): ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDto request)
    {
        try
        {
            var command = mapper.Map<AddRoleCommand>(request);
            await mediator.Send(command);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
