using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.RolePermission.UpsertRolePermission;

public class UpsertRolePermissionCommand: BaseCommand, ICommand
{
    public int RoleId { get; set; }
    public List<int> PermissionIds { get; set; } = [];
}
