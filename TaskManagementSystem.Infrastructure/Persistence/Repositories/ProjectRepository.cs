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

        var response = from p in dbContext.Projects.AsNoTracking()
                       .Include(x => x.WorkItems
                            .Where(w => w.Status != Core.Enums.RecordStatusEnum.Deleted))

                       join u in dbContext.Users.AsNoTracking()
                            on p.CreatedById equals u.Id
                            into groupedUsers
                       from user in groupedUsers.DefaultIfEmpty()

                       join t in dbContext.Teams.AsNoTracking()
                            on p.TeamId equals t.Id
                            into groupedTeams
                       from team in groupedTeams.DefaultIfEmpty()

                       where p.Status != Core.Enums.RecordStatusEnum.Deleted
                          && user == null || user.Status != Core.Enums.RecordStatusEnum.Deleted
                          && team == null || team.Status != Core.Enums.RecordStatusEnum.Deleted
                          && (string.IsNullOrEmpty(request.FilterKey)
                              || EF.Functions.Like(p.Name.Trim().ToUpper(),
                                    $"%{request.FilterKey.Trim().ToUpper()}%")
                              || EF.Functions.Like(team.Name.Trim().ToUpper(),
                                    $"%{request.FilterKey.Trim().ToUpper()}%")
                              || EF.Functions.Like(
                                    (user.FirstName.Trim() + " " + user.LastName.Trim()).ToUpper(),
                                    $"%{request.FilterKey.Trim().ToUpper()}%"))

                       select new GetProjectPagedListDto
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           Type = p.Type,
                           CreatedByFirstName = user != null ? user.FirstName : string.Empty,
                           CreatedByLastName = user != null ? user.LastName : string.Empty,
                           TeamId = team != null ? team.Id : null,
                           TeamName = team != null ? team.Name : null,
                           TotalWorkItem = p.WorkItems.Count,
                       };

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
