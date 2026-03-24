using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class ProjectRepository(ApplicationDbContext dbContext)
    : Repository<Project>(dbContext), IProjectRepository
{
}
