namespace TaskManagementSystem.Application.Commands.Comment.UpdateComment;

public class UpdateCommentCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
}
