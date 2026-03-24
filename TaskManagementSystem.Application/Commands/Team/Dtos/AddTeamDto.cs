namespace TaskManagementSystem.Application.Commands.Team.Dtos;

public sealed record AddTeamDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<int> Members { get; set; } = [];
}
