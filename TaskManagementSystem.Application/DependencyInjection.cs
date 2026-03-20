using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Abstractions.Services;
using TaskManagementSystem.Application.Services;
namespace TaskManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        // Mapping configurations
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.AuthMappingProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.RoleMappingProfile>());

        // Services
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
