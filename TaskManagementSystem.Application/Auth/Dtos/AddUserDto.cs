namespace TaskManagementSystem.Application.Auth.Dtos;

public sealed record AddUserDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<int> Roles { get; set; } = [];

}
