using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class WorkItemTypeRepository(ApplicationDbContext dbContext): IWorkItemTypeRepository
{
    public async Task AddAsync(WorkItemType workItemType)
    {
        await dbContext.WorkItemTypes.AddAsync(workItemType);
    }
}
