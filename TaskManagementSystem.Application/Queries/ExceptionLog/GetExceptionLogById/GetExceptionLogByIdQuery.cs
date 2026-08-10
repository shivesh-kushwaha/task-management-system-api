using TaskManagementSystem.Core.Dtos.ExceptionLog.GetExceptionLogById;

namespace TaskManagementSystem.Application.Queries.ExceptionLog.GetExceptionLogById;

public class GetExceptionLogByIdQuery : IQuery<GetExceptionLogByIdDto>
{
    public int Id { get; set; }
}
