namespace TaskManagementSystem.Application.Commands.Auth.AuthLogout;

internal sealed class AuthLogoutCommandHandler(IUnitOfWork unitOfWork,
    IRefreshTokenRepository refreshTokenRepository)
    : ICommandHandler<AuthLogoutCommand>
{
    public async Task Handle(AuthLogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken)
            ?? throw new InvalidOperationException("Token not found");

        refreshToken.Status = RecordStatusEnum.Deleted;
        refreshToken.DeletedAt = Utility.GetCurrentDateTimeOffset();

        refreshTokenRepository.Update(refreshToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
