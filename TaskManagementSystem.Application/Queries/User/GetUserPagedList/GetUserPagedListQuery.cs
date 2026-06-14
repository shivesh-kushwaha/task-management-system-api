using TaskManagementSystem.Application.Commands;
using TaskManagementSystem.Core.Dtos.User.GetUserPagedList;

namespace TaskManagementSystem.Application.Queries.User.GetUserPagedList;

public class GetUserPagedListQuery: BaseCommand, IQuery<PagedListResponseDto<GetUserPagedListDto>>
{
    public PagedListRequestDto Filter { get; set; } = null!;
}
