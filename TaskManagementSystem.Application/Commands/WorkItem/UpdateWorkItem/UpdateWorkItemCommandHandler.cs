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
        try
        {
            var workItem = await workItemRepository.FindAsync(request.Id)
                ?? throw new InvalidOperationException("Work item not found.");

            var workItemType = await workItemTypeRepository.FindAsync(request.TypeId ?? 0, cancellationToken)
                ?? throw new InvalidOperationException("Work item type not found.");

            if (request.TypeId is null && !string.IsNullOrEmpty(request.Type) && request.UserId.HasValue)
            {
                await workItemTypeRepository.AddAsync(new WorkItemType
                {
                    Name = request.Type,
                    CreatedById = request.UserId.Value,
                    CreatedAt = Utility.GetCurrentDateTimeOffset(),
                    Status = RecordStatusEnum.Active
                });

                request.TypeId = workItemType.Id;
            }

            workItem.Title = request.Title;
            workItem.AssignedToId = request.AssignedToId;
            workItem.DueDate = request.DueDate;
            workItem.Priority = request.Priority;
            workItem.TypeId = request.TypeId;
            workItem.ParentId = request.ParentId;
            workItem.Description = request.Description;
            workItem.UpdatedAt = Utility.GetCurrentDateTimeOffset();
            workItem.UpdatedById = request.UserId;

            workItemRepository.Update(workItem);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }
}
