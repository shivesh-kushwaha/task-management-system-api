namespace TaskManagementSystem.Application.Queries.Role.GetRoleListItem;

internal sealed class GetRoleListItemQueryHandler(IRoleRepository roleRepository)
    : IQueryHandler<GetRoleListItemQuery, List<SelectListItemDto>>
{
    public async Task<List<SelectListItemDto>> Handle(GetRoleListItemQuery request, CancellationToken cancellationToken)
    {
        return await roleRepository
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.Status != RecordStatusEnum.Deleted)
            .Select(x => new SelectListItemDto
            {
                Key = x.Id,
                Value = x.Name
            })
            .ToListAsync(cancellationToken);

    }
}
