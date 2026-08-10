namespace TaskManagementSystem.Application.Commands.Attachment.UploadSingleAttachment;

internal sealed class UploadSingleAttachmentCommandHandler(IAttachmentRepository attachmentRepository,
    IAttachmentService attachmentService) : ICommandHandler<UploadSingleAttachmentCommand>
{
    public async Task Handle(UploadSingleAttachmentCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId is null)
            throw new InvalidOperationException("User is unauthorized");

        await attachmentService.UploadAsync(request.File, request.Type, request.TypeId, request.UserId.Value, cancellationToken);
    }
}
