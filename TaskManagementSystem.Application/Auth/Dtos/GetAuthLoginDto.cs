namespace TaskManagementSystem.Application.Auth.Dtos;

public sealed record GetAuthLoginDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
}