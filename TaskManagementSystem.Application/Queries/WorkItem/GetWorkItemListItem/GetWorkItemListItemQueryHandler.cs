namespace TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemListItem;

internal sealed class GetWorkItemListItemQueryHandler(IWorkItemRepository workItemRepository)
    : IQueryHandler<GetWorkItemListItemQuery, IList<SelectListItemDto>>
{
    public async Task<IList<SelectListItemDto>> Handle(GetWorkItemListItemQuery request, CancellationToken cancellationToken)
    {
        return await workItemRepository
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.Status != RecordStatusEnum.Deleted
                && x.ParentId == null)
            .Select(x => new SelectListItemDto
            {
                Key = x.Id,
                Value = x.Title
            })
            .OrderBy(x => x.Value)
            .ToListAsync(cancellationToken);
    }
}
