using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;

namespace TaskManagementSystem.Application.Queries.Permission.GetPermissionGroupedList;

internal sealed class GetPermissionGroupedListQueryHandler(
    IPermissionRepository permissionRepository)
    : IQueryHandler<GetPermissionGroupedListQuery, List<GetPermissionGroupedListDto>>
{
    public async Task<List<GetPermissionGroupedListDto>> Handle(GetPermissionGroupedListQuery request, CancellationToken cancellationToken)
    {
        return await permissionRepository.GetPermissionGroupedListByRoleIdAsync(request.RoleId, cancellationToken);
    }
}
