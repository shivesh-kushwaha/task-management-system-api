namespace TaskManagementSystem.Core.Entities;

public class ExceptionLog : BaseEntity
{
    public string Message { get; set; } = null!;
    public string StackTrace { get; set; } = null!;
    public LogTypeEnum LogType { get; set; }
    public TypeEnum EntityType { get; set; }
    public string? Description { get; set; }
    public string? RequestUrl { get; set; }
    public string? RequestMethod { get; set; }
    public string? IpAddress { get; set; }
    public string? AdditionalData { get; set; }
}