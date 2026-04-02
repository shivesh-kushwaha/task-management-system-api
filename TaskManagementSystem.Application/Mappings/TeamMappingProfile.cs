using TaskManagementSystem.Application.Commands.Team.AddTeam;
using TaskManagementSystem.Application.Commands.Team.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class TeamMappingProfile: Profile
{
    public TeamMappingProfile()
    {
        CreateMap<AddTeamDto, AddTeamCommand>();
    }
}
