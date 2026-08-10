using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Abstractions.Services;

public interface IExceptionLogService
{
    Task AddAsync(ExceptionLog exceptionLog, CancellationToken cancellationToken = default);
}
