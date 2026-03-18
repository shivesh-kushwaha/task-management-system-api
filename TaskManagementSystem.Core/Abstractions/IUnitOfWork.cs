namespace TaskManagementSystem.Core.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task<int> CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
