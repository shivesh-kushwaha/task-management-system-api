using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.Project.DeleteProject;

public class DeleteProjectCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
}
