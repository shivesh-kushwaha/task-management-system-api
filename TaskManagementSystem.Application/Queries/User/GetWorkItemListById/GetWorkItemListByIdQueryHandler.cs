using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;

namespace TaskManagementSystem.Application.Queries.User.GetWorkItemListById;

internal sealed class GetWorkItemListByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetWorkItemListByIdQuery, IList<GetWorkItemListByIdDto>>
{
    public async Task<IList<GetWorkItemListByIdDto>> Handle(GetWorkItemListByIdQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetWorkItemListByIdAsync(request.Id, cancellationToken);
    }
}
