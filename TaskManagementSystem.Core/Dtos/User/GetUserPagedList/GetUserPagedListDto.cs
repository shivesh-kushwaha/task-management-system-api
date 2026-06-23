namespace TaskManagementSystem.Core.Dtos.User.GetUserPagedList;

public sealed record GetUserPagedListDto: GetUserInformationDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int? CreatedById { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public RecordStatusEnum Status { get; set; }
    public IList<SelectListItemDto> Roles { get; set; } = [];
}
