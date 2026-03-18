using AutoMapper;
using TaskManagementSystem.Application.Auth.Commands.AddUser;
using TaskManagementSystem.Application.Auth.Dtos;

namespace TaskManagementSystem.Application.Mappings;

public sealed class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AddUserDto, AddUserCommand>();
    }
}
