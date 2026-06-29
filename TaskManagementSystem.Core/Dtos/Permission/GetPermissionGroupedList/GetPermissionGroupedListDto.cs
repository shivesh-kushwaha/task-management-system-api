namespace TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;

public sealed record GetPermissionGroupedListDto
{
    public int PermissionGroupId { get; set; }
    public bool IsAllPermissionChecked { get; set; }
    public string PermissionGroupName { get; set; } = null!;
    public List<GetPermissionListItemDto> Permissions { get; set; } = [];
}
