using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.Dtos;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class WorkItemMappingProfile: Profile
{
    public WorkItemMappingProfile()
    {
        // Commands
        CreateMap<AddWorkItemDto, AddWorkItemCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetWorkItemPagedListQuery>()
           .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
