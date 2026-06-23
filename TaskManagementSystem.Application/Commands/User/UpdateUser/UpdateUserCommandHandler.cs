using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Commands.User.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository) : ICommandHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Get the user
        var user = await userRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("User not found.");

        // 2. Check if email already exists (excluding current user)
        if (await userRepository
                .AsQueryable()
                .AnyAsync(x => x.Status != RecordStatusEnum.Deleted
                    && x.Id != request.Id
                    && x.Email.Trim().ToUpper() == request.Email.Trim().ToUpper(),
                    cancellationToken))
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        // 3. Update user basic info
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.UpdatedAt = Utility.GetCurrentDateTimeOffset();
        user.UpdatedById = request.UserId;

        userRepository.Update(user);

        // 4. Get current roles for this user
        var currentRoles = await userRoleRepository
            .AsQueryable()
            .Where(x => x.Status != RecordStatusEnum.Deleted
                && x.UserId == request.Id)
            .ToListAsync(cancellationToken);

        var currentRoleIds = currentRoles.Select(x => x.RoleId).ToHashSet();
        var newRoleIds = request.Roles.ToHashSet();

        // 5. Find roles to remove (in current but NOT in request)
        var roleIdsToRemove = currentRoleIds.Except(newRoleIds).ToList();

        // 6. Find roles to add (in request but NOT in current)
        var roleIdsToAdd = newRoleIds.Except(currentRoleIds).ToList();

        // 7. Soft delete removed roles
        if (roleIdsToRemove.Any())
        {
            await userRoleRepository
                .AsQueryable()
                .Where(x => roleIdsToRemove.Contains(x.RoleId)
                    && x.UserId == request.Id
                    && x.Status != RecordStatusEnum.Deleted)
                .ExecuteUpdateAsync(setters =>
                {
                    setters.SetProperty(x => x.DeletedAt, Utility.GetCurrentDateTimeOffset());
                    setters.SetProperty(x => x.DeletedById, request.UserId);
                    setters.SetProperty(x => x.Status, RecordStatusEnum.Deleted);
                }, cancellationToken);
        }

        // 8. Add new roles
        if (roleIdsToAdd.Any())
        {
            var newUserRoles = roleIdsToAdd.Select(roleId => new UserRole
            {
                RoleId = roleId,
                UserId = request.Id,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = request.UserId,
                Status = RecordStatusEnum.Active
            });

            await userRoleRepository.AddRangeAsync(newUserRoles);
        }

        // 9. Save all changes
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}