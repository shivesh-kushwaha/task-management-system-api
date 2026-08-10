namespace TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;

public sealed record GetExceptionLogPagedListRequestDto : PagedListRequestDto
{
    public LogTypeEnum? LogType { get; set; }
    public TypeEnum? EntityType { get; set; }
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
}
