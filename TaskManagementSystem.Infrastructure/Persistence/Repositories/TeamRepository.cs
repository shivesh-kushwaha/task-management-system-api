using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class TeamRepository(ApplicationDbContext dbContext)
    : Repository<Team>(dbContext), ITeamRepository
{
    public async Task<Team?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Teams
            .Include(t => t.Members
                .Where(x => x.Status != RecordStatusEnum.Deleted))
            .Where(t => t.Status != RecordStatusEnum.Deleted
                && t.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
