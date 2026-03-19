namespace TaskManagementSystem.Application.Auth.Commands.AddUser;

public class AddUserCommand: ICommand
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<int> Roles { get; set; } = [];
}
