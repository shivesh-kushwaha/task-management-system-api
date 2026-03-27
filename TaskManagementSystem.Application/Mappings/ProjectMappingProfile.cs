using TaskManagementSystem.Application.Commands.Project.AddProject;
using TaskManagementSystem.Application.Commands.Project.Dtos;
using TaskManagementSystem.Application.Queries.Project.GetProjectPagedList;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class ProjectMappingProfile: Profile
{
    public ProjectMappingProfile()
    {
        // Commands
        CreateMap<AddProjectDto, AddProjectCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetProjectPagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
