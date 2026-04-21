// WorkItem.cs
namespace TaskManagementSystem.Core.Entities;

public sealed class WorkItem : BaseEntity
{
    public int? ProjectId { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TypeId { get; set; }
    public int? AssignedToId { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public WorkItemPriorityEnum Priority { get; set; }
    public Project? Project { get; set; }
    public WorkItem? Parent { get; set; }
    public WorkItemType? Type { get; set; }
    public ICollection<WorkItem> SubTasks { get; set; } = [];
    public User? AssignedTo { get; set; }
}