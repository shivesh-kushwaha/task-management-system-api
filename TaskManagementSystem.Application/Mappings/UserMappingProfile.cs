using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.Dtos;
using TaskManagementSystem.Application.Queries.User.GetUserPagedList;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        // Commands
        CreateMap<AddUserDto, AddUserCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetUserPagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
