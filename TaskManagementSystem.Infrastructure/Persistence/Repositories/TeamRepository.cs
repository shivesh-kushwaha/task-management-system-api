using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class TeamRepository(ApplicationDbContext dbContext)
    : Repository<Team>(dbContext), ITeamRepository
{
}
