using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemById;
using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class WorkItemRepository(ApplicationDbContext dbContext)
    : Repository<WorkItem>(dbContext), IWorkItemRepository
{
    //public async Task<GetWorkItemByIdDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    //{
    //    var query = from p in dbContext.WorkItems.AsNoTracking() 
    //}

    public async Task<PagedListResponseDto<GetWorkItemPagedListDto>> GetPagedListAsync(WorkItemPagedListRequestDto request, CancellationToken cancellationToken = default)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        var query = from wi in dbContext.WorkItems.AsNoTracking()
                    join wit in dbContext.WorkItemTypes.AsNoTracking()
                        on wi.TypeId equals wit.Id into groupedWorkItemTypes
                    from workItemType in groupedWorkItemTypes
                    join u in dbContext.Users.AsNoTracking()
                        on wi.CreatedById equals u.CreatedById into groupedUsers
                    from user in groupedUsers
                    where (wi.ParentId == null 
                            || wi.ParentId == request.ParentId)
                        && wi.Status != RecordStatusEnum.Deleted
                        && (workItemType == null
                            || workItemType.Status != RecordStatusEnum.Deleted)
                        && (user == null
                            || user.Status != RecordStatusEnum.Deleted)
                    select new { wi, workItemType, user };

        if (!string.IsNullOrEmpty(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim().ToUpper();
            query = query.Where(x =>
                EF.Functions.Like(x.wi.Title.Trim().ToUpper(), $"%{filterKey}%")

                || (x.workItemType != null
                    && EF.Functions.Like(x.workItemType.Name.Trim().ToUpper(), $"%{filterKey}%"))

                || (x.user != null
                    && EF.Functions.Like(x.user.FirstName.Trim().ToUpper() + " " + x.user.LastName.Trim().ToUpper(), $"%{filterKey}%")));
        }

        var response = query.Select(x => new GetWorkItemPagedListDto
        {
            Id = x.wi.Id,
            Title = x.wi.Title,
            Description = x.wi.Description,
            CreateadAt = x.wi.CreatedAt,
            Type = x.workItemType.Name,
            ParentId = x.wi.ParentId,
            DueDate = x.wi.DueDate,
            Status = x.wi.Status,
            AssignedToId = x.wi.AssignedToId,
            Priority = x.wi.Priority,
            TotalSubTasks = x.wi.SubTasks.Count(st => st.Status != RecordStatusEnum.Deleted),
            CreatedByFullName = x.user == null ? string.Empty : x.user.FirstName + " " + x.user.LastName
        });

        return new PagedListResponseDto<GetWorkItemPagedListDto>
        {
            TotalCount = await response.CountAsync(),
            Items = await response
                        .OrderBy(sortExpression)
                        .Skip(recordToSkip)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken)
        };
    }
}
