namespace TaskManagementSystem.Application.Commands.Auth.AuthLogout;

public class AuthLogoutCommand: ICommand
{
    public string RefreshToken { get; set; } = null!;
}
