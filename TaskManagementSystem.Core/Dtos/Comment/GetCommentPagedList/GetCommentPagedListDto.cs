namespace TaskManagementSystem.Core.Dtos.Comment.GetCommentPagedList;

public sealed record GetCommentPagedListDto : GetUserInformationDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int? CreatedById { get; set; }
}
