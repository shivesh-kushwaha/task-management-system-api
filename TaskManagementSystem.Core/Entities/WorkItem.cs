// WorkItem.cs
namespace TaskManagementSystem.Core.Entities;

public sealed class WorkItem : BaseEntity
{
    public int? ProjectId { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TypeId { get; set; }
    public int? AssignedToId { get; set; }                   // ✅ Nullable to allow SetNull on delete
    public DateTimeOffset DueDate { get; set; }
    public Project? Project { get; set; }                    // ✅ Added navigation property
    public WorkItem? Parent { get; set; }                    // ✅ Added self-ref navigation
    public WorkItemType? Type { get; set; }
    public ICollection<WorkItem> SubTasks { get; set; } = [];
    public User? AssignedTo { get; set; }                    // ✅ Nullable to match nullable FK
}