using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Behaviors;
using TaskManagementSystem.Application.Services;

namespace TaskManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Pipelines
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserContextPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

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
