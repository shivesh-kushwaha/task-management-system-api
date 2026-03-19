

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class RoleRepository(ApplicationDbContext dbContext)
    :Repository<Role>(dbContext), IRoleRepository
{
}
