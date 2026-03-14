namespace TaskManagementSystem.Core;

public static class AppSettings
{
    public static string UserId { get; set; } = null!;
    public static string Password { get; set; } = null!;

    public static class Jwt
    {
        public static string Key { get; set; } = null!;
        public static string Issuer { get; set; } = null!;
        public static string Audience { get; set; } = null!;
        public static int ExpiryMinutes { get; set; }
    }
}
