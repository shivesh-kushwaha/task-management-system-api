using TaskManagementSystem.Application.Commands.Comment.AddComment;
using TaskManagementSystem.Application.Commands.Comment.DeleteComment;
using TaskManagementSystem.Application.Commands.Comment.UpdateComment;
using TaskManagementSystem.Application.Queries.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Comment.AddComment;
using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Dtos.Comment.UpdateComment;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CommentController(
    IMapper mapper,
    IMediator mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCommentDto request)
    {
        var command = mapper.Map<AddCommentCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedListResponseDto<GetCommentPagedListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedList([FromQuery] GetCommentPagedListRequestDto request)
    {
        var query = mapper.Map<GetCommentPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCommentDto request)
    {
        var command = mapper.Map<UpdateCommentCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteCommentCommand { Id = id });
        return Ok();
    }
}
