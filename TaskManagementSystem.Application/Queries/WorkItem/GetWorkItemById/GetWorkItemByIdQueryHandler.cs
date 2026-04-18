using TaskManagementSystem.Core.Dtos.WorkItem.GetWorkItemById;

namespace TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemById;

internal sealed class GetWorkItemByIdQueryHandler(
    IWorkItemRepository workItemRepository)
    :IQueryHandler<GetWorkItemByIdQuery, GetWorkItemByIdDto>
{
    public async Task<GetWorkItemByIdDto> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await workItemRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Work item not found.");
    }
}
