using TaskManagementSystem.Application.Abstractions;

namespace TaskManagementSystem.Application.Commands.PermissionGroup.UpsertPermissionGroup;

public class UpsertPermissionGroupCommand: BaseCommand, ICommand
{
    public int Key { get; set; }
    public string Value { get; set; } = null!;
}
