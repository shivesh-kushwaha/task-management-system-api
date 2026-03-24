// WorkItemType.cs
using TaskManagementSystem.Core.Enums;
namespace TaskManagementSystem.Core.Entities;

public sealed class WorkItemType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public int CreatedById { get; set; }
    public RecordStatusEnum Status { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }           // ✅ Must be nullable — not always deleted
}