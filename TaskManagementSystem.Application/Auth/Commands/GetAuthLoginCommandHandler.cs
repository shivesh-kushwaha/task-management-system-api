using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementSystem.Application.Auth.Dtos;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Auth.Commands;

internal sealed class GetAuthLoginCommandHandler(IUserRepository userRepository)
    : ICommandHandler<GetAuthLoginCommand, GetAuthLoginDto> 
{
    public async Task<GetAuthLoginDto> Handle(GetAuthLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email)
            ?? throw new InvalidOperationException("User doesn't exist.");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid email or password.");

        var token = GenerateToken(user);

        return new GetAuthLoginDto();
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computeHash = Convert.ToBase64String(hashBytes);
        return computeHash == passwordHash;
    }

    private static string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: AppSettings.Jwt.Issuer,
            audience: AppSettings.Jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(AppSettings.Jwt.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
