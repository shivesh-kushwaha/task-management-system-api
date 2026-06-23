using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class UserRoleRepository(ApplicationDbContext dbContext)
    : Repository<UserRole>(dbContext), IUserRoleRepository
{

}
