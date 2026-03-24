using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User>(dbContext: dbContext), IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email)
    {
        return dbContext.Users.AsNoTracking()
            .Include(x => x.UserRoles
                .Where(ur => ur.Status != RecordStatusEnum.Deleted))
            .AsNoTracking()
            .Where(x => x.Email.Trim().ToUpper() == email.Trim().ToUpper())
            .FirstOrDefaultAsync();
    }
}
