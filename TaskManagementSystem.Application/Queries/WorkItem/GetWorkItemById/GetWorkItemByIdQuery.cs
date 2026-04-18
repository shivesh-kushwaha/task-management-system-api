using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemById;

namespace TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemById;

public class GetWorkItemByIdQuery: IQuery<GetWorkItemByIdDto>
{
    public int Id { get; set; }
}
