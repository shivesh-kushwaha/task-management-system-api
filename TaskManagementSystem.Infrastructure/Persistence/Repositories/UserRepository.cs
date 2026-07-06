using TaskManagementSystem.Core.Dtos;
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
                .Where(ur => ur.Status != RecordStatusEnum.Deleted          // if UserRole has Status
                             && ur.Role.Status != RecordStatusEnum.Deleted))
                .ThenInclude(ur => ur.Role)
            .Where(x => x.Email.Trim().ToUpper() == email.Trim().ToUpper()
                        && x.Status != RecordStatusEnum.Deleted
                        && x.UserRoles.Any(ur => ur.Status != RecordStatusEnum.Deleted
                                                 && ur.Role.Status != RecordStatusEnum.Deleted))
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
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            Name = x.FirstName + " " + x.LastName,
            Email = x.Email,
            CreatedById = x.CreatedById,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
            CreatedByFirstName = x.FirstName,
            CreatedByLastName = x.LastName,
            UpdatedByFirstName = x.FirstName,
            UpdatedByLastName = x.LastName,
            Roles = x.UserRoles
                .Where(x => x.Status != RecordStatusEnum.Deleted)
                .Select(r => new SelectListItemDto
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
}
