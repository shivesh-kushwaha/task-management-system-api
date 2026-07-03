using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagementSystem.Application.Abstractions.Services;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Api.Filters;

public class AuthorizationPermissionFilter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _requiredPermissions;
        private readonly PermissionMatchTypeEnum _matchType;

        public AuthorizePermissionAttribute(PermissionMatchTypeEnum matchType, params string[] permissions)
        {
            _requiredPermissions = permissions ?? Array.Empty<string>();
            _matchType = matchType;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Access denied!");
            }

            var roleCodesClaim = user.FindFirst("RoleCodes")?.Value;

            if (string.IsNullOrEmpty(roleCodesClaim))
            {
                throw new UnauthorizedAccessException("Access denied!");
            }

            var roleCodes = roleCodesClaim
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x)
                .ToList();

            if (!roleCodes.Any())
            {
                throw new UnauthorizedAccessException("Access denied!");
            }

            var permissionService = context.HttpContext.RequestServices
                .GetRequiredService<IPermissionService>();

            var hasPermissions = await permissionService.HasPermissionAsync(
                roleCodes,
                _requiredPermissions,
                _matchType,
                context.HttpContext.RequestAborted);

            if (!hasPermissions)
            {
                throw new UnauthorizedAccessException("Access denied!");
            }
        }
    }
}


