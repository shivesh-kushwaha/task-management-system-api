namespace TaskManagementSystem.Core.Dtos.RolePermission.UpsertRolePermission;

public sealed record UpsertRolePermissionDto
{
    public int RoleId { get; set; }
    public List<int> PermissionIds { get; set; } = [];
}
