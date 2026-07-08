using TaskManagementSystem.Core.Dtos.User.GetUserById;

namespace TaskManagementSystem.Application.Queries.User.GetUserById;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("User not found.");
    }
}
