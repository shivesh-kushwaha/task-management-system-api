using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class WorkItemRepository(ApplicationDbContext dbContext)
    : Repository<WorkItem>(dbContext), IWorkItemRepository
{
}
