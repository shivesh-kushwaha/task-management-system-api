using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class PermissionGroupRepository(ApplicationDbContext dbContext)
    : Repository<PermissionGroup>(dbContext), IPermissionGroupRepository
{
}