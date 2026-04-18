using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IWorkItemTypeRepository
{
    Task AddAsync(WorkItemType workItemType);
    Task<WorkItemType?> FindAsync(int id, CancellationToken cancellationToken = default);
    void Update(WorkItemType workItemType);
}
