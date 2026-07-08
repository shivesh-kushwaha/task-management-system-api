using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IRolePermissionRepository : IRepository<RolePermission>
{
    Task<List<GetPermissionListByUserIdDto>> GetPermissionListByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<List<string>> GetPermissionCodesByRoleCodesAsync(List<string> roleCodes, CancellationToken cancellationToken = default);
}
