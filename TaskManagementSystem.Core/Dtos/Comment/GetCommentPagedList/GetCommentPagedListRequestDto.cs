namespace TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;

public sealed record GetCommentPagedListRequestDto: PagedListRequestDto
{
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
}
