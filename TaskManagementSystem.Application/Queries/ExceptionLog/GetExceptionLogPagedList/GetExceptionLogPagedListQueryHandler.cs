using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;

namespace TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogPagedList;

internal sealed class GetExceptionLogPagedListQueryHandler(IExceptionLogRepository exceptionLogRepository)
    : IQueryHandler<GetExceptionLogPagedListQuery, PagedListResponseDto<GetExceptionLogPagedListDto>>
{
    public async Task<PagedListResponseDto<GetExceptionLogPagedListDto>> Handle(GetExceptionLogPagedListQuery request, CancellationToken cancellationToken)
    {
        return await exceptionLogRepository.GetPagedListAsync(request.Filter, cancellationToken);
    }
}
