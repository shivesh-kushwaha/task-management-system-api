using TaskManagementSystem.Application.Commands.Auth.AuthLogin;
using TaskManagementSystem.Application.Commands.Auth.Dtos;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AuthLoginDto, AuthLoginCommand>();
    }
}
