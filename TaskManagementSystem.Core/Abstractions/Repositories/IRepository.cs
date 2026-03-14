using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity: BaseEntity
{
    IQueryable<TEntity> AsQueryable();
    List<TEntity> GetAll(Func<TEntity, bool> filter);
    Task<TEntity?> FindAsync(int id);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity Entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
