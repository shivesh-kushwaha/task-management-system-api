using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogById;
using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IExceptionLogRepository : IRepository<ExceptionLog>
{
    Task<PagedListResponseDto<GetExceptionLogPagedListDto>> GetPagedListAsync(
    GetExceptionLogPagedListRequestDto request, CancellationToken cancellationToken = default);
    Task<GetExceptionLogByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
