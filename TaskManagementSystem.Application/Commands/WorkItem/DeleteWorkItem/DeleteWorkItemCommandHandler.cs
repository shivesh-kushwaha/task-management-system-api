namespace TaskManagementSystem.Application.Commands.WorkItem.DeleteWorkItem;

internal sealed class DeleteWorkItemCommandHandler(
    IUnitOfWork unitOfWork,
    IWorkItemRepository workItemRepository)
    : ICommandHandler<DeleteWorkItemCommand>
{
    public async Task Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
    {
        await workItemRepository
            .AsQueryable()
            .Where(x =>  x.Id == request.Id)
            .ExecuteUpdateAsync(setters =>
            {
                setters.SetProperty(x => x.Status, RecordStatusEnum.Deleted);
                setters.SetProperty(x => x.DeletedAt, Utility.GetCurrentDateTimeOffset());
                setters.SetProperty(x => x.DeletedById, request.UserId);
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
