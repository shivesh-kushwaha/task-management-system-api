using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<List<GetPermissionGroupedListDto>> GetPermissionGroupedListByRoleIdAsync(int roleId, CancellationToken cancellationToken = default);
}
