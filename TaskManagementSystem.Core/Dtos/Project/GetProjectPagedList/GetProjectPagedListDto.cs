using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;

public sealed record GetProjectPagedListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public string CreatedByFirstName { get; set; } = null!;
    public string CreatedByLastName { get; set; } = null!;

    // Team
    public int? TeamId { get; set; }
    public string? TeamName { get; set; }

    // WorkItem
    public int TotalWorkItem { get; set; }
}
