namespace TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemById;

public sealed record GetWorkItemByIdDto: GetUserInformationDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? ParentName { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int? AssignedToId { get; set; }
    public string? AssignedToName { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public DateTimeOffset CreateadAt { get; set; }
    public RecordStatusEnum Status { get; set; }
    public WorkItemPriorityEnum Priority { get; set; }
    public int TotalSubTasks { get; set; }
}
