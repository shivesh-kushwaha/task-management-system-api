using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.DeleteProject;
using TaskManagementSystem.Application.Commands.Project.UpdateProject;
using TaskManagementSystem.Application.Queries.Project.GetProjectById;
using TaskManagementSystem.Application.Queries.Project.GetProjectListItem;
using TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.AddProject;
using TaskManagementSystem.Core.Dtos.Project.GetProjectById;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Dtos.Project.UpdateProject;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProjectController(
    IMapper mapper,
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProject([FromBody] AddProjectDto request)
    {
        var command = mapper.Map<AddProjectCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetProjectPagedListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedListRequestDto request)
    {
        var query = mapper.Map<GetProjectPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetProjectByIdDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        var query = await mediator.Send(new GetProjectByIdQuery { Id = id });
        return Ok(query);
    }

    [HttpGet("select-list-item")]
    [ProducesResponseType(typeof(IList<SelectListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectListItem()
    {
        var query = await mediator.Send(new GetProjectListItemQuery());
        return Ok(query);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto request)
    {
        var command = mapper.Map<UpdateProjectCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        await mediator.Send(new DeleteProjectCommand { Id = id });
        return Ok();
    }
}
