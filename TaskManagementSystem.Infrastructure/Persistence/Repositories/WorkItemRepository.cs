using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemById;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class WorkItemRepository(ApplicationDbContext dbContext)
    : Repository<WorkItem>(dbContext), IWorkItemRepository
{
    public async Task<GetWorkItemByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await (from wi in dbContext.WorkItems.AsNoTracking()
                      .Include(wi => wi.AssignedTo)
                      join wit in dbContext.WorkItemTypes.AsNoTracking()
                          on wi.TypeId equals wit.Id into groupedWorkItemTypes
                      from workItemType in groupedWorkItemTypes
                      join createdBy in dbContext.Users.AsNoTracking()
                         on wi.CreatedById equals createdBy.Id into createdByGroup
                      from createdUser in createdByGroup.DefaultIfEmpty()
                      join updatedBy in dbContext.Users.AsNoTracking()
                          on wi.UpdatedById equals updatedBy.Id into updatedByGroup
                      from updatedBy in updatedByGroup.DefaultIfEmpty()
                      where wi.Id == id
                          && wi.Status != RecordStatusEnum.Deleted
                          && (workItemType == null || workItemType.Status != RecordStatusEnum.Deleted)
                          && (createdUser == null || createdUser.Status != RecordStatusEnum.Deleted)
                          && (updatedBy == null || updatedBy.Status != RecordStatusEnum.Deleted)
                      select new GetWorkItemByIdDto
                      {
                          Id = wi.Id,
                          ParentId = wi.ParentId,
                          ParentName = dbContext.WorkItems
                                 .Where(x => x.Id == wi.ParentId)
                                 .Select(x => x.Title)
                                 .SingleOrDefault(),
                          Title = wi.Title,
                          Description = wi.Description,
                          AssignedToId = wi.AssignedToId,
                          TotalSubTasks = wi.SubTasks.Count(x => x.Status != RecordStatusEnum.Deleted),
                          DueDate = wi.DueDate,
                          CreatedAt = wi.CreatedAt,
                          Status = wi.Status,
                          Priority = wi.Priority,
                          Type = workItemType.Name,
                          AssignedToName = wi.AssignedTo == null ? string.Empty : $"{wi.AssignedTo!.FirstName} {wi.AssignedTo!.LastName}",
                          CreatedByFirstName = createdUser == null ? string.Empty : createdUser.FirstName,
                          CreatedByLastName = createdUser == null ? string.Empty : createdUser.LastName,
                          UpdatedByFirstName = updatedBy == null ? string.Empty : updatedBy.FirstName,
                          UpdatedByLastName = updatedBy == null ? string.Empty : updatedBy.LastName,
                      })
                    .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedListResponseDto<GetWorkItemPagedListDto>> GetPagedListAsync(
        WorkItemPagedListRequestDto request, CancellationToken cancellationToken = default)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        var query = from wi in dbContext.WorkItems.AsNoTracking()
                    join wit in dbContext.WorkItemTypes.AsNoTracking()
                        on wi.TypeId equals wit.Id into witGroup
                    from workItemType in witGroup.DefaultIfEmpty()
                    join u in dbContext.Users.AsNoTracking()
                        on wi.CreatedById equals u.Id into userGroup
                    from user in userGroup.DefaultIfEmpty()
                    where wi.Status != RecordStatusEnum.Deleted
                          && (workItemType == null || workItemType.Status != RecordStatusEnum.Deleted)
                          && (user == null || user.Status != RecordStatusEnum.Deleted)
                    select new { wi, workItemType, user };

        if (request.ParentId.HasValue)
            query = query.Where(x => x.wi.ParentId == request.ParentId.Value);
        else
            query = query.Where(x => x.wi.ParentId == null);

        if (!string.IsNullOrEmpty(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x =>
                EF.Functions.Like(x.wi.Title.Trim().ToUpper(), $"%{filterKey}%") ||
                (x.workItemType != null && EF.Functions.Like(x.workItemType.Name.Trim().ToUpper(), $"%{filterKey}%")) ||
                (x.user != null && EF.Functions.Like((x.user.FirstName.Trim() + " " + x.user.LastName.Trim()).ToUpper(), $"%{filterKey}%"))
            );
        }

        var response = query.Select(x => new GetWorkItemPagedListDto
        {
            Id = x.wi.Id,
            Title = x.wi.Title,
            Description = x.wi.Description,
            CreatedAt = x.wi.CreatedAt,
            Type = x.workItemType != null ? x.workItemType.Name : null,
            ParentId = x.wi.ParentId,
            ProjectId = x.wi.ProjectId,
            DueDate = x.wi.DueDate,
            Status = x.wi.Status,
            AssignedToId = x.wi.AssignedToId,
            Priority = x.wi.Priority,
            TotalSubTasks = x.wi.SubTasks.Count(st => st.Status != RecordStatusEnum.Deleted),
            CreatedByFullName = x.user != null ? x.user.FirstName + " " + x.user.LastName : string.Empty
        });

        return new PagedListResponseDto<GetWorkItemPagedListDto>
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
