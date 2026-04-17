using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Project.GetProjectById;
using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class ProjectRepository(ApplicationDbContext dbContext)
    : Repository<Project>(dbContext), IProjectRepository
{
    public async Task<GetProjectByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var query = from p in dbContext.Projects.AsNoTracking()
            .Include(p => p.Team)
            .Include(p => p.WorkItems.Where(wi => wi.Status != Core.Enums.RecordStatusEnum.Deleted))
                    where p.Id == id && p.Status != Core.Enums.RecordStatusEnum.Deleted
                    join createdBy in dbContext.Users.AsNoTracking()
                        on p.CreatedById equals createdBy.Id into createdByGroup
                    from createdUser in createdByGroup.DefaultIfEmpty()
                    join updatedBy in dbContext.Users.AsNoTracking()
                        on p.UpdatedById equals updatedBy.Id into updatedByGroup
                    from updatedUser in updatedByGroup.DefaultIfEmpty()
                    select new GetProjectByIdDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Type = p.Type,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt,
                        CreatedById = p.CreatedById,
                        UpdatedAt = p.UpdatedAt,
                        UpdatedById = p.UpdatedById,
                        TeamName = p.Team != null ? p.Team.Name : string.Empty,
                        TotalWorkItems = p.WorkItems.Count(wi => wi.Status != Core.Enums.RecordStatusEnum.Deleted),
                        // User information
                        CreatedByFirstName = createdUser != null ? createdUser.FirstName : string.Empty,
                        CreatedByLastName = createdUser != null ? createdUser.LastName : null,
                        UpdatedByFirstName = updatedUser != null ? updatedUser.FirstName : null,
                        UpdatedByLastName = updatedUser != null ? updatedUser.LastName : null
                    };

        return await query.SingleOrDefaultAsync(cancellationToken);
    }
    
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
                    where p.Status != RecordStatusEnum.Deleted
                        && (user == null || user.Status != RecordStatusEnum.Deleted)
                        && (team == null || team.Status != RecordStatusEnum.Deleted)
                    select new { p, user, team };

        // Apply filter
        if (!string.IsNullOrWhiteSpace(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x =>
                EF.Functions.Like(x.p.Name.Trim().ToUpper(), $"%{filterKey}%") 
                || (x.user != null 
                    && EF.Functions.Like((x.user.FirstName.Trim() + " " + x.user.LastName.Trim()).ToUpper(), $"%{filterKey}%"))
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
            TotalWorkItem = x.p.WorkItems.Count(w => w.Status != RecordStatusEnum.Deleted)
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
