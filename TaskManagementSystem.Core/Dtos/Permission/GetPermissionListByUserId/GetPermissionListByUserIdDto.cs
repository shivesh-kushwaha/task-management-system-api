namespace TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;

public sealed record GetPermissionListByUserIdDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
