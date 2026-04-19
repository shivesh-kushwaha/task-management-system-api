using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.UpdateProject;
using TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;
using TaskManagementSystem.Core.Dtos.Project.AddProject;
using TaskManagementSystem.Core.Dtos.Project.UpdateProject;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class ProjectMappingProfile: Profile
{
    public ProjectMappingProfile()
    {
        // Commands
        CreateMap<AddProjectDto, AddProjectCommand>();
        CreateMap<UpdateProjectDto, UpdateProjectCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetProjectPagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
