namespace TaskManagementSystem.Core.Dtos.Team.AddTeam;

public record AddTeamDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<int> Members { get; set; } = [];
}
