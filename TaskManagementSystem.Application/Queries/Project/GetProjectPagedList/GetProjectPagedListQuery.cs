using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;

namespace TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;

public class GetProjectPagedListQuery: IQuery<PagedListResponseDto<GetProjectPagedListDto>>
{
    public PagedListRequestDto Filter { get; set; } = null!;
}
