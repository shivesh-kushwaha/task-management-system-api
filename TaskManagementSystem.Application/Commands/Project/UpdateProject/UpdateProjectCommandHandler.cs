namespace TaskManagementSystem.Application.Commands.Project.UpdateProject;

internal sealed class UpdateProjectCommandHandler(
    IUnitOfWork unitOfWork,
    IProjectRepository projectRepository)
    : ICommandHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.FindAsync(request.Id)
            ?? throw new InvalidOperationException("Project not found.");

        var isExists = await projectRepository
            .AsQueryable()
            .AnyAsync(x => x.Id != request.Id
                && x.Status != RecordStatusEnum.Deleted
                && x.Name.Trim().ToUpper().Equals(request.Name.Trim().ToUpper()),
                cancellationToken);

        if (isExists)
        {
            throw new InvalidOperationException($"Project with \"{request.Name}\" already exists.");
        }
        else
        {
            project.Name = request.Name;
            project.Description = request.Description;
            project.UpdatedAt = Utility.GetCurrentDateTimeOffset();
            project.UpdatedById = request.UserId;
            project.Type = request.Type;
            project.TeamId = request.TeamId;

            projectRepository.Update(project);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
