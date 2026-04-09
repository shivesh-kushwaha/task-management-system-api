using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class ProjectRepository(ApplicationDbContext dbContext)
    : Repository<Project>(dbContext), IProjectRepository
{
    public async Task<PagedListResponseDto<GetProjectPagedListDto>> GetPagedListAsync(
    PagedListRequestDto request,
    CancellationToken cancellationToken)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        var query = from p in dbContext.Projects.AsNoTracking()
                    .Include(x => x.WorkItems.Where(w => w.Status != Core.Enums.RecordStatusEnum.Deleted))
                    join u in dbContext.Users.AsNoTracking()
                        on p.CreatedById equals u.Id into groupedUsers
                    from user in groupedUsers.DefaultIfEmpty()
                    join t in dbContext.Teams.AsNoTracking()
                        on p.TeamId equals t.Id into groupedTeams
                    from team in groupedTeams.DefaultIfEmpty()
                    where p.Status != Core.Enums.RecordStatusEnum.Deleted
                        && (user == null || user.Status != Core.Enums.RecordStatusEnum.Deleted)
                        && (team == null || team.Status != Core.Enums.RecordStatusEnum.Deleted)
                    select new { p, user, team };

        // Apply filter
        if (!string.IsNullOrWhiteSpace(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x =>
                EF.Functions.Like(x.p.Name.Trim().ToUpper(), $"%{filterKey}%") ||
                (x.user != null && EF.Functions.Like((x.user.FirstName.Trim() + " " + x.user.LastName.Trim()).ToUpper(), $"%{filterKey}%"))
            );
        }

        // Project to DTO
        var response = query.Select(x => new GetProjectPagedListDto
        {
            Id = x.p.Id,
            Name = x.p.Name,
            Description = x.p.Description,
            Type = x.p.Type,
            CreatedAt = x.p.CreatedAt,
            CreatedByFirstName = x.user != null ? x.user.FirstName : string.Empty,
            CreatedByLastName = x.user != null ? x.user.LastName : string.Empty,
            CreatedByFullName = x.user != null ? x.user.FirstName + " " + x.user.LastName : string.Empty,
            TeamId = x.team != null ? x.team.Id : null,
            TeamName = x.team != null ? x.team.Name : null,
            TotalWorkItem = x.p.WorkItems.Count(w => w.Status != Core.Enums.RecordStatusEnum.Deleted)
        });

        return new PagedListResponseDto<GetProjectPagedListDto>
        {
            TotalCount = await response.CountAsync(cancellationToken),
            Items = await response
                        .OrderBy(sortExpression)
                        .Skip(recordToSkip)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken)
        };
    }
}
