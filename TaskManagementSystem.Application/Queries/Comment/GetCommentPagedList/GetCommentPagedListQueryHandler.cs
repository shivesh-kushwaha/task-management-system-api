using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;

namespace TaskManagementSystem.Application.Queries.Comment.GetCommentPagedList;

internal sealed class GetCommentPagedListQueryHandler(ICommentRepository commentRepository)
    : IQueryHandler<GetCommentPagedListQuery, PagedListResponseDto<GetCommentPagedListDto>>
{
    public async Task<PagedListResponseDto<GetCommentPagedListDto>> Handle(GetCommentPagedListQuery request, CancellationToken cancellationToken = default)
    {
        return await commentRepository.GetPagedListAsync(request.Request, cancellationToken);
    }
}
