using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.UpdateWorkItem;
using TaskManagementSystem.Application.Queries.WorkItem.GetWorkItemPagedList;
using TaskManagementSystem.Core.Dtos.WorkItem.AddWorkItem;
using TaskManagementSystem.Core.Dtos.WorkItem.UpdateWorkItem;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class WorkItemMappingProfile : Profile
{
    public WorkItemMappingProfile()
    {
        // Commands
        CreateMap<AddWorkItemDto, AddWorkItemCommand>();
        CreateMap<UpdateWorkItemDto, UpdateWorkItemCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetWorkItemPagedListQuery>()
           .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
