namespace TaskManagementSystem.Application.Commands.Comment.DeleteComment;

public class DeleteCommentCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
}
