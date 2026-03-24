namespace TaskManagementSystem.Application.Commands.Project.Dtos;

public sealed record AddProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
}
