namespace TaskManagementSystem.Application.Commands.WorkItem.DeleteWorkItem;

public class DeleteWorkItemCommand: BaseCommand, ICommand
{
    public int Id { get; set; }
}
