using TaskManagementSystem.Core.Dtos.Project.GetProjectById;

namespace TaskManagementSystem.Application.Queries.Project.GetProjectById;

public class GetProjectByIdQuery: IQuery<GetProjectByIdDto>
{
    public int Id { get; set; }
}
