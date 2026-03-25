namespace TaskManagementSystem.Application.Commands.Project.AddProject;

internal sealed class AddProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddProjectCommand>
{
    public async Task Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Core.Entities.Project
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            TeamId = request.TeamId,
            CreatedById = request.UserId,
            CreatedAt = Utility.GetCurrentDateTimeOffset(),
            Status = RecordStatusEnum.Active,

        };

        await projectRepository.AddAsync(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
