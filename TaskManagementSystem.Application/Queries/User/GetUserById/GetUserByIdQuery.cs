using TaskManagementSystem.Core.Dtos.User.GetUserById;

namespace TaskManagementSystem.Application.Queries.User.GetUserById;

public class GetUserByIdQuery : IQuery<GetUserByIdDto>
{
    public int Id { get; set; }
}
