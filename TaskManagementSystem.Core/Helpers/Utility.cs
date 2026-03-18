using System.Security.Cryptography;
using System.Text;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Core.Helpers;

public static class Utility
{
    public static DateTimeOffset GetCurrentDateTimeOffset()
    {
        return DateTimeOffset.UtcNow;
    }

    public static int ToInt(this RecordStatusEnum status)
    {
        return (int)status;
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string passwordHash)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computeHash = Convert.ToBase64String(hashBytes);
        return computeHash == passwordHash;
    }
}
