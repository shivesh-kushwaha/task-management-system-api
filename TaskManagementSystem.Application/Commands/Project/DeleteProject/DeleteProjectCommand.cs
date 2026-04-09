namespace TaskManagementSystem.Application.Commands.Project.DeleteProject;

public class DeleteProjectCommand: BaseCommand, ICommand
{
    public int Id { get; set; }
}
