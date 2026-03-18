using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Auth.Commands.AddUser;

internal sealed class AddUserCommandHandler(IUserRepository userRepository,
    IUnitOfWork unitOfWork): ICommandHandler<AddUserCommand, int>
{
    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository
            .AsQueryable()
            .AnyAsync(x => x.Status != RecordStatusEnum.Deleted.ToInt()
                && x.Email.Trim().ToUpper() == request.Email.Trim().ToUpper(),
                cancellationToken))
            throw new InvalidOperationException("User already exists.");

        var hashPassword = Utility.HashPassword(request.Password);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = hashPassword,
            CreatedAt = Utility.GetCurrentDateTimeOffset(),
            CreatedById = null,
            Status = (int)RecordStatusEnum.Active,
        };

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
