using System.Security.Cryptography;
using System.Text;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Commands.User.AddUser;

internal sealed class AddUserCommandHandler(IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddUserCommand>
{
    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await userRepository
                .AsQueryable()
                .AnyAsync(x => x.Status != RecordStatusEnum.Deleted.ToInt()
                    && x.Email.Trim().ToUpper() == request.Email.Trim().ToUpper(),
                    cancellationToken))
                throw new InvalidOperationException("User already exists.");

            var hashPassword = HashPassword(request.Password);

            var user = new Core.Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashPassword,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                CreatedById = null,
                Status = (int)RecordStatusEnum.Active,
                UserRoles = [.. request.Roles.Select(x => new Core.Entities.UserRole
                {
                    RoleId = x,
                    CreatedAt = Utility.GetCurrentDateTimeOffset(),
                    CreatedById = null,
                    Status = (int)RecordStatusEnum.Active
                })]
            };

            await userRepository.AddAsync(user);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }
}
