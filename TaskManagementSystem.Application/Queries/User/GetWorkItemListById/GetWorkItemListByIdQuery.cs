using TaskManagementSystem.Core.Dtos.User.GetWorkItemListById;

namespace TaskManagementSystem.Application.Queries.User.GetWorkItemListById;

public class GetWorkItemListByIdQuery: IQuery<IList<GetWorkItemListByIdDto>>
{
    public int Id { get; set; }
}
