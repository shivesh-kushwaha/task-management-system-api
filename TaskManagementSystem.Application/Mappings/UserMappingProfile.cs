using TaskManagementSystem.Application.Commands.User.AddUser;
using TaskManagementSystem.Application.Commands.User.UpdateUser;
using TaskManagementSystem.Application.Queries.User.GetUserPagedList;
using TaskManagementSystem.Core.Dtos.User.AddUser;
using TaskManagementSystem.Core.Dtos.User.UpdateUser;

namespace TaskManagementSystem.Application.Mappings;

internal sealed class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        // Commands
        CreateMap<AddUserDto, AddUserCommand>();
        CreateMap<UpdateUserDto, UpdateUserCommand>();

        // Queries
        CreateMap<PagedListRequestDto, GetUserPagedListQuery>()
            .ForMember(src => src.Filter, opt => opt.MapFrom(dest => dest));
    }
}
