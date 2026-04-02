using TaskManagementSystem.Application.Commands.Role.AddRole;
using TaskManagementSystem.Application.Commands.Role.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class RoleMappingProfile: Profile
{
    public RoleMappingProfile()
    {
        CreateMap<AddRoleDto, AddRoleCommand>();
    }
}
