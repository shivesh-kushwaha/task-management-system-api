namespace TaskManagementSystem.Core.Dtos.Comment.AddComment;

public sealed record AddCommentDto
{
    public string Description { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
