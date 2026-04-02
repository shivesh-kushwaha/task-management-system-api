// TeamMember.cs
namespace TaskManagementSystem.Core.Entities;

public sealed class TeamMember : BaseEntity
{
    public int TeamId { get; set; }
    public int UserId { get; set; }
    public Team Team { get; set; } = null!;
    public User User { get; set; } = null!;
}