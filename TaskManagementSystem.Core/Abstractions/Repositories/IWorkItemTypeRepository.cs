using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface IWorkItemTypeRepository
{
    Task AddAsync(WorkItemType workItemType);
}
