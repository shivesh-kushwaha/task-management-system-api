using Microsoft.Extensions.DependencyInjection;
namespace TaskManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.AuthMappingProfile>());

        return services;
    }
}
