using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TaskManagementSystem.Api;
using TaskManagementSystem.Api.Extensions;
using TaskManagementSystem.Api.Middlewares;
using TaskManagementSystem.Application;
using TaskManagementSystem.Core;
using TaskManagementSystem.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
     .CreateBootstrapLogger();

try
{
    Log.Information("Starting Task Management System....");

    var builder = WebApplication.CreateBuilder(args);

    builder.AddSerilogLogging();

    builder.LoadAppSettings();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new AuthorizeFilter());
    });

    builder.Services.AddOpenApi();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AppSettings.Jwt.Issuer,
            ValidAudience = AppSettings.Jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(AppSettings.Jwt.Key))
        };
    });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}