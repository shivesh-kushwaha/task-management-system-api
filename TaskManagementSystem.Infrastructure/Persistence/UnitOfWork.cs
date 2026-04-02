using Microsoft.EntityFrameworkCore.Storage;
using TaskManagementSystem.Core.Abstractions;

namespace TaskManagementSystem.Infrastructure.Persistence;

public sealed class UnitOfWork
    : IUnitOfWork
{
    private IDbContextTransaction? _dbContextTransaction;
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        => _dbContextTransaction ??= await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task<int> CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            var tracks = await _dbContext.SaveChangesAsync(cancellationToken);
            if (_dbContextTransaction != null)
            {
                await _dbContextTransaction.CommitAsync(cancellationToken);
                return tracks;
            }
            else
            {
                return 0;
            }
        }
        catch(Exception ex)
        {
            await RollbackTransactionAsync(cancellationToken);
            return 0;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_dbContextTransaction != null)
            await _dbContextTransaction.RollbackAsync(cancellationToken);
    }
}
