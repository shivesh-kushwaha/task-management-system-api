namespace TaskManagementSystem.Application.Commands.RolePermission.UpsertRolePermission;

internal sealed class UpsertRolePermissionCommandHandler(
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpsertRolePermissionCommand>
{
    public async Task Handle(UpsertRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionIds = await rolePermissionRepository.AsQueryable()
                                .Where(x => x.Status != RecordStatusEnum.Deleted
                                    && x.RoleId == request.RoleId)
                                .Select(x => x.PermissionId)
                                .ToListAsync(cancellationToken);

        var deletePermisionIds = permissionIds.Except(request.PermissionIds);
        var addPermissionIds = request.PermissionIds.Except(permissionIds);

        if (deletePermisionIds.Any())
        {
            await rolePermissionRepository.AsQueryable()
                .Where(x => x.Status != RecordStatusEnum.Deleted
                    && x.RoleId == request.RoleId
                    && deletePermisionIds.Contains(x.PermissionId))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Status, RecordStatusEnum.Deleted)
                    .SetProperty(x => x.DeletedAt, Utility.GetCurrentDateTimeOffset())
                    .SetProperty(x => x.DeletedById, request.UserId)
                    , cancellationToken);
        }

        if (addPermissionIds.Any())
        {
            var entities = addPermissionIds.Select(x => new Core.Entities.RolePermission
            {
                RoleId = request.RoleId,
                PermissionId = x,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = request.UserId,
                Status = RecordStatusEnum.Active
            });

            await rolePermissionRepository.AddRangeAsync(entities);
        }

        if (deletePermisionIds.Any() || addPermissionIds.Any())
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
