using Microsoft.AspNetCore.Http;

namespace TaskManagementSystem.Application.Commands.Attachment.UploadSingleAttachment;

public class UploadSingleAttachmentCommand : BaseRequest, ICommand
{
    public IFormFile File { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
