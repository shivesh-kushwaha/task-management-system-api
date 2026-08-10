using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Core.Abstractions;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.Infrastructure.Persistence.Repositories;

namespace TaskManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IWorkItemRepository, WorkItemRepository>();
        services.AddScoped<IWorkItemTypeRepository, WorkItemTypeRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
