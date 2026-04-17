using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;

namespace TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;

internal sealed class GetWorkItemPagedListQueryHandler(
    IWorkItemRepository workItemRepository)
    : IQueryHandler<GetWorkItemPagedListQuery, PagedListResponseDto<GetWorkItemPagedListDto>>
{
    public async Task<PagedListResponseDto<GetWorkItemPagedListDto>> Handle(GetWorkItemPagedListQuery request, CancellationToken cancellationToken)
    {
        return await workItemRepository.GetPagedListAsync(request.Filter, cancellationToken);
    }
}
