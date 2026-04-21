namespace TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;

internal sealed class AddWorkItemCommandHandler(
    IWorkItemRepository workItemRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddWorkItemCommand>
{
    public async Task Handle(AddWorkItemCommand request, CancellationToken cancellationToken)
    {
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

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
