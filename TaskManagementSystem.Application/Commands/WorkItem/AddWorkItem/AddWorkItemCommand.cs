namespace TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;

public class AddWorkItemCommand: BaseCommand, ICommand
{
    public int? ProjectId { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TypeId { get; set; }
    public int? AssignedToId { get; set; }
    public DateTimeOffset DueDate { get; set; }
}
