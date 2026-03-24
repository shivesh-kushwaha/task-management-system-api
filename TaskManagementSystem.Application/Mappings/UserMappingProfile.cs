using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        CreateMap<AddUserDto, AddUserCommand>();
    }
}
