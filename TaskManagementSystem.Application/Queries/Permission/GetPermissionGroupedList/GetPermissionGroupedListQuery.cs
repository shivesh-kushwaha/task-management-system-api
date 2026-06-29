using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;

namespace TaskManagementSystem.Application.Queries.Permission.GetPermissionGroupedList;

public class GetPermissionGroupedListQuery: IQuery<List<GetPermissionGroupedListDto>>
{
    public int RoleId { get; set; }
}
