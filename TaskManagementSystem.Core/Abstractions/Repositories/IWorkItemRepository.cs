using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IWorkItemRepository: IRepository<WorkItem>
{
    Task<PagedListResponseDto<GetWorkItemPagedListDto>> GetPagedListAsync(WorkItemPagedListRequestDto request, CancellationToken cancellationToken = default);
}
