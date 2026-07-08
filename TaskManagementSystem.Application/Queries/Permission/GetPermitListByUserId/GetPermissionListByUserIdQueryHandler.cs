using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;

namespace TaskManagementSystem.Application.Queries.Permission.GetPermitListByUserId;

internal sealed class GetPermissionListByUserIdQueryHandler(
    IRolePermissionRepository rolePermissionRepository)
    : IQueryHandler<GetPermissionListByUserIdQuery, List<GetPermissionListByUserIdDto>>
{
    public async Task<List<GetPermissionListByUserIdDto>> Handle(GetPermissionListByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await rolePermissionRepository.GetPermissionListByUserIdAsync(request.UserId ?? 0, cancellationToken);
    }
}
