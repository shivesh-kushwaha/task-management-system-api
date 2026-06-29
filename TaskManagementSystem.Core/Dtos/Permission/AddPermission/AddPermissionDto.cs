namespace TaskManagementSystem.Core.Dtos.Permission.AddPermission;

public sealed record AddPermissionDto
{
    public int PermissionGroupId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
