namespace TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;

public sealed record GetWorkItemPagedListDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int? AssignedToId { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public RecordStatusEnum Status { get; set; }
    public WorkItemPriorityEnum Priority { get; set; }
    public string CreatedByFullName { get; set; } = null!;
    public int TotalSubTasks { get; set; }
}
