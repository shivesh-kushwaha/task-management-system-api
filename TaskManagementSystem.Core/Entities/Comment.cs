using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Core.Entities;

public sealed class Comment: BaseEntity
{
    public string Description { get; set; } = null!;
    public CommentTypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
