namespace TaskManagementSystem.Application.Abstractions.Services;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(List<string> roleCodes, string[] permissionCodes, PermissionMatchTypeEnum type, CancellationToken cancellationToken = default);
}
