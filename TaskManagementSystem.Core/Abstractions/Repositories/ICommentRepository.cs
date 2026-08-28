using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Abstractions.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<PagedListResponseDto<GetCommentPagedListDto>> GetPagedListAsync(GetCommentPagedListRequestDto request, CancellationToken cancellationToken = default);
}
