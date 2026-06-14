namespace TaskManagementSystem.Application.Commands.User.DeleteUser;

public class DeleteUserCommand : BaseCommand, ICommand
{
    public int Id { get; set; }
}
