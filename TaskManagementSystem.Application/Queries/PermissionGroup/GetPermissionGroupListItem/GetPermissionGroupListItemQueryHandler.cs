namespace TaskManagementSystem.Application.Queries.PermissionGroup.GetPermissionGroupListItem;

internal sealed class GetPermissionGroupListItemQueryHandler(
    IPermissionGroupRepository permissionGroupRepository)
    : IQueryHandler<GetPermissionGroupListItemQuery, List<SelectListItemDto>>
{
    public async Task<List<SelectListItemDto>> Handle(GetPermissionGroupListItemQuery request, CancellationToken cancellationToken)
    {
        return await permissionGroupRepository.AsQueryable()
            .Where(x => x.Status != RecordStatusEnum.Deleted)
            .Select(x => new SelectListItemDto
            {
                Key = x.Id,
                Value = x.Name
            })
            .ToListAsync(cancellationToken);
    }
}
