using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.WorkItem.DeleteWorkItem;

public class DeleteWorkItemCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
}
