using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;

namespace TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;

public class GetWorkItemPagedListQuery: IQuery<PagedListResponseDto<GetWorkItemPagedListDto>>
{
    public WorkItemPagedListRequestDto Filter { get; set; } = null!;
}
