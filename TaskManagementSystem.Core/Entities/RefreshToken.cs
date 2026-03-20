using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Core.Entities;

public sealed class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set;   }
    public DateTimeOffset? DeletedAt { get; set;   }
    public int Status { get; set; }
    public User User { get; set; } = null!;
}