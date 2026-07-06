using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommand: BaseRequest, ICommand
{
    public int Id {  get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
}
