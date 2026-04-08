using TaskManagementSystem.Application.Commands.Team.AddTeam;
using TaskManagementSystem.Application.Commands.Team.Dtos;
using TaskManagementSystem.Application.Queries.Team.GetTeamListItem;
using TaskManagementSystem.Core.Dtos;

namespace TaskManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TeamController(IMapper mapper,
        IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTeam([FromBody] AddTeamDto request)
        {
            var command = mapper.Map<AddTeamCommand>(request);
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("select-list-item")]
        [ProducesResponseType(typeof(List<SelectListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListItem()
        {
            var query = await mediator.Send(new GetTeamListItemQuery());
            return Ok(query);
        }
    }
}
