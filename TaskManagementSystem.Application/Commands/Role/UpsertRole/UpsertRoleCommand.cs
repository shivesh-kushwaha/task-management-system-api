namespace TaskManagementSystem.Application.Commands.Role.UpsertRole;

public class UpsertRoleCommand : BaseRequest, ICommand
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}
