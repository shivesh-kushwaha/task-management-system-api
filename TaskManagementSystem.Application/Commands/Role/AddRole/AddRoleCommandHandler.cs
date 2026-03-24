namespace TaskManagementSystem.Application.Commands.Role.AddRole;

internal sealed class AddRoleCommandHandler(IRoleRepository roleRepository,
    IUnitOfWork unitOfWork): ICommandHandler<AddRoleCommand>
{
    public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Core.Entities.Role
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = Utility.GetCurrentDateTimeOffset(),
            CreatedById = null,
            Status = RecordStatusEnum.Active
        };

        await roleRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
