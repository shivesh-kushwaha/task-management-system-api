namespace TaskManagementSystem.Application.Commands.Comment.DeleteComment;

internal sealed class DeleteCommentCommandHandler(ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteCommentCommand>
{
    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Comment not found.");

        comment.Status = RecordStatusEnum.Deleted;
        comment.DeletedById = request.UserId;
        comment.DeletedAt = Utility.GetCurrentDateTimeOffset();

        commentRepository.Update(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
