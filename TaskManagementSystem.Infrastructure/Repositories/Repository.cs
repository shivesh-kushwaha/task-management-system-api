using TaskManagementSystem.Core.Abstractions.Repositories;
using TaskManagementSystem.Infrastructure.Persistence.Context;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class Repository<TEntity>(ApplicationDbContext dbContext): IRepository<TEntity> where TEntity: BaseEntity
{
    public IQueryable<TEntity> AsQueryable()
    {
        return dbContext.Set<TEntity>().AsQueryable();
    }

    public IQueryable<TEntity> AsQueryable(Func<TEntity, bool> filter)
    {
        return dbContext.Set<TEntity>().Where(filter).AsQueryable();
    }

    public List<TEntity> GetAll(Func<TEntity, bool> filter)
    {
        return [.. AsQueryable(filter)];
    }

    public Task<TEntity?> FindAsync(int id)
    {
        return dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task AddAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().AddRangeAsync(entities);
        return Task.CompletedTask;
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().RemoveRange(entities);
    }
}
