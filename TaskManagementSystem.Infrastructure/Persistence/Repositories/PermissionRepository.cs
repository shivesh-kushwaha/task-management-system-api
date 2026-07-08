using TaskManagementSystem.Core.Dtos.Permission.GetPermissionGroupedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class PermissionRepository(ApplicationDbContext dbContext)
    : Repository<Permission>(dbContext), IPermissionRepository
{
    public async Task<List<GetPermissionGroupedListDto>> GetPermissionGroupedListByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        var response = await dbContext.PermissionGroups
            .AsNoTracking()
            .Where(pg => pg.Status != RecordStatusEnum.Deleted
                && pg.Permissions.Any())
            .Select(pg => new GetPermissionGroupedListDto
            {
                PermissionGroupId = pg.Id,
                PermissionGroupName = pg.Name,
                Permissions = pg.Permissions
                    .Where(p => p.Status != RecordStatusEnum.Deleted)
                    .Select(p => new GetPermissionListItemDto
                    {
                        Key = p.Id,
                        Value = p.Name,
                        IsChecked = dbContext.RolePermissions
                            .Any(rp => rp.PermissionId == p.Id
                                       && rp.RoleId == roleId
                                       && rp.Status != RecordStatusEnum.Deleted)
                    })
                    .OrderBy(p => p.Value)
                    .ToList(),
            })
            .OrderBy(pg => pg.PermissionGroupName)
            .ToListAsync(cancellationToken);

        foreach (var group in response)
        {
            group.IsAllPermissionChecked = group.Permissions.All(x => x.IsChecked);
        }

        return response;
    }
}