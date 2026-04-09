using TaskManagementSystem.Core.Dtos.Project.GetProjectById;

namespace TaskManagementSystem.Application.Queries.Project.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(
    IProjectRepository projectRepository)
    : IQueryHandler<GetProjectByIdQuery, GetProjectByIdDto>
{
    public async Task<GetProjectByIdDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (response == null)
            throw new InvalidOperationException("Project does not found.");
        else
            return response;
    }
}
