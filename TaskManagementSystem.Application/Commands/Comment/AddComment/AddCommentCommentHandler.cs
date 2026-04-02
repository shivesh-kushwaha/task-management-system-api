namespace TaskManagementSystem.Application.Commands.Comment.AddComment;

internal sealed class AddCommentCommentHandler(
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork): ICommandHandler<AddCommentCommand>
{
    public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Core.Entities.Comment
        {
            Description = request.Description,
            Type = request.Type,
            TypeId = request.TypeId,
            CreatedById = request.UserId,
            CreatedAt = Utility.GetCurrentDateTimeOffset(),
            Status = RecordStatusEnum.Active
        };

        await commentRepository.AddAsync(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
