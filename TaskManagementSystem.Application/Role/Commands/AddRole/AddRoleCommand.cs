namespace TaskManagementSystem.Application.Role.Commands.AddRole;

public class AddRoleCommand: ICommand
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
