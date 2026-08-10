using TaskManagementSystem.Core;

namespace TaskManagementSystem.Api;

public static class ApplicationBuilderExtensions
{
    public static void LoadAppSettings(this WebApplicationBuilder builder)
    {
        AppSettings.Cors.ValidOrigins = builder.Configuration
            .GetSection("ValidOrigins")
            .Get<string[]>() ?? [];

        AppSettings.Jwt.Key = builder.Configuration["Jwt:Key"] ?? string.Empty;
        AppSettings.Jwt.Issuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
        AppSettings.Jwt.Audience = builder.Configuration["Jwt:Audience"] ?? string.Empty;

        var expiryMinutesString = builder.Configuration["Jwt:ExpiryMinutes"];
        AppSettings.Jwt.ExpiryMinutes = expiryMinutesString != null && int.TryParse(expiryMinutesString, out var minutes) ? minutes : 15;

        var expiryDaysString = builder.Configuration["Jwt:ExpiryDays"];
        AppSettings.Jwt.ExpiryDays = expiryDaysString != null && int.TryParse(expiryDaysString, out var days) ? days : 15;

        // File Uploadx
        var maxFileSizeString = builder.Configuration["FileUpload:MaxFileSize"];
        AppSettings.Attachment.MaxFileSize = maxFileSizeString != null && int.TryParse(maxFileSizeString, out var maxFileSize) ? maxFileSize : (10 * 1024 * 1024); // 10 MB
        AppSettings.Attachment.AllowedExtensions = builder.Configuration
            .GetSection("FileUpload:AllowedExtensions")
            .Get<string[]>() ?? [];
    }
}
