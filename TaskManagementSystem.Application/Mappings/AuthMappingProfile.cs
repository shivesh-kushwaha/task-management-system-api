using TaskManagementSystem.Application.Auth.Commands.AddUser;
using TaskManagementSystem.Application.Auth.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AddUserDto, AddUserCommand>();
    }
}
