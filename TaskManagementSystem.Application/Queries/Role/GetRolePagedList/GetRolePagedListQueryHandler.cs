using TaskManagementSystem.Core.Dtos.Role.GetRolePagedList;

namespace TaskManagementSystem.Application.Queries.Role.GetRolePagedList
{
    internal sealed class GetRolePagedListQueryHandler(
        IRoleRepository roleRepository)
        : IQueryHandler<GetRolePagedListQuery, PagedListResponseDto<GetRolePagedListDto>>
    {
        public async Task<PagedListResponseDto<GetRolePagedListDto>> Handle(GetRolePagedListQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.GetPagedListAsync(request.Filter, cancellationToken);
        }
    }
}
