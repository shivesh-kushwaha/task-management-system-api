using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Infrastructure.Persistence.Context;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext): Repository<User>(dbContext), IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email)
    {
        return dbContext.Users
            .AsNoTracking()
            .Where(x => x.Email.Trim().ToUpper() == email.Trim().ToUpper())
            .FirstOrDefaultAsync();
    }
}
