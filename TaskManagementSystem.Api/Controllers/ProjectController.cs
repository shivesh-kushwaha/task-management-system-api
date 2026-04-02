using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.Dtos;
using TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;

namespace TaskManagementSystem.Api.Controllers;

[ApiController] dfsdfsdf
[Route("[controller]")]
public sealed class ProjectController(
    ILogger<ProjectController> logger,
    IMapper mapper, 
    IMediator mediator) : ControllerBase
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

    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetProjectPagedListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedListRequestDto request)
    {
        try
        {
            var query = mapper.Map<GetProjectPagedListQuery>(request);
            return Ok(await mediator.Send(query));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Get project paged list.");
            return BadRequest(ex.Message);
        }
    }
}
