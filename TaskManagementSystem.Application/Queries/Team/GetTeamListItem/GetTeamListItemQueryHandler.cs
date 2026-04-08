namespace TaskManagementSystem.Application.Queries.Team.GetTeamListItem;

internal sealed class GetTeamListItemQueryHandler(
    ITeamRepository teamRepository)
    : IQueryHandler<GetTeamListItemQuery, List<SelectListItemDto>>
{
    public async Task<List<SelectListItemDto>> Handle(GetTeamListItemQuery request, CancellationToken cancellationToken)
    {
        return await teamRepository.AsQueryable()
            .AsNoTracking()
            .Where(x => x.Status != RecordStatusEnum.Deleted)
            .Select(x => new SelectListItemDto
            {
                Key = x.Id,
                Value = x.Name
            }).ToListAsync(cancellationToken);
    }
}
