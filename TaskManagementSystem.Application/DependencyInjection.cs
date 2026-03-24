using Microsoft.Extensions.DependencyInjection;
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
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.UserMappingProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.ProjectMappingProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.TeamMappingProfile>());

        // Services
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
