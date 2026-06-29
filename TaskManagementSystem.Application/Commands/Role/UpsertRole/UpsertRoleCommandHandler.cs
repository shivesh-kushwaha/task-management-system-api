namespace TaskManagementSystem.Application.Commands.Role.UpsertRole;

internal sealed class UpsertRoleCommandHandler(IRoleRepository roleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpsertRoleCommand>
{
    public async Task Handle(UpsertRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FindAsync(request.Id.HasValue ? request.Id.Value : 0);

        if (role == null)
        {
            var entity = new Core.Entities.Role
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = request.UserId,
                Status = RecordStatusEnum.Active
            };

            await roleRepository.AddAsync(entity);
        }
        else
        {
            if (await roleRepository.AsQueryable()
                    .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                        && x.Id != request.Id
                        && x.Code == request.Code, cancellationToken))
            {
                throw new InvalidOperationException("Code already exists");
            }
            else
            {
                await roleRepository.AsQueryable()
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.Name, request.Name.Trim())
                        .SetProperty(x => x.Description, request.Description?.Trim())
                        .SetProperty(x => x.UpdatedAt, Utility.GetCurrentDateTimeOffset())
                        .SetProperty(x => x.UpdatedById, request.UserId), cancellationToken);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
