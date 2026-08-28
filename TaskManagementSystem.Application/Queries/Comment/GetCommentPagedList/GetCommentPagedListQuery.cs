using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;

namespace TaskManagementSystem.Application.Queries.Comment.GetCommentPagedList;

public class GetCommentPagedListQuery : IQuery<PagedListResponseDto<GetCommentPagedListDto>>
{
    public GetCommentPagedListRequestDto Request { get; set; } = null!;
}
