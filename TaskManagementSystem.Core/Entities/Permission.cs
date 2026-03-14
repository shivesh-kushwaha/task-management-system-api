namespace TaskManagementSystem.Core.Entities;

public class Permission: BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int PermissionId { get; set; }
    public PermissionGroup PermissionGroup { get; set; } = null!;
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
