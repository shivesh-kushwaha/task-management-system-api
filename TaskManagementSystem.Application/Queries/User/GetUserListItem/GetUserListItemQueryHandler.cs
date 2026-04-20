namespace TaskManagementSystem.Application.Queries.User.GetUserListItem;

internal sealed class GetUserListItemQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserListItemQuery, IList<SelectListItemDto>>
{
    public async Task<IList<SelectListItemDto>> Handle(GetUserListItemQuery request, CancellationToken cancellationToken)
    {
        return await userRepository
            .AsQueryable()
            .AsNoTracking()
            .Where(x => x.Status != RecordStatusEnum.Deleted)
            .Select(x => new SelectListItemDto
            {
                Key = x.Id,
                Value = x.FirstName + " " + x.LastName
            })
            .OrderBy(x => x.Value)
            .ToListAsync(cancellationToken);
    }
}
