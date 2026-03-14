namespace TaskManagementSystem.Api.Models.Auth;

public sealed record LoginRequestModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
