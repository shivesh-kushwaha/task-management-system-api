using TaskManagementSystem.Core.Dtos.User.AddUser;

namespace TaskManagementSystem.Core.Dtos.User.UpdateUser;

public sealed record UpdateUserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<int> Roles { get; set; } = [];
}
