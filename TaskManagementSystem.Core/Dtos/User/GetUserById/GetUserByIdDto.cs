namespace TaskManagementSystem.Core.Dtos.User.GetUserById;

public sealed record GetUserByIdDto: GetUserInformationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public RecordStatusEnum Status { get; set; }
    public List<SelectListItemDto> Roles { get; set; } = [];
}
