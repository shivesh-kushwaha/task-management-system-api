namespace TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;

public sealed record GetExceptionLogPagedListDto : GetUserInformationDto
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public string StackTrace { get; set; } = null!;
    public LogTypeEnum LogType { get; set; }
    public TypeEnum EntityType { get; set; }
    public string? Description { get; set; }
    public string? RequestUrl { get; set; }
    public string? RequestMethod { get; set; }
    public string? IpAddress { get; set; }
    public string? AdditionalData { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public RecordStatusEnum Status { get; set; }
    public string? CreatedByFullName { get; set; }
    public string? UpdatedByFullName { get; set; }
}
