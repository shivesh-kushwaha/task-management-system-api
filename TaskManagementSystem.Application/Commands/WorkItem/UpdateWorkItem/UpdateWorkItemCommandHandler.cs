using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Commands.WorkItem.UpdateWorkItem;

internal sealed class UpdateWorkItemCommandHandler(
    IUnitOfWork unitOfWork,
    IWorkItemRepository workItemRepository,
    IWorkItemTypeRepository workItemTypeRepository)
    : ICommandHandler<UpdateWorkItemCommand>
{
    public async Task Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Work item not found.");

        workItem.Title = request.Title;
        workItem.AssignedToId = request.AssignedToId;
        workItem.DueDate = request.DueDate;
        workItem.Priority = request.Priority;
        workItem.TypeId = request.TypeId;
        workItem.ParentId = request.ParentId;
        workItem.ProjectId = request.ProjectId;
        workItem.Description = request.Description;
        workItem.UpdatedAt = Utility.GetCurrentDateTimeOffset();
        workItem.UpdatedById = request.UserId;

        workItemRepository.Update(workItem);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
