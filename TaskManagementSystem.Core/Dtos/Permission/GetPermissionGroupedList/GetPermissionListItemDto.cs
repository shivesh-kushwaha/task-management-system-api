namespace TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;

public sealed record GetPermissionListItemDto : SelectListItemDto
{
    public bool IsChecked { get; set; }
}
