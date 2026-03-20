namespace TaskManagementSystem.Application.Common.Dtos;

public sealed record TokenResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
