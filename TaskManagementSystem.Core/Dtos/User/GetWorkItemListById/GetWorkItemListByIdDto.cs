namespace TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;

public sealed record GetWorkItemListByIdDto
{
    public int WorkItemId { get; set;  }
    public string WorkItemName { get; set; } = null!;
    public int? WorkItemParentId { get; set; }
    public int? ProjectId { get; set; }
    public string? ProjectName { get; set; } = null!;
}
