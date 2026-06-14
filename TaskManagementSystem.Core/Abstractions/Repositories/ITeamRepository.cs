using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface ITeamRepository: IRepository<Team>
{
    Task<Team?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
