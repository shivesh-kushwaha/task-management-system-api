namespace TaskManagementSystem.Application.Commands.Role.AddRole;

public class AddRoleCommand: BaseCommand, ICommand
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
