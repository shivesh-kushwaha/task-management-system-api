using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Abstractions.Services;

public interface IAuthService
{
    string GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken(int userId, CancellationToken cancellationToken);
}
