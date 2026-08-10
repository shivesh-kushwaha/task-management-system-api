namespace TaskManagementSystem.Application.Commands.ExceptionLog.UpdateExceptionLog;

internal sealed class UpdateExceptionLogCommandHandler(IUnitOfWork unitOfWork,
    IExceptionLogRepository exceptionLogRepository)
    : ICommandHandler<UpdateExceptionLogCommand>
{
    public async Task Handle(UpdateExceptionLogCommand request, CancellationToken cancellationToken)
    {
        var exceptionLog = await exceptionLogRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Exception log not found.");

        exceptionLog.Description = request.Description;
        exceptionLog.UpdatedById = request.UserId;
        exceptionLog.UpdatedAt = Utility.GetCurrentDateTimeOffset();

        exceptionLogRepository.Update(exceptionLog);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
