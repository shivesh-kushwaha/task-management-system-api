using TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;

namespace TaskManagementSystem.Application.Queries.Role.GetRolePagedList;

public class GetRolePagedListQuery : IQuery<PagedListResponseDto<GetRolePagedListDto>>
{
    public PagedListRequestDto Filter { get; set; } = null!;
}
