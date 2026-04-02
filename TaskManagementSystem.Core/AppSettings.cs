namespace TaskManagementSystem.Core;

public static class AppSettings
{
    public static class Cors
    {
        public static string[] ValidOrigins { get; set; } = [];
    }

    public static class Jwt
    {
        public static string Key { get; set; } = null!;
        public static string Issuer { get; set; } = null!;
        public static string Audience { get; set; } = null!;
        public static int ExpiryMinutes { get; set; }
        public static int ExpiryDays { get; set; }
    }
}
