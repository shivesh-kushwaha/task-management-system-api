using Microsoft.EntityFrameworkCore.Storage;
using TaskManagementSystem.Core.Abstractions;
using TaskManagementSystem.Infrastructure.Persistence.Context;

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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
        => _dbContextTransaction ??= await _dbContext.Database.BeginTransactionAsync();

    public async Task<int> CommitTransactionAsync()
    {
        try
        {
            var tracks = await _dbContext.SaveChangesAsync();
            if (_dbContextTransaction != null)
            {
                await _dbContextTransaction.CommitAsync();
                return tracks;
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            return 0;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_dbContextTransaction != null)
            await _dbContextTransaction.RollbackAsync();
    }
}
