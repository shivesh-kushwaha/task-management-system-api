using TaskManagementSystem.Application.Auth.Dtos;

namespace TaskManagementSystem.Application.Auth.Commands;

public class GetAuthLoginCommand: ICommand<GetAuthLoginDto>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
