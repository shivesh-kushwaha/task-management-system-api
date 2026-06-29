using TaskManagementSystem.Application.Queries.Dashboard.GetDashboard;
using TaskManagementSystem.Core.Dtos.Dashboard.GetDashboard;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class DashboardController(IMediator mediator) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetDashboardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        return Ok(await mediator.Send(new GetDashboardQuery()));
    }
}
