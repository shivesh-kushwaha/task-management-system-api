using Azure.Core;
using TaskManagementSystem.Application.Commands.ExceptionLog.DeleteExceptionLog;
using TaskManagementSystem.Application.Commands.ExceptionLog.UpdateExceptionLog;
using TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogById;
using TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogById;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Dtos.ExceptionLog.UpdateExceptionLog;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ExceptionLogController(
    IMapper mapper,
    IMediator mediator) : ControllerBase
{
    [HttpGet("paged-list")]
    [ProducesResponseType(typeof(PagedListResponseDto<GetExceptionLogPagedListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedList([FromQuery] GetExceptionLogPagedListRequestDto request)
    {
        var query = mapper.Map<GetExceptionLogPagedListQuery>(request);
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetExceptionLogByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var query = await mediator.Send(new GetExceptionLogByIdQuery { Id = id });
        return Ok(query);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateExceptionLogDto request)
    {
        var command = mapper.Map<UpdateExceptionLogCommand>(request);
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteExceptionLogCommand { Id = id });
        return Ok();
    }
}
