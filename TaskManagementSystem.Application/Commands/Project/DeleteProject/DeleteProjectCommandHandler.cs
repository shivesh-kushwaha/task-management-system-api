namespace TaskManagementSystem.Application.Commands.Project.DeleteProject;

internal sealed class DeleteProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository
            .AsQueryable()
            .Where(x => x.Id == request.Id
                && x.Status != RecordStatusEnum.Deleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (project == null)
        {
            throw new InvalidOperationException("Project doesn't exists.");
        }
        else
        {
            project.Status = RecordStatusEnum.Deleted;
            project.DeletedAt = Utility.GetCurrentDateTimeOffset();
            project.DeletedById = request.UserId;
            projectRepository.Update(project);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
