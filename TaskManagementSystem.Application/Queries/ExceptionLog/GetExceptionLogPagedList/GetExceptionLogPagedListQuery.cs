using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;

namespace TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogPagedList;

public class GetExceptionLogPagedListQuery : IQuery<PagedListResponseDto<GetExceptionLogPagedListDto>>
{
    public GetExceptionLogPagedListRequestDto Filter { get; set; } = null!;
}
