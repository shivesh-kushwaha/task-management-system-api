namespace TaskManagementSystem.Core.Dtos.Project.AddProject;

public record AddProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
}
