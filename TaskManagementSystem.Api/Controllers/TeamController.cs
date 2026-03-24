using TaskManagementSystem.Application.Commands.Team.AddTeam;
using TaskManagementSystem.Application.Commands.Team.Dtos;

namespace TaskManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TeamController(IMapper mapper, IMediator mediator): ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTeam([FromBody] AddTeamDto request)
        {
            try
            {
                var command = mapper.Map<AddTeamCommand>(request);
                await mediator.Send(command);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
