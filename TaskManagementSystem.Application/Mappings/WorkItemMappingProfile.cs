using TaskManagementSystem.Application.Commands.WorkItem.AddWorkItem;
using TaskManagementSystem.Application.Commands.WorkItem.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class WorkItemMappingProfile: Profile
{
    public WorkItemMappingProfile()
    {
        CreateMap<AddWorkItemDto, AddWorkItemCommand>();
    }
}
