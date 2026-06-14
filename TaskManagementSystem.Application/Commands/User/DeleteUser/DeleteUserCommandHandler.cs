namespace TaskManagementSystem.Application.Commands.User.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(request.Id) 
                ?? throw new InvalidOperationException("User not found.");

        user.Status = RecordStatusEnum.Deleted;
        user.DeletedAt = Utility.GetCurrentDateTimeOffset();
        user.DeletedById = request.UserId;

        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
