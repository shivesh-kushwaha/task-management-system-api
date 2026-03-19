using TaskManagementSystem.Application.Role.Commands.AddRole;
using TaskManagementSystem.Application.Role.Commands.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class RoleMappingProfile: Profile
{
    public RoleMappingProfile()
    {
        CreateMap<AddRoleDto, AddRoleCommand>();
    }
}
