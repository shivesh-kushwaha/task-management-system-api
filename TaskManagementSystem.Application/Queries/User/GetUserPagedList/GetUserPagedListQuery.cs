using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Core.Dtos.User.GetUserPagedList;

namespace TaskManagementSystem.Application.Queries.User.GetUserPagedList;

public class GetUserPagedListQuery: BaseRequest, IQuery<PagedListResponseDto<GetUserPagedListDto>>
{
    public PagedListRequestDto Filter { get; set; } = null!;
}
