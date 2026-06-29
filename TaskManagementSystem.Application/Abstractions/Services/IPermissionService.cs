namespace TaskManagementSystem.Application.Abstractions.Services;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(List<int> roleIds, string[] permissionCodes, PermissionMatchTypeEnum type, CancellationToken cancellationToken = default);
}
