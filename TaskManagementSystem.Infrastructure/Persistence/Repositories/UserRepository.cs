using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.User.GetUserById;
using TaskManagementSystem.Core.Dtos.User.GetUserPagedList;
using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User>(dbContext: dbContext), IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email)
    {
        return dbContext.Users.AsNoTracking()
            .Include(x => x.UserRoles
                .Where(ur => ur.Status != RecordStatusEnum.Deleted))
            .AsNoTracking()
            .Where(x => x.Email.Trim().ToUpper() == email.Trim().ToUpper())
            .FirstOrDefaultAsync();
    }

    public async Task<PagedListResponseDto<GetUserPagedListDto>> GetPagedListAsync(PagedListRequestDto request, int userId, CancellationToken cancellationToken = default)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        var query = dbContext.Users.AsNoTracking()
            .Include(x => x.UserRoles
                .Where(r => r.Status != RecordStatusEnum.Deleted))
            .ThenInclude(x => x.Role)
            .Where(x => x.Status != RecordStatusEnum.Deleted
                && x.Id != userId)
            .Select(x => x);

        if (!string.IsNullOrWhiteSpace(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x => EF.Functions.Like((x.FirstName.Trim() + " " + x.LastName.Trim()).ToUpper(), $"%{filterKey}%"));
        }

        var response = query.Select(x => new GetUserPagedListDto
        {
            Id = x.Id,
            Name = x.FirstName + " " + x.LastName,
            Email = x.Email,
            CreatedById = x.CreatedById,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
            CreatedByFirstName = x.FirstName,
            CreatedByLastName = x.LastName,
            UpdatedByFirstName = x.FirstName,
            UpdatedByLastName = x.LastName,
            Roles = x.UserRoles.Select(r => new SelectListItemDto
            {
                Key = r.Role.Id,
                Value = r.Role.Name
            }).ToList()
        });

        return new PagedListResponseDto<GetUserPagedListDto>
        {
            TotalCount = await response.CountAsync(cancellationToken),
            Items = await response
                        .OrderBy(sortExpression)
                        .Skip(recordToSkip)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken)
        };
    }

    public Task<List<GetWorkItemListByIdDto>> GetWorkItemListByIdAsync(int id, CancellationToken cancellationToken)
    {
        return (from w in dbContext.WorkItems.AsNoTracking()
                join p in dbContext.Projects.AsNoTracking()
                    on w.ProjectId equals p.Id
                    into groupedProject
                from project in groupedProject
                where w.Status != RecordStatusEnum.Deleted
                && w.AssignedToId == id
                && (project == null
                    || project.Status != RecordStatusEnum.Deleted)
                select new GetWorkItemListByIdDto
                {
                    WorkItemId = w.Id,
                    WorkItemName = w.Title,
                    WorkItemParentId = w.ParentId,
                    ProjectId = project != null ? project.Id : 0,
                    ProjectName = project != null ? project.Name : "-",

                }).ToListAsync(cancellationToken);
    }

    public async Task<GetUserByIdDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var query = from u in dbContext.Users.AsNoTracking()
                    where u.Id == id && u.Status != RecordStatusEnum.Deleted
                    join createdBy in dbContext.Users.AsNoTracking()
                        on u.CreatedById equals createdBy.Id into createdByGroup
                    from createdUser in createdByGroup.DefaultIfEmpty()
                    join updatedBy in dbContext.Users.AsNoTracking()
                        on u.UpdatedById equals updatedBy.Id into updatedByGroup
                    from updatedUser in updatedByGroup.DefaultIfEmpty()
                    join ur in dbContext.UserRoles.AsNoTracking() on u.Id equals ur.UserId into userRoles
                    where userRoles.Any(ur => ur.Status != RecordStatusEnum.Deleted
                                              && ur.Role.Status != RecordStatusEnum.Deleted)
                    select new GetUserByIdDto
                    {
                        Id = u.Id,
                        Name = u.FirstName + " " + u.LastName,
                        Email = u.Email,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt,
                        Status = u.Status,
                        // User information (from base DTO)
                        CreatedByFirstName = createdUser != null ? createdUser.FirstName : null,
                        CreatedByLastName = createdUser != null ? createdUser.LastName : null,
                        UpdatedByFirstName = updatedUser != null ? updatedUser.FirstName : null,
                        UpdatedByLastName = updatedUser != null ? updatedUser.LastName : null,
                        // Roles
                        Roles = userRoles
                            .Where(ur => ur.Status != RecordStatusEnum.Deleted
                                         && ur.Role.Status != RecordStatusEnum.Deleted)
                            .Select(ur => new SelectListItemDto
                            {
                                Key = ur.Role.Id,
                                Value = ur.Role.Name
                            })
                            .OrderBy(r => r.Value)
                            .ToList()
                    };

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
