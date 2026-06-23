namespace TaskManagementSystem.Application.Commands.Comment.AddComment;

public class AddCommentCommand: BaseCommand, ICommand
{
    public string Description { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
