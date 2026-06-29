using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<PagedListResponseDto<GetRolePagedListDto>> GetPagedListAsync(PagedListRequestDto request, CancellationToken cancellationToken = default);
}
