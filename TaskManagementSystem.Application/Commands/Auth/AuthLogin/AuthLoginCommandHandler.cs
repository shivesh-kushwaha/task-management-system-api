using System.Security.Cryptography;
using System.Text;
using TaskManagementSystem.Application.Common.Dtos;
namespace TaskManagementSystem.Application.Commands.Auth.AuthLogin;

internal sealed class AuthLoginCommandHandler(
    IUserRepository userRepository,
    IAuthService authService)
    : ICommandHandler<AuthLoginCommand, TokenResponseDto>
{
    public async Task<TokenResponseDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email)
            ?? throw new InvalidOperationException("User doesn't exist.");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid email or password.");

        var accessToken = authService.GenerateAccessToken(user);
        var refreshToken = await authService.GenerateRefreshToken(user.Id, cancellationToken);

        var response = new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return response;
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computeHash = Convert.ToBase64String(hashBytes);
        return computeHash == passwordHash;
    }
}