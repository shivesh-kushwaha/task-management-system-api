using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Services;

internal sealed class AuthService(
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork) : IAuthService
{
    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var userRoleIds = user.UserRoles.Select(x => x.RoleId);
        var roleIds = userRoleIds.Any()
            ? string.Join(", ", userRoleIds)
            : string.Empty;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName.Trim() + " " + user.LastName.Trim()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("RoleIds", roleIds)
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

    public async Task<string> GenerateRefreshToken(int userId, CancellationToken cancellationToken)
    {
        var existingRefreshTokens = await refreshTokenRepository.GetListByUserIdAsync(userId, cancellationToken);

        foreach (var existingRefreshToken in existingRefreshTokens)
        {
            existingRefreshToken.Status = (int)RecordStatusEnum.Deleted;
            existingRefreshToken.DeletedAt = Core.Helpers.Utility.GetCurrentDateTimeOffset();
        }

        refreshTokenRepository.UpdateRange(existingRefreshTokens);

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

        var expiresAt = Core.Helpers.Utility.GetCurrentDateTimeOffset().AddDays(AppSettings.Jwt.ExpiryDays);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = expiresAt,
            CreatedAt = Core.Helpers.Utility.GetCurrentDateTimeOffset(),
            Status = (int)RecordStatusEnum.Active
        };

        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return token;
    }
}
