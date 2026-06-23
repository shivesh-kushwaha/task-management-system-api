namespace TaskManagementSystem.Application.Commands.Comment.Dtos;

public sealed class AddCommentDto
{
    public string Description { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
