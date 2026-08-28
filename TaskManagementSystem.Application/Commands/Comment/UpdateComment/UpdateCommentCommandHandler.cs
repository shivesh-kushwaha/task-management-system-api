namespace TaskManagementSystem.Application.Commands.Comment.UpdateComment;

internal sealed class UpdateCommentCommandHandler(ICommentRepository commentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCommentCommand>
{
    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Comment not found.");

        comment.Description = request.Description;
        comment.UpdatedAt = Utility.GetCurrentDateTimeOffset();
        comment.UpdatedById = request.UserId;

        commentRepository.Update(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
