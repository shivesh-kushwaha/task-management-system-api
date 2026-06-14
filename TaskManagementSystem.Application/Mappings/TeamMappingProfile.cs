using TaskManagementSystem.Application.Commands.Team.AddTeam;
using TaskManagementSystem.Core.Dtos.Team.AddTeam;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class TeamMappingProfile: Profile
{
    public TeamMappingProfile()
    {
        CreateMap<AddTeamDto, AddTeamCommand>();
    }
}
