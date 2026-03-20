using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    void Update(RefreshToken refreshToken);
    void UpdateRange(List<RefreshToken> refreshTokens);
    Task<List<RefreshToken>> GetListByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
}
