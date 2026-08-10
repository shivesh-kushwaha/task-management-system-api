namespace TaskManagementSystem.Application.Commands.ExceptionLog.UpdateExceptionLog;

public class UpdateExceptionLogCommand: BaseRequest, ICommand
{
    public int Id { get; set; }
    public string? Description { get; set; }
}
