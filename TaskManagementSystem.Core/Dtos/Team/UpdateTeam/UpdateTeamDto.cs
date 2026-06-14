using TaskManagementSystem.Core.Dtos.Team.AddTeam;

namespace TaskManagementSystem.Core.Dtos.Team.UpdateTeam;

public sealed record UpdateTeamDto: AddTeamDto
{
    public int Id { get; set; }
}
