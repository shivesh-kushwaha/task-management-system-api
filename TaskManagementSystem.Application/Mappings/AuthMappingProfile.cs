using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AddUserDto, AddUserCommand>();
    }
}
