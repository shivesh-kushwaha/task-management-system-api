using TaskManagementSystem.Core.Dtos.Project.GetProjectPagedList;

namespace TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;

internal sealed class GetProjectPagedListQueryHandler(
    IProjectRepository projectRepository)
    : IQueryHandler<GetProjectPagedListQuery, PagedListResponseDto<GetProjectPagedListDto>>
{
    public async Task<PagedListResponseDto<GetProjectPagedListDto>> Handle(GetProjectPagedListQuery request, CancellationToken cancellationToken)
    {
        return await projectRepository.GetPagedListAsync(request.Filter, cancellationToken);
    }
}
