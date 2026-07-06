using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.Comment.AddComment;

public class AddCommentCommand: BaseRequest, ICommand
{
    public string Description { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
