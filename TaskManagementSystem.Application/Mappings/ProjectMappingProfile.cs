using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class ProjectMappingProfile: Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<AddProjectDto, AddProjectCommand>();
    }
}
