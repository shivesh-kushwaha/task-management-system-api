using TaskManagementSystem.Application.Commands.RolePermission.UpsertRolePermission;
using TaskManagementSystem.Core.Dtos.RolePermission.UpsertRolePermission;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class RolePermissionMappingProfile : Profile
{
    public RolePermissionMappingProfile()
    {
        CreateMap<UpsertRolePermissionDto, UpsertRolePermissionCommand>();
    }
}
