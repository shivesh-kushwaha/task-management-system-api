namespace TaskManagementSystem.Application.Commands.Permission.AddPermission;

public class AddPermissionCommand : BaseCommand, ICommand
{
    public int PermissionGroupId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
