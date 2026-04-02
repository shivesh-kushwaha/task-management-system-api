using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class RefreshTokenRepository(ApplicationDbContext dbContext)
    : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public async void Update(RefreshToken refreshToken)
    {
        dbContext.RefreshTokens.Update(refreshToken);
    }

    public async void UpdateRange(List<RefreshToken> refreshTokens)
    {
        dbContext.RefreshTokens.UpdateRange(refreshTokens);
    }

    public async Task<List<RefreshToken>> GetListByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await dbContext.RefreshTokens
            .Where(x => x.Status != (int)RecordStatusEnum.Deleted
                && x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await dbContext.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Status != (int)RecordStatusEnum.Deleted
                && x.Token.Trim().ToUpper() == token.Trim().ToUpper(),
             cancellationToken);
    }
}
