namespace TaskManagementSystem.Application.Services;

internal sealed class PermissionService(
    IRolePermissionRepository rolePermissionRepository) : IPermissionService
{
    public async Task<bool> HasPermissionAsync(List<int> roleIds, string[] permissionCodes, PermissionMatchTypeEnum type, CancellationToken cancellationToken = default)
    {
        // Fetch all permission codes for the given roles
        var existingPermissionCodes = await rolePermissionRepository
            .GetPermissionCodesByRoleIdsAsync(roleIds, cancellationToken);

        var permissionSet = new HashSet<string>(permissionCodes);

        return type switch
        {
            PermissionMatchTypeEnum.Any => permissionCodes.Any(p => permissionSet.Contains(p)),
            PermissionMatchTypeEnum.All => permissionCodes.All(p => permissionSet.Contains(p)),
            _ => false
        };
    }
}
