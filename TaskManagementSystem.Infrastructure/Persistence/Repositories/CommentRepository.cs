using TaskManagementSystem.Core.Dtos;
using TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Infrastructure.Persistence.Repositories;

internal sealed class CommentRepository(ApplicationDbContext dbContext)
    : Repository<Comment>(dbContext), ICommentRepository
{
    public async Task<PagedListResponseDto<GetCommentPagedListDto>> GetPagedListAsync(GetCommentPagedListRequestDto request, CancellationToken cancellationToken = default)
    {
        var sortExpression = request.SortExpression();
        var recordToSkip = request.RecordsToSkip();

        var query = from c in dbContext.Comments.AsNoTracking()
                    join u in dbContext.Users.AsNoTracking()
                        on c.CreatedById equals u.Id into groupedUser
                    from user in groupedUser
                    where c.Status != RecordStatusEnum.Deleted
                    && (user == null || user.Status != RecordStatusEnum.Deleted)
                    && c.Type == request.Type
                    && c.TypeId == request.TypeId
                    select new { c, user };

        if (!string.IsNullOrEmpty(request.FilterKey))
        {
            var filterKey = request.FilterKey.Trim();
            query = query.Where(x =>
                EF.Functions.Like(x.c.Description.Trim().ToUpper(), $"%{filterKey}%")
                || (x.user != null
                    && EF.Functions.Like((x.user.FirstName.Trim() + " " + x.user.LastName.Trim()).ToUpper(), $"%{filterKey}%"))
            );
        }

        var response = query.Select(x => new GetCommentPagedListDto
        {
            Id = x.c.Id,
            Description = x.c.Description,
            Type = x.c.Type,
            TypeId = x.c.TypeId,
            CreatedAt = x.c.CreatedAt,
            CreatedById = x.c.CreatedById,
            CreatedByFirstName = x.user == null ? string.Empty : x.user.FirstName,
            CreatedByLastName = x.user == null ? string.Empty : x.user.LastName,
        });

        return new PagedListResponseDto<GetCommentPagedListDto>
        {
            TotalCount = await response.CountAsync(cancellationToken),
            Items = await response
                    .OrderBy(sortExpression)
                    .Skip(recordToSkip)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken)
        };
    }
}
