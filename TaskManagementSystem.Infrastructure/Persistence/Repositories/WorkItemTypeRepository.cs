using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class WorkItemTypeRepository(ApplicationDbContext dbContext): IWorkItemTypeRepository
{
    public async Task AddAsync(WorkItemType workItemType)
    {
        await dbContext.WorkItemTypes.AddAsync(workItemType);
    }

    public async Task<WorkItemType?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.WorkItemTypes.FindAsync(id, cancellationToken);
    }

    public void Update(WorkItemType workItemType)
    {
        dbContext.Update(workItemType);
    }
}
