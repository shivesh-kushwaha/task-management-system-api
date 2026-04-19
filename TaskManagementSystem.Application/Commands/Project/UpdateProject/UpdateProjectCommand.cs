namespace TaskManagementSystem.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommand: BaseCommand, ICommand
{
    public int Id {  get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
}
