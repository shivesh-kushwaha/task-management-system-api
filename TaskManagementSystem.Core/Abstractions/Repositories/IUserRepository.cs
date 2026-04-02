using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}
