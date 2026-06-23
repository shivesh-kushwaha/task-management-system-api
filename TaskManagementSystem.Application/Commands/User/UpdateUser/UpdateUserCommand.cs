namespace TaskManagementSystem.Application.Commands.User.UpdateUser;

public class UpdateUserCommand : BaseCommand, ICommand
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<int> Roles { get; set; } = [];
}
