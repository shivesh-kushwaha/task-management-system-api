namespace TaskManagementSystem.Application.Commands.ExceptionLog.DeleteExceptionLog;

internal sealed class DeleteExceptionLogCommandHandler(IUnitOfWork unitOfWork,
    IExceptionLogRepository exceptionLogRepository)
    : ICommandHandler<DeleteExceptionLogCommand>
{
    public async Task Handle(DeleteExceptionLogCommand request, CancellationToken cancellationToken)
    {
        var exceptionLog = await exceptionLogRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Exception log not found.");

        exceptionLog.Status = RecordStatusEnum.Deleted;
        exceptionLog.DeletedById = request.UserId;
        exceptionLog.DeletedAt = Utility.GetCurrentDateTimeOffset();

        exceptionLogRepository.Update(exceptionLog);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
