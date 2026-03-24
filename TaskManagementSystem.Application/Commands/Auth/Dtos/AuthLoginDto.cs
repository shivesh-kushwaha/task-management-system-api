namespace TaskManagementSystem.Application.Commands.Auth.Dtos;

public sealed record AuthLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
