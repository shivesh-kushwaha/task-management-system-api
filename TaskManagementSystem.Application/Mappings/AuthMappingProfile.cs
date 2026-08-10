using TaskManagementSystem.Application.Commands.Auth.AuthLogin;
using TaskManagementSystem.Application.Commands.Auth.AuthLogout;
using TaskManagementSystem.Core.Dtos.Auth.AuthLogin;
using TaskManagementSystem.Core.Dtos.Auth.AuthLogout;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AuthLoginDto, AuthLoginCommand>();
        CreateMap<AuthLogoutDto, AuthLogoutCommand>();
    }
}
