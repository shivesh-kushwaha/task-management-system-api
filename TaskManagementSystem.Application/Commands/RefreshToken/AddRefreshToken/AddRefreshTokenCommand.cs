using TaskManagementSystem.Application.Common.Dtos;

namespace TaskManagementSystem.Application.Commands.RefreshToken.AddRefreshToken;

public sealed class AddRefreshTokenCommand: ICommand<TokenResponseDto>
{
    public string Token { get; set; } = null!;
}
