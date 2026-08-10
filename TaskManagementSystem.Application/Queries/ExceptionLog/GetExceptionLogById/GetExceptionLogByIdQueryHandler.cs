using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogById;

namespace TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogById;

internal sealed class GetExceptionLogByIdQueryHandler(IExceptionLogRepository exceptionLogRepository)
    : IQueryHandler<GetExceptionLogByIdQuery, GetExceptionLogByIdDto>
{
    public async Task<GetExceptionLogByIdDto> Handle(GetExceptionLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await exceptionLogRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Exception log not found.");
    }
}
