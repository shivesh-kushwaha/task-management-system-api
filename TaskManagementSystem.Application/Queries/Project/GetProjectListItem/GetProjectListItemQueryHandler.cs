namespace TaskManagementSystem.Application.Queries.Project.GetProjectListItem;

internal sealed class GetProjectListItemQueryHandler(IProjectRepository projectRepository)
    : IQueryHandler<GetProjectListItemQuery, IList<SelectListItemDto>>
{
    public async Task<IList<SelectListItemDto>> Handle(GetProjectListItemQuery request, CancellationToken cancellationToken)
    {
        return await projectRepository
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
