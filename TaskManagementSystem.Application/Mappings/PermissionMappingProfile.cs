using TaskManagementSystem.Application.Commands.Permission.AddPermission;
using TaskManagementSystem.Core.Dtos.Permission.AddPermission;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<AddPermissionDto, AddPermissionCommand>();
    }
}
