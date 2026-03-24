using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProjectController(IMapper mapper, IMediator mediator): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProject([FromBody] AddProjectDto request)
    {
        try
        {
            var command = mapper.Map<AddProjectCommand>(request);
            await mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
