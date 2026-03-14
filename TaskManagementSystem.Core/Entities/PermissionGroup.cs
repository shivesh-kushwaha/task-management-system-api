namespace TaskManagementSystem.Core.Entities;

public class PermissionGroup: BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Permission> Permissions { get; set; } = [];
}
