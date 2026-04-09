namespace TaskManagementSystem.Core.Dtos.Project.GetProjectById;

public sealed record GetProjectByIdDto : GetUserInformationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProjectTypeEnum Type { get; set; }
    public string? Description { get; set; }
    public RecordStatusEnum Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int? CreatedById { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public int? UpdatedById { get; set; }
    public string? TeamName { get; set; }
    public int TotalWorkItems { get; set; }
}
