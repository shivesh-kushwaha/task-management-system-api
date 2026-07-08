using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class RoleRepository(ApplicationDbContext dbContext)
    : Repository<Role>(dbContext), IRoleRepository
{
    public async Task<PagedListResponseDto<GetRolePagedListDto>> GetPagedListAsync(PagedListRequestDto request, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Roles.AsNoTracking()
            .Where(r => r.Status != RecordStatusEnum.Deleted);

        if (!string.IsNullOrWhiteSpace(request.FilterKey))
        {
            var filter = request.FilterKey.Trim().ToUpper();
            query = query.Where(r =>
                EF.Functions.Like(r.Name.Trim().ToUpper(), $"%{filter}%") ||
                EF.Functions.Like(r.Code.Trim().ToUpper(), $"%{filter}%"));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(request.SortExpression())
            .Skip(request.RecordsToSkip())
            .Take(request.PageSize)
            .Select(r => new GetRolePagedListDto
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                Description = r.Description
            })
            .ToListAsync(cancellationToken);

        return new PagedListResponseDto<GetRolePagedListDto>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
