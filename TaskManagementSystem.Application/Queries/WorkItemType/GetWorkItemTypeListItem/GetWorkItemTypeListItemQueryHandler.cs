namespace TaskManagementSystem.Application.Queries.WorkItemType.GetWorkItemTypeListItem;

internal sealed class GetWorkItemTypeListItemQueryHandler(IWorkItemTypeRepository workItemTypeRepository)
    : IQueryHandler<GetWorkItemTypeListItemQuery, IList<SelectListItemDto>>
{
    public async Task<IList<SelectListItemDto>> Handle(GetWorkItemTypeListItemQuery request, CancellationToken cancellationToken)
    {
        return await workItemTypeRepository.GetListItemAsync(cancellationToken);
    }
}
