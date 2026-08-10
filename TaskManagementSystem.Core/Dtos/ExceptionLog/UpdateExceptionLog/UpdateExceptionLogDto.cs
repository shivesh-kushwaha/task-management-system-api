namespace TaskManagementSystem.Core.Dtos.ExceptionLog.UpdateExceptionLog;

public sealed record UpdateExceptionLogDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
}
