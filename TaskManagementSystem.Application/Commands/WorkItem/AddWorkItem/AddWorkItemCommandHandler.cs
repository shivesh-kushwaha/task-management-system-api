using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;

internal sealed class AddWorkItemCommandHandler(
    IWorkItemTypeRepository workItemTypeRepository,
    IWorkItemRepository workItemRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddWorkItemCommand>
{
    public async Task Handle(AddWorkItemCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var workItemType = new WorkItemType();

            if (request.TypeId is null && !string.IsNullOrEmpty(request.Type) && request.UserId.HasValue)
            {
                workItemType = new WorkItemType
                {
                    Name = request.Type,
                    CreatedById = request.UserId.Value,
                    CreatedAt = Utility.GetCurrentDateTimeOffset(),
                    Status = RecordStatusEnum.Active
                };

                await workItemTypeRepository.AddAsync(workItemType);
            }

            var workItem = new Core.Entities.WorkItem
            {
                ProjectId = request.ProjectId,
                ParentId = request.ParentId,
                Title = request.Title,
                Description = request.Description,
                TypeId = request.TypeId,
                AssignedToId = request.AssignedToId,
                DueDate = request.DueDate,
                CreatedById = request.UserId,
                CreatedAt = Utility.GetCurrentDateTimeOffset(),
                Status = RecordStatusEnum.Active
            };

            await workItemRepository.AddAsync(workItem);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }
}
