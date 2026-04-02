namespace TaskManagementSystem.Core.Entities;

public sealed class Team : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Project> Projects { get; set; } = [];
    public ICollection<TeamMember> Members { get; set; } = [];
}
