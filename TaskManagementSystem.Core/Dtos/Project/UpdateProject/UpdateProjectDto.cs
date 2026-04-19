using TaskManagementSystem.Core.Dtos.Project.AddProject;

namespace TaskManagementSystem.Core.Dtos.Project.UpdateProject;

public sealed record UpdateProjectDto: AddProjectDto
{
    public int Id { get; set; }
}
