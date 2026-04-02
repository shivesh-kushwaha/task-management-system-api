namespace TaskManagementSystem.Core.Entities;

public sealed class User: BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTimeOffset? LastLoginAt { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<TeamMember> TeamMembers { get; set; } = [];
}
