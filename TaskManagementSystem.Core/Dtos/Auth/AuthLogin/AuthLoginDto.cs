namespace TaskManagementSystem.Core.Dtos.Auth.AuthLogin;

public sealed record AuthLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
