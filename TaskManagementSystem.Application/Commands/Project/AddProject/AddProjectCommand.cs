namespace TaskManagementSystem.Application.Commands.Project.AddProject;

public class AddProjectCommand: ICommand
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
}
