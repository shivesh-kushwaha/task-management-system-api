using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.User.DeleteUser;

public class DeleteUserCommand : BaseRequest, ICommand
{
    public int Id { get; set; }
}
