using TaskManagementSystem.Core.Dtos.WorkItem.AddWorkItem;

namespace TaskManagementSystem.Core.Dtos.WorkItem.UpdateWorkItem;

public sealed record UpdateWorkItemDto: AddWorkItemDto
{
    public int Id { get; set; }
}
