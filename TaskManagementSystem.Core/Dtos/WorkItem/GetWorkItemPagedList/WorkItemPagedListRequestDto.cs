namespace TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;

public sealed record WorkItemPagedListRequestDto: PagedListRequestDto
{
    public int? ParentId { get; set; }
}
