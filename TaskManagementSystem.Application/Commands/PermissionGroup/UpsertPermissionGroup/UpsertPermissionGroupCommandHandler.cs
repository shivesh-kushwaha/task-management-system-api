namespace TaskManagementSystem.Application.Commands.PermissionGroup.UpsertPermissionGroup;

internal sealed class UpsertPermissionGroupCommandHandler(
    IPermissionGroupRepository permissionGroupRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpsertPermissionGroupCommand>
{
    public async Task Handle(UpsertPermissionGroupCommand request, CancellationToken cancellationToken)
    {
        var permissionGroup = await permissionGroupRepository.FindAsync(request.Key);

        if (permissionGroup is null)
        {
            var entity = new Core.Entities.PermissionGroup
            {
                Name = request.Value,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = request.UserId,
                Status = RecordStatusEnum.Active
            };

            await permissionGroupRepository.AddAsync(entity);
        }
        else
        {
            if (await permissionGroupRepository.AsQueryable()
                    .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                        && x.Id != request.Key
                        && x.Name.Trim().ToUpper() == request.Value.Trim().ToUpper()
                            , cancellationToken))
            {
                throw new InvalidOperationException("Permission Group name already exists.");
            }

            await permissionGroupRepository.AsQueryable()
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Name, request.Value.Trim())
                    .SetProperty(x => x.UpdatedAt, Utility.GetCurrentDateTimeOffset())
                    .SetProperty(x => x.UpdatedById, request.UserId), cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
