using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Services;

internal sealed class ExceptionLogService(
    IExceptionLogRepository exceptionLogRepository,
    IUnitOfWork unitOfWork) : IExceptionLogService
{
    public async Task AddAsync(ExceptionLog exceptionLog, CancellationToken cancellationToken = default)
    {
        await exceptionLogRepository.AddAsync(exceptionLog);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
