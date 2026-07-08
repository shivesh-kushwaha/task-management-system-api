

using TaskManagementSystem.Core.Dtos.Permission.GetPermissionListByUserId;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class RolePermissionRepository(ApplicationDbContext dbContext)
    : Repository<RolePermission>(dbContext), IRolePermissionRepository
{
    public Task<List<GetPermissionListByUserIdDto>> GetPermissionListByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return (from u in dbContext.Users.AsNoTracking()
                join ur in dbContext.UserRoles.AsNoTracking()
                    on u.Id equals ur.UserId
                join rp in dbContext.RolePermissions.AsNoTracking()
                    on ur.RoleId equals rp.RoleId
                join p in dbContext.Permissions.AsNoTracking()
                    on rp.PermissionId equals p.Id
                where u.Status != RecordStatusEnum.Deleted
                    && ur.Status != RecordStatusEnum.Deleted
                    && rp.Status != RecordStatusEnum.Deleted
                    && u.Id == userId
                select new GetPermissionListByUserIdDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code
                })
                .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetPermissionCodesByRoleCodesAsync(List<string> roleCodes, CancellationToken cancellationToken = default)
    {
        var query = from rp in dbContext.RolePermissions
                    join p in dbContext.Permissions on rp.PermissionId equals p.Id
                    join r in dbContext.Roles on rp.RoleId equals r.Id
                    where roleCodes.Contains(r.Code)
                    select p.Code;

        return await query.Distinct().ToListAsync(cancellationToken);
    }
}
