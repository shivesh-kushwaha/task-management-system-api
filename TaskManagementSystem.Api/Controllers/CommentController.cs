using TaskManagementSystem.Application.Commands.Comment.AddComment;
using TaskManagementSystem.Application.Commands.Comment.Dtos;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CommentController(
    ILogger<CommentController> logger,
    IMapper mapper,
    IMediator mediator): BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto request)
    {
        try
        {
            var command = mapper.Map<AddCommentCommand>(request);
            await mediator.Send(command);
            return Ok();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Add Comment.");
            return BadRequest(ex.Message);
        }
    }
}
