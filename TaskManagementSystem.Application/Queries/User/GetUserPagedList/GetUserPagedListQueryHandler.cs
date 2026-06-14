using TaskManagementSystem.Core.Dtos.User.GetUserPagedList;

namespace TaskManagementSystem.Application.Queries.User.GetUserPagedList;

internal sealed class GetUserPagedListQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserPagedListQuery, PagedListResponseDto<GetUserPagedListDto>>
{
    public async Task<PagedListResponseDto<GetUserPagedListDto>> Handle(GetUserPagedListQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetPagedListAsync(request.Filter, request.UserId ?? -1, cancellationToken);
    }
}
