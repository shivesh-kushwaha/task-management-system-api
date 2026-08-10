namespace TaskManagementSystem.Core.Dtos.Auth.AuthLogout;

public sealed record AuthLogoutDto
{
    public string RefreshToken { get; set; } = null!;
}
