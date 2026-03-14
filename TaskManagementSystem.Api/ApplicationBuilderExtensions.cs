using TaskManagementSystem.Core;

namespace TaskManagementSystem.Api;

public static class ApplicationBuilderExtensions
{
    public static void LoadAppSettings(this WebApplicationBuilder builder)
    {
        AppSettings.Jwt.Key = builder.Configuration["Jwt:Key"] ?? string.Empty;
        AppSettings.Jwt.Issuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
        AppSettings.Jwt.Audience = builder.Configuration["Jwt:Audience"] ?? string.Empty;
        
        var expiryString = builder.Configuration["Jwt:ExpiryMinutes"];
        AppSettings.Jwt.ExpiryMinutes = expiryString != null && int.TryParse(expiryString, out var minutes) ? minutes : 15;
    }
}
