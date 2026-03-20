using TaskManagementSystem.Application.Common.Dtos;

namespace TaskManagementSystem.Application.Commands.RefreshToken.AddRefreshToken;

internal sealed class AddRefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    IAuthService authService)
    : ICommandHandler<AddRefreshTokenCommand, TokenResponseDto>
{
    public async Task<TokenResponseDto> Handle(AddRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var existingRefreshToken = await refreshTokenRepository.GetByTokenAsync(command.Token, cancellationToken)
            ?? throw new InvalidOperationException($"Refresh token could not found.");

        if (existingRefreshToken.ExpiresAt < Utility.GetCurrentDateTimeOffset())
        {
            throw new InvalidOperationException($"{existingRefreshToken.Token} has been expired at {existingRefreshToken.ExpiresAt}");
        }

        var existingUser = await userRepository
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.Status != (int)RecordStatusEnum.Deleted
                && x.Id == existingRefreshToken.UserId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException($"User not found by id {existingRefreshToken.Id}.");

        var accessToken = authService.GenerateAccessToken(existingUser);

        var refreshToken = await authService.GenerateRefreshToken(existingUser.Id, cancellationToken);

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
