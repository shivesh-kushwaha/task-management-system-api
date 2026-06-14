using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class TeamMemberRepository(ApplicationDbContext context)
    : Repository<TeamMember>(context), ITeamMemberRepository
{
}
