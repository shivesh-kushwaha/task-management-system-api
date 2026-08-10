namespace TaskManagementSystem.Application.Commands.ExceptionLog.DeleteExceptionLog;

public class DeleteExceptionLogCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
}
