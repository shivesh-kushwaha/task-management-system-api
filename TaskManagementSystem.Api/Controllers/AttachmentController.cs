using TaskManagementSystem.Application.Commands.Attachment.UploadSingleAttachment;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AttachmentController(
    IMapper mapper,
    IMediator mediator) : BaseController
{
    [HttpPost("upload-single")]
    public async Task<IActionResult> UploadSingle(
        [FromForm] TypeEnum type,
        [FromForm] int typeId,
        [FromForm] IFormFile file)
    {
        await mediator.Send(new UploadSingleAttachmentCommand { File = file, Type = type, TypeId = typeId });
        return Ok();
    }
}
