using TaskManagementSystem.Application.Common.Dtos;

namespace TaskManagementSystem.Application.Commands.Auth.AuthLogin;

public class AuthLoginCommand: ICommand<TokenResponseDto>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
