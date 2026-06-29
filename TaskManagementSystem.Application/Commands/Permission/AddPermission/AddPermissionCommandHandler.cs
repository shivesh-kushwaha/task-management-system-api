namespace TaskManagementSystem.Application.Commands.Permission.AddPermission;

internal sealed class AddPermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddPermissionCommand>
{
    public async Task Handle(AddPermissionCommand request, CancellationToken cancellationToken)
    {
        var isExist = await permissionRepository.AsQueryable()
            .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                && x.PermissionGroupId == request.PermissionGroupId
                && x.Code.Trim().ToUpper() == request.Code.Trim().ToUpper(),
                    cancellationToken);

        if (isExist)
        {
            throw new InvalidOperationException(request.Code + " already exists for the given permission group");
        }
        else
        {
            var entity = new Core.Entities.Permission
            {
                Name = request.Name,
                Code = request.Code,
                PermissionGroupId = request.PermissionGroupId,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = request.UserId,
                Status = RecordStatusEnum.Active
            };

            await permissionRepository.AddAsync(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
