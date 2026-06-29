using TaskManagementSystem.Core.Dtos.Dashboard.GetDashboard;

namespace TaskManagementSystem.Application.Queries.Dashboard.GetDashboard;

internal sealed class GetDashboardQueryHandler(
    IProjectRepository projectRepository,
    IWorkItemRepository workItemRepository,
    IUserRepository userRepository,
    ITeamRepository teamRepository)
    : IQueryHandler<GetDashboardQuery, GetDashboardDto>
{
    public async Task<GetDashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Execute sequentially - DbContext is safe this way
            var tasks = await workItemRepository
                .AsQueryable()
                .Where(x => x.Status != RecordStatusEnum.Deleted && x.ParentId == null)
                .Select(x => new SelectListItemDto
                {
                    Key = x.Id,
                    Value = x.Title
                })
                .ToListAsync(cancellationToken);

            var totalProject = await projectRepository
                .AsQueryable()
                .Where(x => x.Status != RecordStatusEnum.Deleted)
                .CountAsync(cancellationToken);

            var totalTeam = await teamRepository
                .AsQueryable()
                .Where(x => x.Status != RecordStatusEnum.Deleted)
                .CountAsync(cancellationToken);

            var totalUser = await userRepository
                .AsQueryable()
                .Where(x => x.Status != RecordStatusEnum.Deleted)
                .CountAsync(cancellationToken);

            return new GetDashboardDto
            {
                TotalProject = totalProject,
                TotalTask = tasks.Count,
                TotalTeam = totalTeam,
                TotalUser = totalUser,
                Tasks = tasks
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve dashboard data", ex);
        }
    }
}