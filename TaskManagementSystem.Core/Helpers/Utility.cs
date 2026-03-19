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
}
