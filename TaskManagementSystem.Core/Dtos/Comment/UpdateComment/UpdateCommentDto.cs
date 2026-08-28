using TaskManagementSystem.Core.Dtos.Comment.AddComment;

namespace TaskManagementSystem.Core.Dtos.Comment.UpdateComment;

public sealed record UpdateCommentDto : AddCommentDto
{
    public int Id { get; set; }
}
