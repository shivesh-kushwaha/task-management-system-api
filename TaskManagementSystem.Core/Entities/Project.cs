namespace TaskManagementSystem.Core.Entities;

public sealed class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTypeEnum Type { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
    public ICollection<WorkItem> WorkItems { get; set; } = [];
}