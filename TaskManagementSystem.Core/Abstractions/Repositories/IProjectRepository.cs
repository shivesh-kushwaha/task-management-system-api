using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.GetProjectById;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IProjectRepository: IRepository<Project>
{
    Task<GetProjectByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedListResponseDto<GetProjectPagedListDto>> GetPagedListAsync(PagedListRequestDto request, CancellationToken cancellationToken);
}
